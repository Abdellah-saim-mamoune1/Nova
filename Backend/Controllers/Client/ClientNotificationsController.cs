using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bankApI.Controllers.ClientController
{
    [Authorize(Roles ="Client")]
    [Route("api/client/notifications")]
    [ApiController]
    public class ClientNotificationsController(INotificationsManagementService _Notifications) : ControllerBase
    {
       

        [HttpGet("{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetClientNotifications(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var Account = User.FindFirst(ClaimTypes.Email)!.Value;
    
            var data=await _Notifications.GetAsync(Account,PageNumber,PageSize);

            return Ok(data.Data);

        }

        [HttpGet("non-read-notifications-count")]
        public async Task<IActionResult> GetClientNotificationsNonReadCount()
        {
            var Account = User.FindFirst(ClaimTypes.Email)!.Value;
            var data = await _Notifications.GetNonReadNotificationsCountAsync(Account);

            return Ok(data.Data);

        }


        [HttpPut("mark-as-viewed/{NotificationId}")]
        public async Task<IActionResult> MarkAsViewed(int NotificationId)
        {
            var Account = User.FindFirst(ClaimTypes.Email)!.Value;
            var data = await _Notifications.MarkAsViewedAsync(NotificationId, Account);

            if (data.Status == 400)
                return BadRequest("Request Not Valid.");

            else if (data.Status == 500)
                return StatusCode(500,"Internal server error.");
          
                return Ok("Marked successfully.");

        }

    }
}
