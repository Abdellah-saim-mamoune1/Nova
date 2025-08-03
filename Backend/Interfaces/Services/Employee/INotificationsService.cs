using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.Interfaces.ServicesInterfaces.Employee
{
    public interface INotificationsService
    {
        public Task<ServiceResponseDto<NotificationsPaginatedGetDto>> GetNotificationsAsync(int Id, int PageNumber, int PageSize);
        public Task<ServiceResponseDto<object?>> MarkNotificationAsViewedAsync(int NotificationId, int EmployeeId);
    }
}
