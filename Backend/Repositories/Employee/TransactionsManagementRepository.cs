using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Data;
using bankApI.Dto_s.Client.TransactionsHistory;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.Repositories.Employee;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bankApI.Repositories.Employee
{
    public class TransactionsManagementRepository
        (

         AppDbContext Context
        ,IMemoryCache _Cache
        ,ILogger<TransactionsManagementRepository> _logger

        ) : ITransactionsManagementRepository
    {
        private static readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);

        public async Task<ClientsTransactionsHistoryPaginatedGetDto?> GetTransactionsHistoryPaginatedAsync(int PageNumber, int PageSize)
        {

            if (_Cache.TryGetValue($"TransactionsHistory_{PageNumber}", out ClientsTransactionsHistoryPaginatedGetDto? PaginatedTransactionsHistory))
                return PaginatedTransactionsHistory;
          
                await _cacheLock.WaitAsync();
            try
            {
                if (_Cache.TryGetValue($"TransactionsHistory_{PageNumber}", out ClientsTransactionsHistoryPaginatedGetDto? cached))
                    return cached;

                var AllTransactionsHistory = Context.TransactionsHistory
                .Include(t => t.Account)
                .ThenInclude(a => a!.Person)
                .Include(t => t.TransactionsType)
                .AsQueryable()
                .OrderByDescending(t => t.CreatedAt);

                var transactionsHistory = await AllTransactionsHistory
                .Select(t => new TransactionsHistoryGetDto
                {
                    AccountId = t.Account!.Id,
                    Account = t.Account.AccountAddress,
                    CreatedAt=t.CreatedAt,
                    Amount = t.Amount,
                    Type = t.TransactionsType!.Type,
                }).Skip((PageNumber - 1) * PageSize).Take(PageSize)
                .ToListAsync();

                PaginatedTransactionsHistory!.TotalPages=(int)Math.Ceiling((float)AllTransactionsHistory.Count()/PageSize);
                PaginatedTransactionsHistory.Transactions = transactionsHistory;
                _Cache.Set<ClientsTransactionsHistoryPaginatedGetDto>($"TransactionsHistory_{PageNumber}", PaginatedTransactionsHistory, TimeSpan.FromMinutes(40));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching transactions history.");
            }

            finally
            {
                _cacheLock.Release();
            }
            return PaginatedTransactionsHistory;
        }
  
        public async Task<TransferFundPaginatedGetDto?> GetTransfersHistoryPaginatedAsync(int PageNumber, int PageSize)
        {

            if (_Cache.TryGetValue($"TransfersHistory_{PageNumber}", out TransferFundPaginatedGetDto? PaginatedTransfersHistory))
                return PaginatedTransfersHistory;

            await _cacheLock.WaitAsync();
            try
            {
                if (_Cache.TryGetValue($"TransfersHistory_{PageNumber}", out TransferFundPaginatedGetDto? cached))
                    return cached;
                var AllTransfers = Context.TransferFundHistory.AsQueryable()

                    .Include(t => t.SenderAccountIds)
                    .Include(t => t.RecipientAccountIds)
                    .OrderByDescending(t => t.CreatedAt);

                var TransfersHistory = await AllTransfers
                    .Select(t => new TransferFundGetDto
                    {
                        Id=t.Id,
                        SenderAccount = t.SenderAccountIds!.AccountAddress,
                        RecipientAccount = t.RecipientAccountIds!.AccountAddress,
                        Amount = t.Amount,
                        CreatedAt=t.CreatedAt

                    }).Skip((PageNumber - 1) * PageSize).Take(PageSize)
                    .ToListAsync();

                PaginatedTransfersHistory!.Transfers = TransfersHistory;
                PaginatedTransfersHistory.TotalPages = (int)Math.Ceiling((float)AllTransfers.Count() / PageSize);

                _Cache.Set<TransferFundPaginatedGetDto>($"TransfersHistory_{PageNumber}", PaginatedTransfersHistory, TimeSpan.FromHours(5));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching transfers history.");
            }

            finally
            {
                _cacheLock.Release();
            }
            return PaginatedTransfersHistory;
        }

        async public Task<ClientsTransactionsHistoryPaginatedGetDto?> GetTransactionsHistoryPaginatedAsync(int ClientId, int PageNumber, int PageSize)
        {
            var PaginatedTransactions = new ClientsTransactionsHistoryPaginatedGetDto();
            try
            {
                var AllTransactions = Context.TransactionsHistory.Include(t => t.Account).AsQueryable()
                   .Include(t => t.TransactionsType)
                   .Where(A => A.Account!.PersonId == ClientId);

                var Transactions = await AllTransactions
                   .Select(t =>
                    new bankApI.Dto_s.Employee.TransactionsHistoryGetDto
                    {
                        Id = t.Id,
                        Amount = t.Amount,
                        Type = t.TransactionsType!.Type,
                        CreatedAt = t.CreatedAt,
                        AccountId = t.AccountId,
                        Account = t.Account!.AccountAddress
                    }
                ).Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();

                PaginatedTransactions.Transactions = Transactions;
                PaginatedTransactions.TotalPages = (int)Math.Ceiling((float)AllTransactions.Count() / PageSize);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching client transactions history.");
            }

            return PaginatedTransactions;
        }
       
        async public Task<TransferFundPaginatedGetDto?> GetTransfersHistoryPaginatedAsync(int Id, int PageNumber, int PageSize)
        {
            var PaginatedTransfers = new TransferFundPaginatedGetDto();
            try
            {
                var account = await Context.Accounts.AsQueryable().FirstAsync(A => A.PersonId == Id);

                var AllTransfers = Context.TransferFundHistory.AsQueryable().Include(t => t.SenderAccountIds).Include(t => t.RecipientAccountIds)
                   .Where(A => A.SenderAccountId == account.Id || A.RecipientAccountId == account.Id);

                var Transfers = await AllTransfers
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

                PaginatedTransfers.Transfers = Transfers;
                PaginatedTransfers.TotalPages= (int)Math.Ceiling((float)AllTransfers.Count() / PageSize);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching client transfers history.");
            }

            return PaginatedTransfers;
        }

        public async Task<int> Withdraw(DepositWithdrawDto form)
        {
            int TransactionId = -1;
            try
            {
                await Context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await Context.Database.BeginTransactionAsync();
                    var ClientAccount = await Context.Accounts.FirstAsync(c => c.AccountAddress == form.ClientAccount);
                    var EmployeeAccount = await Context.EmployeeAccount.FirstAsync(e => e.Id == form.EmployeeAccountId);

                    ClientAccount.Balance -= form.Amount;

                    var Transaction= new TransactionsHistory
                    {
                        AccountId = ClientAccount.Id,
                        TypeId = 2,
                        Amount = form.Amount,
                        EmployeeAccountId=form.EmployeeAccountId


                    };
                     Context.Add(Transaction);
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TransactionId = Transaction.Id;
                });
                _Cache.Remove("TransactionsHistory");
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while withdrawing.");
            }
            return TransactionId;


        }

        public async Task<int> Deposit(DepositWithdrawDto form)
        {
            int TransactionId = -1;

            try
            {
                await Context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await Context.Database.BeginTransactionAsync();
                    var ClientAccount = await Context.Accounts.AsQueryable().FirstAsync(c => c.AccountAddress == form.ClientAccount);

                    ClientAccount.Balance += form.Amount;

                    var Transaction = new TransactionsHistory
                    {
                        AccountId = ClientAccount.Id,
                        TypeId = 1,
                        Amount = form.Amount,
                        EmployeeAccountId = form.EmployeeAccountId
                    };
                     Context.Add(Transaction);
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TransactionId = Transaction.Id;
                });
                _Cache.Remove("TransactionsHistory"); 

            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while depositing.");
            }

            return TransactionId;
        }

    }
}
