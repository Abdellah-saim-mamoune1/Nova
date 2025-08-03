using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Data;
using bankApI.Dto_s.Client.TransactionsHistory;
using bankApI.Interfaces.Repositories.Employee;
using bankApI.Interfaces.ServicesInterfaces.Employee;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Services.Employee
{
    public class TransactionsService(
        AppDbContext Context,
        ITransactionsManagementRepository _TransactionsManagementRepository,
        IBankRevenueManagementRepository _BankRevenueManagementRepository

        ) : ITransactionsService
    {

        public async Task<ServiceResponseDto<ClientsTransactionsHistoryPaginatedGetDto?>> GetTransactionsHistoryPaginatedAsync(int PageNumber, int PageSize)
        {
            var data = await _TransactionsManagementRepository.GetTransactionsHistoryPaginatedAsync(PageNumber, PageSize);
            return new ServiceResponseDto<ClientsTransactionsHistoryPaginatedGetDto?> { Status = 200,Data=data };

        }

        public async Task<ServiceResponseDto<ClientsTransactionsHistoryPaginatedGetDto?>> GetTransactionsHistoryPaginatedAsync(int Id,int PageNumber, int PageSize)
        {
            var data = await _TransactionsManagementRepository.GetTransactionsHistoryPaginatedAsync(Id,PageNumber, PageSize);
            return new ServiceResponseDto<ClientsTransactionsHistoryPaginatedGetDto?> { Status = 200, Data = data };

        }

        public async Task<ServiceResponseDto<TransferFundPaginatedGetDto?>> GetTransfersHistoryPaginatedAsync(int PageNumber, int PageSize)
        {
            var data = await _TransactionsManagementRepository.GetTransfersHistoryPaginatedAsync(PageNumber, PageSize);
            return new ServiceResponseDto<TransferFundPaginatedGetDto?> { Status = 200, Data = data };

        }

        public async Task<ServiceResponseDto<TransferFundPaginatedGetDto?>> GetTransfersHistoryPaginatedAsync(int Id,int PageNumber, int PageSize)
        {
            var data = await _TransactionsManagementRepository.GetTransfersHistoryPaginatedAsync(Id,PageNumber, PageSize);
            return new ServiceResponseDto<TransferFundPaginatedGetDto?> { Status = 200, Data = data };

        }

        public async Task<ServiceResponseDto<object?>> Deposit(DepositWithdrawDto form)
        {
            var transactionId = await _TransactionsManagementRepository.Deposit(form);

            if(transactionId == -1)
                return new ServiceResponseDto<object?> { Status = 500 };

            var RevenueResult = await _BankRevenueManagementRepository.SetTransactionRevenue(form.Amount * 0.01, "Deposit", transactionId, form.Note);

            if (!RevenueResult)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }

        public async Task<ServiceResponseDto<object?>> Withdraw(DepositWithdrawDto form)
        {
            if (!await ValidateWithdraw(form))
                return new ServiceResponseDto<object?> { Status = 400 };

            var transactionId = await _TransactionsManagementRepository.Withdraw(form);

            if (transactionId == -1)
                return new ServiceResponseDto<object?> { Status = 500 };

            var RevenueResult=await _BankRevenueManagementRepository.SetTransactionRevenue(form.Amount * 0.01, "Withdrawal", transactionId, form.Note);

            if(!RevenueResult)
                return new ServiceResponseDto<object?> { Status = 500 };


            return new ServiceResponseDto<object?> { Status = 200 };
        }
        private async Task<bool> ValidateWithdraw(DepositWithdrawDto form)
        {
            var account = await Context.Accounts.AsQueryable().FirstOrDefaultAsync(a => a.AccountAddress == form.ClientAccount);

            return account != null && account?.Balance > form.Amount;

        }
    }
}
