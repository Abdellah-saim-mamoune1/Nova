using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Dto_s.Employee;

namespace bankApI.Interfaces.RepositoriesInterfaces.Employee
{
    public interface IClientAdminRepository
    {
        public Task<string?> AddNewClientAsync(PersonClientSetDto Client);
        public Task<GetPaginatedClientsInfoDto> GetAllClientsAsync(int PageNumber, int PageSize);
        public Task<AccountGetDto?> AddNewAccountAsync(BankEmailDto email);
        public Task<bool> FreezeClientAccountAsync(SetEmailStateDto state);
        public Task<bool> SendClientAccountMessage(NotificationsDto Notification);
       
    }
}
