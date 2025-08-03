using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Interfaces.Repositories.Shared;
using bankApI.Interfaces.ServicesInterfaces.Shared;

namespace bankApI.Services.Shared
{
    public class ClientInfoGetService(IClientInfoGetRepository _ClientInfoGetRepository) : IClientInfoGetService
    {

        public async Task<ServiceResponseDto<GetClientInfoDto?>> GetInfoAsync(int Id)
        {
            var data = await _ClientInfoGetRepository.GetClientInfo(Id);
            return new ServiceResponseDto<GetClientInfoDto?> { Status = 200, Data = data };
        }

        public async Task<ServiceResponseDto<List<AccountGetDto>>> GetAccountsAsync(int Id)
        {
            var data = await _ClientInfoGetRepository.GetClientAccounts(Id);
            return new ServiceResponseDto<List<AccountGetDto>> { Status = 200, Data = data };

        }


    }
}
