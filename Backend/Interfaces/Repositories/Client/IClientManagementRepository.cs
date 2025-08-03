using bankApI.BusinessLayer.Dto_s;

namespace bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface IClientManagementRepository
    {
        public Task<bool> UpdateAsync(UpdateClientDto form, int Id);
        public Task<bool> AddGetHelpRequestsAsync(NotificationsDto state);
       
    }
}
