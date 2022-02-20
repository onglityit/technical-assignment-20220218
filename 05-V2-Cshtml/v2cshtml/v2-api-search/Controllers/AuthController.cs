using Microsoft.AspNetCore.Mvc;

namespace v2_api_search.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("hello")]
        public IActionResult hello()
        {
            return Ok("hello123");
        }
    }
}
