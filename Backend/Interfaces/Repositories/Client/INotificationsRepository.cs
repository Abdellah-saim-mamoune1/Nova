using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.Models.ClientModels;

namespace bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface INotificationsRepository
    {
        public Task<NotificationsPaginatedGetDto> GetAsync(string Account,int PageNumber,int PageSize);
        public Task<bool> MarkAsViewedAsync(string Account, ClientXNotifications Notification);
        public Task<int> GetNonReadNotificationsCountAsync(string Account);
    }
}
