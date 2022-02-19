using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using v2cshtml.Models.alpha;

namespace v2cshtml.Controllers
{
    public class AlphaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Alpha")]
        public async Task<IActionResult> Alpha(IFormFile file1)
        {
            List<CsvIdName> ret = new List<CsvIdName>();
            using (var reader = new StreamReader(file1.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                ret = csv.GetRecords<CsvIdName>().ToList();
            }
            return Ok(ret);        
        }
    }
}
