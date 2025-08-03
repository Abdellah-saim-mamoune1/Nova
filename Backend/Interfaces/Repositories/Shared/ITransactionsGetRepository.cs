using bankApI.Dto_s.Client.TransactionsHistory;

namespace bankApI.Interfaces.Repositories.Shared
{
    public interface ITransactionsGetRepository
    {
        public Task<List<bankApI.Dto_s.Employee.TransactionsHistoryGetDto>> GetTransactionsPaginatedAsync(int AccountId, int PageNumber, int PageSize);
        public Task<List<TransferFundPaginatedGetDto>> GetTransfersPaginatedAsync(int Id, int PageNumber, int PageSize);
    }
}
