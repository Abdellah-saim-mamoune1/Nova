using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Dto_s.TokenDto_s;
using bankApI.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using bankApI.Methods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _Authenticate) : ControllerBase
    {
        
        private void CreateCookies(TokenResponseDto tokens)
        {

            Response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(50)
            });

            Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });
            var CSRF = GenerateKeys.GenerateId();
            Response.Cookies.Append("CSRF", CSRF, new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            Console.WriteLine("CSRF: " + CSRF);
        }



        [Authorize]
        [HttpDelete("DeleteCookies")]
        public IActionResult DeleteCookies()
        {

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return Ok();
        }


        [HttpPost("login")]
        public async Task<ActionResult?> Login(LoginDto login)
        {
            var result = await _Authenticate.LoginAsync(login);

            if (result.Status == 400)
                return Unauthorized("Invalid credentials.");

            else if (result.Status == 500)
                return StatusCode(500, "Internal server error.");

            CreateCookies(result.Data!);
            return Ok("Logged in successfully.");
        }



        [HttpPut("refresh-tokens/{Role}")]
        public async Task<ActionResult> RefreshTokensAsync(string Role)
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var RefreshToken))
            {
                return Unauthorized("Invalid credentials");
            }

            var data = await _Authenticate.RefreshTokensAsync(new RefreshTokenRequestDto
            {
                RefreshToken = RefreshToken,
                Role = Role
            });

            if (data.Data == null)
            {
                return Unauthorized();
            }

            CreateCookies(data.Data!);
            return Ok();

        }

    }
}