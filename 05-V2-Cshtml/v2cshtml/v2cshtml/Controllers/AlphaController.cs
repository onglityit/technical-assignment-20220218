using CsvHelper;
using CsvHelper.Configuration;
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
            List<ColByColTest> ret = new List<ColByColTest>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader(file1.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
                ret = csv.GetRecords<ColByColTest>().ToList();
            }
            return Ok(ret);        
        }
    }
}
