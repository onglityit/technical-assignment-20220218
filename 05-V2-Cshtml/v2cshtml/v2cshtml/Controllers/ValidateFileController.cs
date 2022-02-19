using Microsoft.AspNetCore.Mvc;
using v2cshtml.Models;
using v2cshtml.Services;
using v2cshtml.Services.Interface;

namespace v2cshtml.Controllers
{
    public class ValidateFileController : Controller
    {
        private IValidateFileService vfs;
        public ValidateFileController(IValidateFileService _vfs)
        {
            vfs = _vfs;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost("ValidateFile")]
        public async Task<IActionResult> ValidateFile(IFormFile file1)
        {
            ValidateFileResponseModel vfrm = await vfs.ValidateFile(file1);
            if (!vfrm.Success)
            {

                return BadRequest(vfrm.ErrorMessage);
            }
            return Ok("File Uploaded Successfully");
        }
    }
}
