using Microsoft.AspNetCore.Mvc;

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
            return Ok("File Uploaded Successfully");
        }
    }
}
