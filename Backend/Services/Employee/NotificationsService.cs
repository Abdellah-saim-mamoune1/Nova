using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.Employee;

namespace bankApI.Services.Employee
{
    public class NotificationsService
        (
         
        INotificationsRepository _NotificationsRepository

        ) : INotificationsService
    {
        public async Task<ServiceResponseDto<NotificationsPaginatedGetDto>> GetNotificationsAsync(int Id,int PageNumber, int PageSize)
        {
            var data = await _NotificationsRepository.GetNotifications(Id,PageNumber,PageSize);
            return new ServiceResponseDto<NotificationsPaginatedGetDto> { Status = 200 };

        }
        public async Task<ServiceResponseDto<object?>> MarkNotificationAsViewedAsync(int NotificationId, int EmployeeId)
        {
            var data = await _NotificationsRepository.MarkAsViewed(NotificationId, EmployeeId);
            if (!data)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }
    }
}
