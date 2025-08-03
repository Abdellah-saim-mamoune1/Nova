using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Dto_s.Employee;

namespace bankApI.Interfaces.ServicesInterfaces.Employee
{
    public interface IBankRevenueService
    {

        public Task<ServiceResponseDto<BankRevenueGetPaginated>> GetBankRevenuesPaginatedAsync(int PageNumber, int PageSize);
    }
}
