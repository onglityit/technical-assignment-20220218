using Darren.Model.ApiModel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Darren.Api.ValidateFile.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        [HttpPost("validateFile")]
        public async Task<ActionResult> ValidateFile([FromForm] FileForValidate UploadedFile)
        {

            return Ok();
        }
    }
}
