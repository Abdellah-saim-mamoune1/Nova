using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces
{
    public interface INotificationsManagementService
    {
        public Task<ServiceResponseDto<NotificationsPaginatedGetDto>> GetAsync(string Account,int PageNumber,int PageSize);
        public Task<ServiceResponseDto<object?>> MarkAsViewedAsync(int Id, string Account);
        public Task<ServiceResponseDto<int>> GetNonReadNotificationsCountAsync(string Account);
    }
}
