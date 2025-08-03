using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;
using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Dto_s.Client.TransactionsHistory;
using bankApI.Models.ClientModels;

namespace bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces
{
    public interface ITransactionsManagementService
    {
        public Task<ServiceResponseDto<TransactionsHistoryPaginatedGetDto>> GetTransactionsAsync(string Account, int PageNumber, int PageSize);
        public Task<ServiceResponseDto<TransferFundPaginatedGetDto>> GetTransfersAsync(string Account, int PageNumber, int PageSize);
        public Task<ServiceResponseDto<object?>> TransferFundAsync(TransferFundSetDto form,string Account);
        public Task<ServiceResponseDto<List<TransactionsHistoryGetDto>>> GetLastMonthTransactionsAsync(int Id);
    }
}
