﻿using Darren.Base.Model.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using v2_api_search.Services;

namespace v2_api_search.Controllers
{

    [Route("api/v2/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly JwtSettings jwtSettings;
        private readonly IEnumerable<UserCredential> tempUsers = new List<UserCredential>() { new UserCredential()
            {
                UserName = "a",
                Password = "b"
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
    }
}
