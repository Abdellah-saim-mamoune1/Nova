using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.Employee;

namespace bankApI.Services.EmployeeServices
{
    public class StatisticsService
        (

        IStatisticsRepository _StatisticsRepository

        ) : IStatisticsService
    {

        public async Task<ServiceResponseDto<GetCardsInfoDto?>> GetStatsAsync()
        {

            var data=await _StatisticsRepository.GeDashBoardStatsAsync();
     
            return new ServiceResponseDto<GetCardsInfoDto?> { Status=200,Data=data };
        }
     


    }
}
