using bankApI.Interfaces.ServicesInterfaces.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers.Employee
{
    [Authorize(Roles ="Admin,Cashier")]
    [Route("api/employee/bank-revenue")]
    [ApiController]
    public class BankRevenueController(IBankRevenueService _BankRevenueService) : ControllerBase
    {

        [HttpGet("{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetRevenueHistory(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var response = await _BankRevenueService.GetBankRevenuesPaginatedAsync(PageNumber, PageSize);

            return Ok(response.Data);
        }

    }
}
