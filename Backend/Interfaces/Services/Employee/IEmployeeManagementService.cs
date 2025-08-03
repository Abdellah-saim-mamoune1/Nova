using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Dto_s.Employee;
using bankApI.Models.EmployeeModels;

namespace bankApI.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces
{
    public interface IEmployeeManagementService
    {
        public Task<ServiceResponseDto<EmployeeGetDto?>> GetEmployeeAsync(int Id);
        public Task<ServiceResponseDto<EmployeesPaginatedGetDto>> GetAllEmployeesAsync(int PageNumber,int PageSize);
        public Task<ServiceResponseDto<object?>> AddNewEmployeeAsync(EmployeePersonDto employee);
        public Task<ServiceResponseDto<object?>> UpdateEmployeeAsync(EmployeeUpdateDto EmployeeInfo);
        public Task<ServiceResponseDto<object?>> SendEmployeeMessage(NotificationsDto Notification, int Id);
        public Task<ServiceResponseDto<object?>> FreezeUnfreezeEmployeeAccountAsync(SetEmailStateDto state);
        public Task<ServiceResponseDto<LoginRegisterHistoryGetPaginatedDto>> LoginRegisterGetPaginatedAsync(int PageNumber, int PageSize);
    }
}
