using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Darren.Base.Model;
using v2_api_search.Services.Interface;

namespace v2_api_search.Controllers
{
    [Route("api/v2/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        
    public class SearchTransactionController : ControllerBase
    {
        private readonly ITransactionBusinessLogic ibl;
        public SearchTransactionController(ITransactionBusinessLogic _ibl)
        {
            ibl = _ibl;
        }

        [HttpGet("{currencycode}")]
        public async Task<IActionResult> TransactionsByCurrency([FromRoute] string currencycode)
        {
            TransactionResultListInfo lsInfo = await ibl.TransactionsByCurrency(currencycode);
            if (lsInfo != null 
                && !lsInfo.isSuccess)
            {
                return BadRequest(new
                {
                    isSuccess = lsInfo.isSuccess,
                    errorMessage = lsInfo.errorMessage,
                    data = lsInfo.lsTr
                });
            }
            return Ok(lsInfo.lsTr);
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
