using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Dto_s.Employee;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.Employee;

namespace bankApI.Services.EmployeeServices
{
    public class ClientAdminService(IClientAdminRepository _ClientAdminRepository) : IClientAdminService
    {

        public async Task<ServiceResponseDto<GetPaginatedClientsInfoDto>> GetAllClientsAsync(int PageNumber,int PageSize)
        {

            var data = await _ClientAdminRepository.GetAllClientsAsync(PageNumber, PageSize);
            return new ServiceResponseDto<GetPaginatedClientsInfoDto> { Status = 200,Data=data };

        }

        public async Task<ServiceResponseDto<string>> AddNewClientAsync(PersonClientSetDto Client)
        {

            var data = await _ClientAdminRepository.AddNewClientAsync(Client);

            if (data==null)
                return new ServiceResponseDto<string> { Status = 500 };

            return new ServiceResponseDto<string> { Status = 200,Data=data };

        }

        public async Task<ServiceResponseDto<AccountGetDto?>> AddNewAccountAsync(BankEmailDto Account)
        {

            var data = await _ClientAdminRepository.AddNewAccountAsync(Account);

            if(data==null)
                return new ServiceResponseDto<AccountGetDto?> { Status = 500 };

            return new ServiceResponseDto<AccountGetDto?> { Status = 200,Data=data };

        }

        public async Task<ServiceResponseDto<object?>> FreezeClientAccountAsync(SetEmailStateDto state)
        {

            var data = await _ClientAdminRepository.FreezeClientAccountAsync(state);

            if (!data)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200};

        }

        public async Task<ServiceResponseDto<object?>> SendClientAccountMessage(BusinessLayer.Dto_s.NotificationsDto Notification)
        {

            var data = await _ClientAdminRepository.SendClientAccountMessage(Notification);

            if (!data)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }

       
    }
}
