using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;

namespace bankApI.Interfaces.RepositoriesInterfaces.Employee
{
    public interface IStatisticsRepository
    {
        public Task<GetCardsInfoDto> GeDashBoardStatsAsync();
       
    }
}
