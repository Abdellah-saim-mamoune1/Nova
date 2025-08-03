using bankApI.Data;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.Repositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Repositories.Employee
{
    public class BankRevenueManagementRepository(AppDbContext Context,ILogger<BankRevenueManagementRepository> _logger) : IBankRevenueManagementRepository
    {

        public async Task<bool> SetTransactionRevenue(double Amount,string Source,int TransactionId,string? Note = null)
        {
            try
            {
                var Revenue = new Models.EmployeeModels.BankRevenue
                {
                    Source = Source,
                    Amount = Amount,
                    Note = Note,
                    RelatedTransactionId = TransactionId
                };

                Context.Add(Revenue);
                await Context.SaveChangesAsync();
                return true;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error while setting transaction revenue.");
                return false;
            }
        }

        public async Task<BankRevenueGetPaginated> GetTransactionRevenuePaginated(int PageNumber,int PageSize)
        {
            var AllRevenues = Context.BankRevenues.AsQueryable();

            var Revenues = await AllRevenues.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();

            var PaginatedRevenues=new BankRevenueGetPaginated { BankRevenues=Revenues,TotalPages= (int)Math.Ceiling((float)AllRevenues.Count() / PageSize) };

            return PaginatedRevenues;

        }

    }
}
