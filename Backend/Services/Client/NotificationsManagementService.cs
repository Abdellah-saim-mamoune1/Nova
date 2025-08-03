using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Data;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;
using bankApI.Models.ClientModels;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Services.Client
{
    public class NotificationsManagementService(

        AppDbContext Context,
        INotificationsRepository _NotificationsRepository

        ) : INotificationsManagementService
    {

        public async Task<ServiceResponseDto<NotificationsPaginatedGetDto>> GetAsync(string Account,int PageNumber,int PageSize)
        {
            var data = await _NotificationsRepository.GetAsync(Account,PageNumber, PageSize);
            return new ServiceResponseDto< NotificationsPaginatedGetDto > { Status = 200,Data=data };
        }
        public async Task<ServiceResponseDto<int>> GetNonReadNotificationsCountAsync(string Account)
        {
            var data = await _NotificationsRepository.GetNonReadNotificationsCountAsync(Account);
            return new ServiceResponseDto<int> { Status = 200, Data = data };
        }
        public async Task<ServiceResponseDto<object?>> MarkAsViewedAsync(int Id,string Account)
        {
            var Notification = await ValidateNotificationExistence(Id);
            if (Notification == null)
                return new ServiceResponseDto<object?> {Status=400 };

            bool result = await _NotificationsRepository.MarkAsViewedAsync(Account,Notification);
            if (!result)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };
        }
        private async Task<ClientXNotifications?> ValidateNotificationExistence(int Id)
        {
            return await Context.ClientXNotifications.Include(n=>n.Notification).FirstOrDefaultAsync(n => n.NotificationId == Id);
        }

    }
}
