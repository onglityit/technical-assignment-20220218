using Microsoft.AspNetCore.Mvc;
using v2cshtml.Models;
using v2cshtml.Services;

namespace v2cshtml.Controllers
{
    public class ValidateFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost("ValidateFile")]
        public async Task<IActionResult> ValidateFile(IFormFile file1)
        {
            ValidateFileService vfs = new ValidateFileService()
            {
                file1 = file1,
            };
            ValidateFileResponseModel vfrm = vfs.ValidateFile();
            if (!vfrm.Success)
            {

                return BadRequest(vfrm.ErrorMessage);
            }
            return Ok("File Uploaded Successfully");
        }
    }
}
