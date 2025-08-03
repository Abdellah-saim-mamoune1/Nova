using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Dto_s.Employee;
using bankApI.Models.EmployeeModels;

namespace bankApI.Interfaces.RepositoriesInterfaces.Employee
{
    public interface IEmployeeManagementRepository
    {
        public Task<bool> AddNewEmployeeAsync(EmployeePersonDto employee);
        public Task<EmployeeGetDto?> GetEmployeeAsync(int Id);
        public Task<EmployeesPaginatedGetDto> GetAllEmployeesAsync(int PageNumber, int PageSize);
        public Task<bool> UpdateEmployeeAsync(EmployeeAccount Employee, EmployeeUpdateDto EmployeeInfo);
        public Task<bool> FreezeUnfreezeEmployeeAccountAsync(EmployeeAccount Account, SetEmailStateDto state);
        public Task<bool> SendEmployeeMessage(NotificationsDto Notification, int Id);
        public Task LoginRegisterAsync(string EmployeeAccount);
        public Task<LoginRegisterHistoryGetPaginatedDto> GetLoginRegisterHistoryPaginatedAsync(int PageNumber, int PageSize);
    }
}
