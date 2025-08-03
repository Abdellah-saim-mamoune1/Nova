using bankApI.BusinessLayer.Dto_s;

namespace bankApI.Interfaces.Repositories.Shared
{
    public interface IClientInfoGetRepository
    {
         public Task<GetClientInfoDto?> GetClientInfo(int Id);
         public Task<List<AccountGetDto>> GetClientAccounts(int Id);
    }
}
