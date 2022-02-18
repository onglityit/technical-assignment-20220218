using Darren.Api.ValidateFile.Services.Interface;
using Darren.Model.ApiModel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Darren.Api.ValidateFile.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly IValidateFileService _iValidateFileService;
        public ValidationController(IValidateFileService iValidateFileService)
        {
            _iValidateFileService = iValidateFileService;
        }
        [HttpPost("validateFile")]
        public async Task<ActionResult> ValidateFile([FromForm] FileForValidate UploadedFile)
        {
            String returnValidationMessage = String.Empty;
            if (!_iValidateFileService.CheckFileExtension(UploadedFile.FileExtension))
            {
                returnValidationMessage += "Unknown Format\r\n";
            }
            if (!String.IsNullOrEmpty(returnValidationMessage))
            {
                return BadRequest(returnValidationMessage);
            }
            return Ok();
        }
    }
}
