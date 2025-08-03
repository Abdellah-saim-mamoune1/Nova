using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.Repositories.Employee;
using bankApI.Interfaces.ServicesInterfaces.Employee;

namespace bankApI.Services.Employee
{
    public class BankRevenueService
        (
        IBankRevenueManagementRepository _BankRevenueManagementRepository
        ): IBankRevenueService
    {

        public async Task<ServiceResponseDto<BankRevenueGetPaginated>> GetBankRevenuesPaginatedAsync(int PageNumber, int PageSize)
        {
            var data = await _BankRevenueManagementRepository.GetTransactionRevenuePaginated(PageNumber, PageSize);

            return new ServiceResponseDto<BankRevenueGetPaginated> { Status = 200, Data = data };

        }


    }
}
