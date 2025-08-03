using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Data;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using Microsoft.EntityFrameworkCore;
namespace bankApI.Repositories.EmployeeRepositories
{

    public class StatisticsRepository(AppDbContext Context) : IStatisticsRepository
    {

        public async Task<GetCardsInfoDto> GeDashBoardStatsAsync()
        {

            DateOnly CurrentWeek = DateOnly.FromDateTime(DateTime.Today).AddDays(-17);

            short newClients = 0;
            await Context.Clients.ForEachAsync(a =>
            {
                if (a.CreatedAt >= CurrentWeek)
                    newClients++;
            });
            GetCardsInfoDto data = new GetCardsInfoDto
            {
                totalStaff = await Context.Employees.AsQueryable().CountAsync(),
                totalDeposits = await Context.TransactionsHistory.AsQueryable().Where(t => t.TransactionsType!.Type == "Deposit").CountAsync(),
                totalWithdraws = await Context.TransactionsHistory.AsQueryable().Where(t => t.TransactionsType!.Type == "Withdraw").CountAsync(),
                newClients = newClients,
                totalTransfers = await Context.TransferFundHistory.AsQueryable().CountAsync(),
                totalClients = await Context.Clients.AsQueryable().CountAsync(),

            };

            return data;
        }

    }
}
