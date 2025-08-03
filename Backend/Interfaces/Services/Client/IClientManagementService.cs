using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces
{
    public interface IClientManagementService
    {
        public Task<ServiceResponseDto<object?>> UpdateAsync(UpdateClientDto form, int Id);
        public Task<ServiceResponseDto<object?>> AddGetHelpRequestAsync(NotificationsDto form);

    }
}
