using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.Interfaces.ServicesInterfaces.Shared
{
    public interface IClientInfoGetService
    {
        public Task<ServiceResponseDto<GetClientInfoDto?>> GetInfoAsync(int Id);
        public Task<ServiceResponseDto<List<AccountGetDto>>> GetAccountsAsync(int Id);
    }
}
