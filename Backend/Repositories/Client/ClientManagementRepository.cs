using bankApI.BusinessLayer.Dto_s;
using bankApI.Data;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bankApI.Repositories.ClientRepositories
{
    public class ClientManagementRepository
        (

        AppDbContext _db
        ,ILogger<ClientManagementRepository> _logger,
        IMemoryCache _cache

        ) : IClientManagementRepository
    {

        public async Task<bool> UpdateAsync(UpdateClientDto form,int Id)
        {
            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var Client = await _db.Clients.Include(c => c.Person).FirstAsync(c => c.PersonId==Id);

                    Client.Person!.FirstName = form.FirstName;
                    Client.Person.LastName = form.LastName;
                    Client.Person.PhoneNumber = form.PhoneNumber;
                    Client.Person.BirthDate = form.BirthDate;
                    Client.Person.Address = form.Address;
                    Client.Person.Gender = form.Gender;
                    Client.Person.Email = form.personalEmail;
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();

                });
                _cache.Remove($"Client_{Id}");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating client info.");
                return false;
            }


        }
     
        async public Task<bool> AddGetHelpRequestsAsync(NotificationsDto state)
        {

            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();


                    var account = await _db.Accounts.AsQueryable().FirstAsync(a => a.AccountAddress == state.Account);

                    Notification notification = new Notification
                    {
                        Body = "From " + account.AccountAddress + ": " + state.Body,
                        Title = state.Title,
                        TypeId = 1,
                    };

                    await _db.Notifications.AddAsync(notification);
                    await _db.SaveChangesAsync();

                    var EAccounts = await _db.EmployeeAccount.AsQueryable().ToListAsync();

                    List<EmployeeNotifications> EmployeeNotifications = new List<EmployeeNotifications>();

                    foreach (var n in EAccounts)
                    {

                        EmployeeNotifications notify = new EmployeeNotifications
                        {
                            AccountId = n.Id,
                            NotificationId = notification.Id,
                            Isviewed = false
                        };

                        EmployeeNotifications.Add(notify);

                    }

                    await _db.EmployeeNotifications.AddRangeAsync(EmployeeNotifications);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                });

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding get help request.");
                return false;
            }
            
        }


    }
}
