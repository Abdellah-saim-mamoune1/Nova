using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;
using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Data;
using bankApI.Dto_s.Client.TransactionsHistory;
using bankApI.Interfaces.Repositories.Employee;
using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Services.Client
{
    public class TransactionsManagementService
        (
        
        AppDbContext Context,
        Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces.ITransactionsManagementRepository _Repo,
        IBankRevenueManagementRepository _BankRevenueManagementRepository,
        bankApI.Interfaces.Repositories.Client.IStatisticsRepository _StatisticsRepository

        ) : ITransactionsManagementService
    {
        public async Task<ServiceResponseDto<TransactionsHistoryPaginatedGetDto>> GetTransactionsAsync(string Account,int PageNumber,int PageSize)
        {
            var data = await _Repo.GetTransactionsHistoryPaginatedAsync(Account,PageNumber,PageSize);
            return new ServiceResponseDto<TransactionsHistoryPaginatedGetDto> { Status = 200, Data = data };
        }

        public async Task<ServiceResponseDto<TransferFundPaginatedGetDto>> GetTransfersAsync(string Account, int PageNumber, int PageSize)
        {
            var data = await _Repo.GetTransfersHistoryPaginatedAsync(Account, PageNumber, PageSize);
            return new ServiceResponseDto<TransferFundPaginatedGetDto > { Status = 200, Data = data };
        }

        public async Task<ServiceResponseDto<object?>> TransferFundAsync(TransferFundSetDto form,string Account)
        {
            if (!await ValidateTransfer(form,Account))
                return new ServiceResponseDto<object?> { Status = 400 };

            var result = await _Repo.TransferFundAsync(form, Account);

            if (result==-1)
                return new ServiceResponseDto<object?> { Status = 500 };


            var RevenueResult=await _BankRevenueManagementRepository.SetTransactionRevenue(form.Amount * 0.01, "Transfer", result);

            if(!RevenueResult)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200};
        }

        private async Task<bool> ValidateTransfer(TransferFundSetDto form, string Account)
        {
            if (Account == form.RecipientAccount)
                return false;

            var Sender = await Context.Accounts.AsQueryable().FirstOrDefaultAsync(e => e.AccountAddress == Account);
            if (Sender == null || Sender.Balance < form.Amount || form.Amount > 1000000 )
                return false;

            else if (!await Context.Accounts.AnyAsync(a => a.AccountAddress == form.RecipientAccount))
                return false;

            return true;

        }

        public async Task<ServiceResponseDto<List<TransactionsHistoryGetDto>>> GetLastMonthTransactionsAsync(int Id)
        {
            var data=await _StatisticsRepository.GetLastMonthTransactionsAsync(Id);
            return new ServiceResponseDto<List<TransactionsHistoryGetDto>> { Data = data, Status = 200 };

        }


    }
}
