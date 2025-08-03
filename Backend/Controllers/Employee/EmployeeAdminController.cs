using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Interfaces.ServicesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace bankApI.Controllers.EmployeeController
{
    [Authorize(Roles ="Admin")]
    [Route("api/employee")]
    [ApiController]
    public class EmployeeAdminController
        (
        IEmployeeManagementService _EmployeeManagementService,
        INotificationsService _NotificationsService

        ) : ControllerBase
    {

     
        [HttpPost("{CSRF_Token}")]
        public async Task<IActionResult> AddEmployeeAsync(EmployeePersonDto employee,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();

            var response = await _EmployeeManagementService.AddNewEmployeeAsync(employee);
            if (response.Status == 400)
                return BadRequest("Request not valid.");

            else if (response.Status == 500)
                return StatusCode(500, "Internal server error");

            return Ok("Employee added successfully.");
        }


        [HttpGet("notifications/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetNotifications(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var response = await _NotificationsService.GetNotificationsAsync(int.Parse(Id),PageNumber, PageSize);
           
            return Ok(response.Data);

        }


        [HttpPut("mark-notification-as-viewed/")]
        public async Task<IActionResult> MarkAsViewed(int NotificationId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var response = await _NotificationsService.MarkNotificationAsViewedAsync(NotificationId,int.Parse(Id));

            if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Marked as viewed successfully");

        }


        [HttpGet("employee-Info/")]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var response = await _EmployeeManagementService.GetEmployeeAsync(int.Parse(Id));

            return Ok(response.Data);
        }

      
        [HttpGet("employees-Info/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetAllEmployees(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var response = await _EmployeeManagementService.GetAllEmployeesAsync(PageNumber,PageSize);

            return Ok(response.Data);
        }

        [HttpGet("login-register/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetEmployeesLoginRegister(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid pieces of information.");

            var response = await _EmployeeManagementService.LoginRegisterGetPaginatedAsync(PageNumber,PageSize);

            return Ok(response.Data);
        }


        [HttpPut("{CSRF_Token}")]
        public async Task<IActionResult> UpdateEmployeeAsync( EmployeeUpdateDto form,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();

            var response = await _EmployeeManagementService.UpdateEmployeeAsync(form);
            if (response.Status == 400)
                return BadRequest("Request not valid.");
             
            else if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Updating employee process went successfully.");

        }


        [HttpPut("freeze-account/{CSRF_Token}")]
        public async Task<IActionResult> FreezeUnfreezeEmployeeAccountAsync(SetEmailStateDto state,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();


            var response = await _EmployeeManagementService.FreezeUnfreezeEmployeeAccountAsync(state);
            if (response.Status==400)
                return BadRequest("Request not valid.");

            else if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Freezing process went successfully.");
        }

        [HttpPost("message/{CSRF_Token}")]
        public async Task<IActionResult> SendMessageToEmployee(NotificationsDto form,string CSRF_Token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Request.Cookies["CSRF"] != CSRF_Token)
                return Unauthorized();

            var Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _EmployeeManagementService.SendEmployeeMessage(form,int.Parse(Id));
            if (response.Status == 400)
                return BadRequest("Request not valid.");

            else if (response.Status == 500)
                return StatusCode(500, "Internal server error.");

            return Ok("Message sent successfully.");
        }

    }
}
