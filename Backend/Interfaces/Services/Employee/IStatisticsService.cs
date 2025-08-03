using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;

namespace bankApI.Interfaces.ServicesInterfaces.Employee
{
    public interface IStatisticsService
    {
        public Task<ServiceResponseDto<GetCardsInfoDto?>> GetStatsAsync();
       
    }
}
