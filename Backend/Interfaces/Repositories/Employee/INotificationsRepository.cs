using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.Interfaces.RepositoriesInterfaces.Employee
{
    public interface INotificationsRepository
    {
        public Task<NotificationsPaginatedGetDto> GetNotifications(int Id,int PageNumber,int PageSize);
        public Task<bool> MarkAsViewed(int NotificationId, int EmployeeId);
    }
}
