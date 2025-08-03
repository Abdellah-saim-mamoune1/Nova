using bankApI.Interfaces.ServicesInterfaces.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers.EmployeeController
{
    [Authorize(Roles ="Admin,Cashier")]
    [Route("api/employee/statistics")]
    [ApiController]
    public class StatisticsController(IStatisticsService _Service) : ControllerBase
    {

        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetGetDashBoardStatsAsync()
        {
            var response = await _Service.GetStatsAsync();
            return Ok(response.Data);
        }


    }
}
