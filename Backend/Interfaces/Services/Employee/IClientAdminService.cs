using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Dto_s.Employee;

namespace bankApI.Interfaces.ServicesInterfaces.Employee
{
    public interface IClientAdminService
    {
        public  Task<ServiceResponseDto<GetPaginatedClientsInfoDto>> GetAllClientsAsync(int PageNumber, int PageSize);
        public  Task<ServiceResponseDto<string>> AddNewClientAsync(PersonClientSetDto Client);
        public  Task<ServiceResponseDto<AccountGetDto?>> AddNewAccountAsync(BankEmailDto Account);
        public  Task<ServiceResponseDto<object?>> FreezeClientAccountAsync(SetEmailStateDto state);
        public  Task<ServiceResponseDto<object?>> SendClientAccountMessage(NotificationsDto Notification);
        
    }
}
