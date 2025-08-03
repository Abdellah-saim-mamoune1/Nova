using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;
using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.Data;
using bankApI.Dto_s.Client.TransactionsHistory;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bankApI.Repositories.ClientRepositories
{
    public class TransactionsRepository(AppDbContext Context,IMemoryCache _Cache,ILogger<TransactionsRepository> _logger) : ITransactionsManagementRepository
    {
        async public Task<int> TransferFundAsync(TransferFundSetDto form,string Account)
        {
            int TransferId = -1;
            int AccountId=-1;
            try
            {
               
                await Context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await Context.Database.BeginTransactionAsync();

                    var SenderAccount = await Context.Accounts.FirstAsync(a => a.AccountAddress == Account);
                    var RecipientAccount = await Context.Accounts.FirstAsync(a => a.AccountAddress == form.RecipientAccount);
                    AccountId = SenderAccount.Id;

                    RecipientAccount.Balance += form.Amount;
                    SenderAccount.Balance -= form.Amount;

                    TransferFundHistory Record = new TransferFundHistory
                    {
                        SenderAccountId = SenderAccount.Id,
                        RecipientAccountId = RecipientAccount.Id,
                        Amount = form.Amount,
                        Description = form.Description
                    };

                    await Context.TransferFundHistory.AddAsync(Record);
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TransferId = Record.Id;
                });

                _Cache.Remove("TransactionsHistory");
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while marking client notification as viewed.");
            }

            await SendTransferNotification(TransferId != -1,AccountId, form);
           
            return TransferId;
        }
        async public Task<TransactionsHistoryPaginatedGetDto> GetTransactionsHistoryPaginatedAsync(string Account, int PageNumber, int PageSize)
        {
            var PaginatedTransactions = new TransactionsHistoryPaginatedGetDto();
            try
            {
                var AllTransactions = Context.TransactionsHistory.Include(t => t.Account)
                   .Include(t => t.TransactionsType).AsQueryable()
                   .Where(A => A.Account!.AccountAddress == Account);

                var transactions = await AllTransactions
                   .Select(t =>
                    new TransactionsHistoryGetDto
                    {
                        Id = t.Id,
                        Amount = t.Amount,
                        Type = t.TransactionsType!.Type,
                        CreatedAt = t.CreatedAt,
                    }
                ).Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();

                PaginatedTransactions.TotalPages = (int)Math.Ceiling((float)AllTransactions.Count() / PageSize);
                PaginatedTransactions.Transactions = transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to get transactions.");
            }

            return PaginatedTransactions;
        }     
        async public Task<TransferFundPaginatedGetDto> GetTransfersHistoryPaginatedAsync(string Account, int PageNumber, int PageSize)
        {
            var Transfers = new TransferFundPaginatedGetDto();
            try
            {

                var AllTransfers = Context.TransferFundHistory.
                    Include(t => t.SenderAccountIds)
                    .Include(t => t.RecipientAccountIds)
                    .AsQueryable()
                   .Where(A => A.SenderAccountIds!.AccountAddress == Account
                   || A.RecipientAccountIds!.AccountAddress == Account);

                   var transfers= await AllTransfers
                   .Select(w =>
                    new TransferFundGetDto
                    {
                        Id = w.Id,
                        Amount = w.Amount,
                        SenderAccount = w.SenderAccountIds!.AccountAddress,
                        RecipientAccount = w.RecipientAccountIds!.AccountAddress,
                        CreatedAt = w.CreatedAt,

                    }
                ).Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();

                Transfers.Transfers = transfers;
                Transfers.TotalPages = (int)Math.Ceiling((float)AllTransfers.Count() / PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to get transfers.");
            }

            return Transfers;
        }
        private async Task SendTransferNotification(bool status,int AccountId,TransferFundSetDto form)
        {

            try
            {
                var body = status ? $"The process of transferring {form.Amount} to {form.RecipientAccount} went successfully"
                    : $"The process of transferring {form.Amount} to {form.RecipientAccount} failed";

                var Notification = new Notification
                {
                    Title = "Transfer",
                    Body = body,
                    TypeId = 2,

                };
                Context.Add(Notification);
                await Context.SaveChangesAsync();

                var ClientNotification = new ClientXNotifications
                {
                    AccountId = AccountId,
                    NotificationId = Notification.Id,

                };
                Context.Add(ClientNotification);
                await Context.SaveChangesAsync();
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error while trying to send transfer notification.");
            }



        }
      
    }
}
