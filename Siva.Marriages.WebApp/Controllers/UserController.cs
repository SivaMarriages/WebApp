using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Siva.Marriages.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("isauthenticated")]
        public bool IsAuthenticated()
        {
            return HttpContext.User.Identity?.IsAuthenticated ?? false;
        }

        [HttpGet("getUserName")]
        public IActionResult GetUserName()
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Ok(new { name = HttpContext.User.Identity.Name ?? string.Empty });
            }
            return Ok(new { name = string.Empty });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserSignInfo signInfo)
        {
            var appUsername = configuration.GetValue<string>("APP_USERNAME");
            var appPassword = configuration.GetValue<string>("APP_PASSWORD");
            if (appUsername == signInfo.Username && appPassword == signInfo.Password)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, signInfo.Username),
                    new Claim(ClaimTypes.Role, "Administrator")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = signInfo.Remember,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }

    public class UserSignInfo
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Remember { get; set; }
    }
}
