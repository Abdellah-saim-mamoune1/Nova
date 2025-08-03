using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;
using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.Dto_s.Client.TransactionsHistory;

namespace bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ITransactionsManagementRepository
    {
        public Task<int> TransferFundAsync(TransferFundSetDto form, string Account);
        public Task<TransactionsHistoryPaginatedGetDto> GetTransactionsHistoryPaginatedAsync(string Account, int PageNumber, int PageSize);
        public Task<TransferFundPaginatedGetDto> GetTransfersHistoryPaginatedAsync(string Account, int PageNumber, int PageSize);
    }
}
