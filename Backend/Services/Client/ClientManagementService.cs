using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;

namespace bankApI.Services.Client
{
    public class ClientManagementService
        (
        IClientManagementRepository _Repo
        ): IClientManagementService
    {

        public async Task<ServiceResponseDto<object?>> UpdateAsync(UpdateClientDto form,int Id)
        {

            bool result= await _Repo.UpdateAsync(form,Id);
            if (!result)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }
        public async Task<ServiceResponseDto<object?>> AddGetHelpRequestAsync(NotificationsDto form)
        {

            bool result = await _Repo.AddGetHelpRequestsAsync(form);
            if (!result)
                return new ServiceResponseDto<object?> { Status = 500 };

            return new ServiceResponseDto<object?> { Status = 200 };

        }
      
    }
}
