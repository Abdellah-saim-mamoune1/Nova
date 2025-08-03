using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.Data;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using Microsoft.EntityFrameworkCore;

namespace bankApI.Repositories.Employee
{
    public class NotificationsRepository(AppDbContext Context,ILogger<NotificationsRepository> _logger) : INotificationsRepository
    {
        public async Task<NotificationsPaginatedGetDto> GetNotifications(int Id,int PageNumber,int PageSize)
        {
            var AllNotifications = Context.EmployeeAccount
                .Include(c => c.EmployeeNotifications)
                .Where(c => c.EmployeeId == Id)
                .AsQueryable();

            var Notifications = await AllNotifications
                .SelectMany(a => a.EmployeeNotifications!)
                .Select(cxn => new NotificationsGetDto
                {
                    Id = cxn.Notification!.Id,
                    Title = cxn.Notification.Title,
                    Notification = cxn.Notification.Body,
                    Type = cxn.Notification.types!.Name,
                    Date = cxn.Date,
                    IsViewed = cxn.Isviewed

                }).Skip((PageNumber-1)*PageSize).
                  Take(PageSize)
                 .ToListAsync();

            return new NotificationsPaginatedGetDto { Notifications = Notifications,
                TotalPages = (int)Math.Ceiling((float)AllNotifications.Count() / PageSize) };

        }
        public async Task<bool> MarkAsViewed(int NotificationId, int EmployeeId)
        {
            try
            {

                var Notification = await Context.EmployeeNotifications
                    .AsQueryable()
                    .FirstAsync(c => c.NotificationId == NotificationId && c.Account!.EmployeeId == EmployeeId);
                Notification.Isviewed = true;
                await Context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while marking employee notification as viewed.");
                return false;
            }

        }

    }
}
