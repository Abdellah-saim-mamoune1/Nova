using bankApI.Dto_s.Employee;

namespace bankApI.Interfaces.Repositories.Employee
{
    public interface IBankRevenueManagementRepository
    {
        public Task<bool> SetTransactionRevenue(double Amount, string Source, int TransactionId, string? Note = null);
        public Task<BankRevenueGetPaginated> GetTransactionRevenuePaginated(int PageNumber, int PageSize);
    }
}
