using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bankApI.Controllers.ClientController
{
    [Authorize(Roles ="Client")]
    [Route("api/client/transactions")]
    [ApiController]
    public class TransactionsManagementController(ITransactionsManagementService _Service) : ControllerBase
    {

        [HttpGet("transactions-history/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetTransactionHistory(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var Account = User.FindFirst(ClaimTypes.Email)!.Value;
            var TransactionsHistory = await _Service.GetTransactionsAsync(Account,PageNumber, PageSize);

            return Ok(TransactionsHistory.Data);
        }


        [HttpGet("transfers-history/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetTransfersHistory(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var Account = User.FindFirst(ClaimTypes.Email)!.Value;
            var TransactionsHistory = await _Service.GetTransfersAsync(Account, PageNumber, PageSize);

            return Ok(TransactionsHistory.Data);
        }


        [HttpGet("last-month-transactions-history")]
        public async Task<IActionResult> GetLastMonthTransactionHistory()
        {

            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var TransactionsHistory = await _Service.GetLastMonthTransactionsAsync(int.Parse(Id));

            return Ok(TransactionsHistory.Data);
        }


        [HttpPut("transfer-fund/")]
        async public Task<IActionResult> TransferFundAsync(TransferFundSetDto form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Account = User.FindFirst(ClaimTypes.Email)!.Value;
            var result = await _Service.TransferFundAsync(form,Account);
            if (result.Status == 400)
            {
                return BadRequest("Request Not Valid.");
            }

            else if (result.Status == 500)
                return StatusCode(500, "Internal server error.");

            else
                return Ok("Transaction went successfully.");
        }
    }
}
