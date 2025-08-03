using bankApI.BusinessLayer.Dto_s;
using bankApI.Interfaces.ServicesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationsDto = bankApI.BusinessLayer.Dto_s.NotificationsDto;

namespace bankApI.Controllers.EmployeeController
{
    [Authorize(Roles="Admin")]
    [Route("api/employee/client")]
    [ApiController]
    public class ClientAdminController(
        
         IClientAdminService _ClientAdminService,
         IClientInfoGetService _ClientInfoGetService
        ) : ControllerBase
    {

        [HttpPost("{CSRF_Token}")]
        public async Task<IActionResult> AddNewClientAsync( [FromBody] PersonClientSetDto form,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();


            var response = await _ClientAdminService.AddNewClientAsync(form);
            if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok(response.Data); 
        }

       
      
        [HttpGet("clients/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetAllClientsAsync(int PageNumber,int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var response = await _ClientAdminService.GetAllClientsAsync(PageNumber,PageSize);
 
            return Ok(response.Data);
        }



        [HttpGet("client/{Id}")]
        public async Task<IActionResult> GetClientInfo(int Id)
        {
            var ClientInfo = await _ClientInfoGetService.GetInfoAsync(Id);

            return Ok(ClientInfo.Data);
        }

        [HttpGet("accounts/{Id}")]
        public async Task<IActionResult> GetClientAccounts(int Id)
        {
            
            var ClientInfo = await _ClientInfoGetService.GetAccountsAsync(Id);


            return Ok(ClientInfo.Data);
        }


       
        [HttpPost("account/{CSRF_Token}")]
        public async Task<IActionResult> AddNewAccountAsync([FromBody] BankEmailDto form,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();


            var response = await _ClientAdminService.AddNewAccountAsync(form);
            if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Client account added successfully.");


        }

        [HttpPost("notification/{CSRF_Token}")]
        async public Task<IActionResult> AddClientNotification([FromBody] NotificationsDto form,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();

            var response = await _ClientAdminService.SendClientAccountMessage(form);
            if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Notification added successfully."); 
        }

        [HttpPut("freeze-account/{CSRF_Token}")]
        public async Task<IActionResult> FreezeUnfreezeClientAccountAsync([FromBody] SetEmailStateDto form,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();

            var response = await _ClientAdminService.FreezeClientAccountAsync(form);

            if (response.Status==500)
                return StatusCode(500, "Internal server error.");

            return Ok("freezing process successfully.");
        }

     

    }
}
