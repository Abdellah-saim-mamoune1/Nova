using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.Data;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Repositories.ClientRepositories
{
    public class NotificationsRepository(AppDbContext Context,ILogger<NotificationsRepository> _logger) : INotificationsRepository
    {

        public async Task<NotificationsPaginatedGetDto> GetAsync(string Account,int PageNumber,int PageSize)
        {
            var TotalNotifications = Context.Accounts.Include(c => c.clientXNotifications).AsQueryable()
                .Where(c => c.AccountAddress == Account );

                var Notifications= await TotalNotifications
                    .SelectMany(a => a.clientXNotifications!)

                .Select(cxn => new NotificationsGetDto
                {
                    Id = cxn.Notification!.Id,
                    Title = cxn.Notification.Title,
                    Notification = cxn.Notification.Body,
                    Type = cxn.Notification.types!.Name,
                    Date = cxn.Date,
                    IsViewed = cxn.IsViewed

                }).Skip((PageNumber-1)*PageSize)
                .Take(PageSize)
                .ToListAsync();
               Notifications.Reverse();

            var PaginatedNotifications = new NotificationsPaginatedGetDto { Notifications = Notifications, 
                TotalPages =(int)Math.Ceiling((float)TotalNotifications.Count()/PageSize) };
              
                return PaginatedNotifications;
       
        }
        public async Task<int> GetNonReadNotificationsCountAsync(string Account)
        {

            return await Context.ClientXNotifications.Include(n=>n.Account)
                .AsQueryable().Where(n=>n.IsViewed==false&&n.Account!.AccountAddress==Account)
                .CountAsync();

        }
        public async Task<bool> MarkAsViewedAsync(string Account, ClientXNotifications Notification)
        {
            try
            {
                Notification.IsViewed = true;
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while marking client notification as viewed.");
                return false;
            }
        }

    }
}
