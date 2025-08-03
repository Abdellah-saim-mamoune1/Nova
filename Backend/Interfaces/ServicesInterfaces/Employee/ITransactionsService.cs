using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Dto_s.Client.TransactionsHistory;

namespace bankApI.Interfaces.ServicesInterfaces.Employee
{
    public interface ITransactionsService
    {
        public Task<ServiceResponseDto<ClientsTransactionsHistoryPaginatedGetDto?>> GetTransactionsHistoryPaginatedAsync(int PageNumber, int PageSize);
        public Task<ServiceResponseDto<ClientsTransactionsHistoryPaginatedGetDto?>> GetTransactionsHistoryPaginatedAsync(int Id, int PageNumber, int PageSize);
        public Task<ServiceResponseDto<TransferFundPaginatedGetDto?>> GetTransfersHistoryPaginatedAsync(int PageNumber, int PageSize);
        public Task<ServiceResponseDto<TransferFundPaginatedGetDto?>> GetTransfersHistoryPaginatedAsync(int Id, int PageNumber, int PageSize);
        public Task<ServiceResponseDto<object?>> Deposit(DepositWithdrawDto form);
        public Task<ServiceResponseDto<object?>> Withdraw(DepositWithdrawDto form);
    }
}
