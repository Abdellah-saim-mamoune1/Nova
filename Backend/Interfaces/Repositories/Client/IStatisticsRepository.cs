using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;

namespace bankApI.Interfaces.Repositories.Client
{
    public interface IStatisticsRepository
    {
        public Task<List<TransactionsHistoryGetDto>> GetLastMonthTransactionsAsync(int Id);
    }
}
