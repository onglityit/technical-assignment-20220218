using Darren.Base.Model.Auth;
using Microsoft.AspNetCore.Mvc;
using v2_api_search.Services;

namespace v2_api_search.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly JwtSettings jwtSettings;
        private readonly IEnumerable<UserCredential> tempUsers = new List<UserCredential>() { new UserCredential()
            {
                UserName = "a",
                Password = "B5c#de4f"
            } 
        };
        public AuthController(JwtSettings _jwtSettings)
        {
            jwtSettings = _jwtSettings;
        }
        [HttpPost("Login")]
        public IActionResult Login(UserCredential uc)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = tempUsers.Any(x => x.UserName.Equals(uc.UserName)
                && x.Password.Equals(uc.Password));
                if (Valid)
                {
                    Token = JwtService.GenTokenkey(new UserTokens()
                    {
                        UserName = uc.UserName
                    }, jwtSettings);
                }
                else
                {
                    return BadRequest("Your username and password does not match records in system.");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("hello")]
        public IActionResult hello()
        {
            return Ok("hello123");
        }
    }
}
