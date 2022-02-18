using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using v2cshtml.Models;

namespace v2cshtml.Services
{
    public class ValidateFileService
    {
        public IFormFile file1 { get; set; }
        private readonly int MAX_FILE_SIZE = 1024 * 1024;
        private readonly String[] SUPPORTED_EXTENSIONS = { "CSV", "XML" };

        public ValidateFileResponseModel ValidateFile()
        {
            String ext = file1.FileName.Split('.').Last().ToUpper();
            ValidateFileResponseModel vfrm = ValidateFileExt(ext);
            vfrm = ValidateSize(vfrm);
            vfrm = ValidateCsvColumn(ext, vfrm);
            return vfrm;
        }
        public ValidateFileResponseModel ValidateFileExt(String ext)
        {
            ValidateFileResponseModel vfrm = new ValidateFileResponseModel()
            {
                Success = true,
                ErrorMessage = String.Empty,
            };
            if (file1 == null)
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "File is empty!\r\n";
            }
            if (file1 != null && !SUPPORTED_EXTENSIONS.Contains(ext))
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "Unknown format\r\n";
            }
            return vfrm;
        }

        public ValidateFileResponseModel ValidateSize(ValidateFileResponseModel vfrm)
        {
            if (file1 == null || file1.Length == 0)
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "File is empty!\r\n";
            }
            if (file1 != null && file1.Length > MAX_FILE_SIZE)
            {
                vfrm.Success=false;
                vfrm.ErrorMessage += "Max file size is 1MB\r\n";
            }
            return vfrm;
        }
        public ValidateFileResponseModel ValidateCsvColumn(String ext, ValidateFileResponseModel vfrm) { 
            if(file1 != null && ext == "CSV")
            {
                try
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                    };
                    using (var reader = new StreamReader(file1.OpenReadStream()))
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<CsvTransactionItem>();
                    }
                }catch(Exception e)
                {
                    vfrm.Success = false;
                    vfrm.ErrorMessage += e.Message;
                }
            }
            return vfrm;
        }
    }
}
