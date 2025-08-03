using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Dto_s.Client.TransactionsHistory;

namespace bankApI.Interfaces.Repositories.Employee
{
    public interface ITransactionsManagementRepository
    {
        public Task<ClientsTransactionsHistoryPaginatedGetDto?> GetTransactionsHistoryPaginatedAsync(int PageNumber, int PageSize);
        public Task<TransferFundPaginatedGetDto?> GetTransfersHistoryPaginatedAsync(int PageNumber, int PageSize);
        public Task<TransferFundPaginatedGetDto?> GetTransfersHistoryPaginatedAsync(int Id, int PageNumber, int PageSize);
        public Task<ClientsTransactionsHistoryPaginatedGetDto?> GetTransactionsHistoryPaginatedAsync(int ClientId, int PageNumber, int PageSize);
        public Task<int> Deposit(DepositWithdrawDto form);
        public Task<int> Withdraw(DepositWithdrawDto form);
    }
}
