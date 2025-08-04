using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Interfaces.ServicesInterfaces.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers.Employee
{
    [Authorize(Roles ="Admin,Cashier")]
    [Route("api/employee/client/transactions")]
    [ApiController]
    public class TransactionsController(ITransactionsService _TransactionsService) : ControllerBase
    {

        [HttpGet("transactions-history/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetTransactionsHistory(int PageNumber, int PageSize)
        {
            if (PageNumber<=0||PageSize<=0)
                return BadRequest("Invalid pieces of information.");



            var response = await _TransactionsService.GetTransactionsHistoryPaginatedAsync(PageNumber,PageSize);

            return Ok(response.Data);
        }

        [HttpGet("transfers-history/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetTransfersHistory(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var response = await _TransactionsService.GetTransfersHistoryPaginatedAsync(PageNumber, PageSize);

            return Ok(response.Data);
        }


        [HttpPost("deposit/")]
        public async Task<IActionResult> Deposit(DepositWithdrawDto form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != Request.Headers["CSRF"])
                return Unauthorized();

            var response = await _TransactionsService.Deposit(form);
            if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Deposit process successfully.");
        }


        [HttpPost("withdraw/")]
        public async Task<IActionResult> Withdraw(DepositWithdrawDto form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != Request.Headers["CSRF"])
                return Unauthorized();

            var response = await _TransactionsService.Withdraw(form);
            if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Withdraw process successfully.");
        }


    }
}
