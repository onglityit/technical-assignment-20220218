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
            using (var reader = new StreamReader("c:\\pathcsv\\file.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvIdName>().ToList();
                foreach(CsvIdName rec in records)
                {
                    
                }
            }
            return Ok();        
        }
    }
}
