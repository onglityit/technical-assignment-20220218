using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace v2_api_search.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchTransactionController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{currencycode}")]

        public async Task<IActionResult> TransactionsByCurrency([FromRoute] string currencycode)
        {
            return Ok("a: " + currencycode);
        }
    }
}
