using bankApI.BusinessLayer.Dto_s;
using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;
using bankApI.Interfaces.ServicesInterfaces.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bankApI.Controllers.ClientController
{
    [Authorize(Roles ="Client")]
    [Route("api/client/manage")]
    [ApiController]
    public class ClientManagementController
        (
        IClientManagementService _ClientManagementService,
        IClientInfoGetService _ClientInfoGetService


        ) : ControllerBase
    {

        [HttpPut]
        public async Task<IActionResult> UpdateClientByIdAsync( UpdateClientDto client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var response = await _ClientManagementService.UpdateAsync(client,int.Parse(Id));
            if (response != null)
            {
                return Ok("Client updated successfully.");
            }

            return BadRequest("Request not valid.");

        }


        [HttpPost("add-get-help-request/")]
        async public Task<IActionResult> AddGetHelpRequestAsync(NotificationsDto form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _ClientManagementService.AddGetHelpRequestAsync(form);

            if (response.Status==200)
                return Ok("Request added successfully.");

            return BadRequest("Request not valid.");
        }


        [HttpGet("client-info/")]
        public async Task<IActionResult> GetClientInfo()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var ClientInfo = await _ClientInfoGetService.GetInfoAsync(int.Parse(Id));


            return Ok(ClientInfo.Data);
        }

        [HttpGet("accounts/")]
        public async Task<IActionResult> GetClientAccounts()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var ClientInfo = await _ClientInfoGetService.GetAccountsAsync(int.Parse(Id));


            return Ok(ClientInfo.Data);
        }

       


    }
}