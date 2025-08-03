using bankApI.BusinessLayer.Dto_s.Client.TransactionsHistoryDto;
using bankApI.Data;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Repositories.Client
{
    public class StatisticsRepository(AppDbContext Context): bankApI.Interfaces.Repositories.Client.IStatisticsRepository
    {

        public async Task<List<TransactionsHistoryGetDto>> GetLastMonthTransactionsAsync(int Id)
        {
            var lastMonth = DateTime.Now.AddDays(-30);

            var Transactions = await Context.TransactionsHistory
                .Include(t => t.Account)
                .Include(t => t.TransactionsType)
                .AsQueryable()
                .Where(t => t.Account!.PersonId == Id && t.CreatedAt >= lastMonth)
                .Select(t=>new TransactionsHistoryGetDto
                {
                    Id= t.Id,
                    Amount=t.Amount,
                    CreatedAt=t.CreatedAt,
                    Type=t.TransactionsType!.Type

                }).ToListAsync();

            return Transactions;
            
        }


    }
}
