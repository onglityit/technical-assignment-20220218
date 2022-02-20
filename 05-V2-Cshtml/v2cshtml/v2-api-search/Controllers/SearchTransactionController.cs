using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Darren.Base.Model;

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
            List<TransactionResultModel> lsTr = new List<TransactionResultModel>();
            return Ok("a: " + currencycode);
        }

        [HttpGet]
        public async Task<IActionResult> TransactionsByDateRange([FromQuery] string dateFrom_yyyyMMddTHHmmss,
            [FromQuery] string dateTo_yyyyMMddTHHmmss)
        {
            List<TransactionResultModel> lsTr = new List<TransactionResultModel>();
            return Ok("a: " + dateFrom_yyyyMMddTHHmmss + " to " + dateTo_yyyyMMddTHHmmss);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> TransactionsByStatus([FromRoute] string status)
        {
            List<TransactionResultModel> lsTr = new List<TransactionResultModel>();
            return Ok("a: " + status);
        }

    }
}
