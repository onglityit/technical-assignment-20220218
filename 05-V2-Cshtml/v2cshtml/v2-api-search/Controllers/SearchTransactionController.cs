using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace v2_api_search.Controllers
{
    [Route("api/v2/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        
    public class SearchTransactionController : ControllerBase
    {
        [HttpGet("{currencycode}")]

        public async Task<IActionResult> TransactionsByCurrency([FromRoute] string currencycode)
        {
            return Ok("a: " + currencycode);
        }
    }
}
