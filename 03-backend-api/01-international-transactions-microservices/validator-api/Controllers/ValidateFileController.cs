using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace validator_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValidateFileController : ControllerBase
    {
        public async Task<ActionResult> ValidateFile()
        {
            return Ok();
        }
    }
}
