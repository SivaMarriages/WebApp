using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Siva.Marriages.WebApp.Controllers
{
    [Route("auth/[controller]")]
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
        public IActionResult LogIn([FromBody] UserSignInfo signInfo)
        {
            var appUsername = configuration.GetValue<string>("APP_USERNAME");
            var appPassword = configuration.GetValue<string>("APP_PASSWORD");
            if (appUsername == signInfo.Username && appPassword == signInfo.Password)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWT_SECRET"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", signInfo.Username) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class UserSignInfo
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
