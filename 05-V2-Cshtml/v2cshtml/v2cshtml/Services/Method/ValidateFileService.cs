using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using v2cshtml.Models;
using v2cshtml.Services.Interface;

namespace v2cshtml.Services
{
    public class ValidateFileService : IValidateFileService
    {
        private readonly int MAX_FILE_SIZE = 1024 * 1024;
        private readonly String[] SUPPORTED_EXTENSIONS = { "CSV", "XML" };
        private readonly IUploadFileService iupload;
        public ValidateFileService(IUploadFileService _iupload)
        {
            iupload = _iupload;
        }

        public async Task<ValidateFileResponseModel> ValidateFile(IFormFile file1)
        {
            String ext = file1.FileName.Split('.').Last().ToUpper();
            ValidateFileResponseModel vfrm = ValidateFileExt(ext, file1);
            vfrm = ValidateSize(vfrm, file1);
            vfrm = ValidateCsvColumn(ext, vfrm, file1);
            String fileguid1 = Guid.NewGuid().ToString() + "." + ext;
            String uploadUri = await iupload.WriteToStorageReturnUri(fileguid1, vfrm.Success, file1, true);
            return vfrm;
        }
        public ValidateFileResponseModel ValidateFileExt(String ext, IFormFile file1)
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

        public ValidateFileResponseModel ValidateSize(ValidateFileResponseModel vfrm, IFormFile file1)
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
        public ValidateFileResponseModel ValidateCsvColumn(String ext, ValidateFileResponseModel vfrm, IFormFile file1) { 

            if(file1 != null && ext == "CSV")
            {
                try
                {
                    ValidateCsvColumnGroomData(ext, file1, 1);
                }catch(Exception e)
                {
                    vfrm.Success = false;
                    vfrm.ErrorMessage += e.Message;
                }
            }
            return vfrm;
        }
        public void ValidateCsvColumnGroomData(String ext, IFormFile file1, int dataGroomingLevel = 0)
        {
            if(dataGroomingLevel == 0)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ",",
                };
                using (var reader = new StreamReader(file1.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<CsvTransactionItem>().ToList();
                }
            }
            if(dataGroomingLevel > 0)
            {
                if (dataGroomingLevel == 1)
                {
                    using (var reader = new StreamReader(file1.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            string theLine = reader.ReadLine();
                            //replace unicode quote and comma
                            theLine = theLine.Replace("“", "\"");
                            theLine = theLine.Replace("”" , "\"");
                            theLine = theLine.Replace("，", ",");
                            //remove whitespace between quote and comma
                            theLine = Regex.Replace(theLine, "[\"][\\s]+[,]", "\",");
                            theLine = Regex.Replace(theLine, "[,][\\s]+[\"]", ",\"");

                            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                HasHeaderRecord = false,
                                Delimiter = ",",
                            };
                            using (var lineReader = new StringReader(theLine))
                            using (var csvReader = new CsvReader(lineReader, config))
                            {
                                var records = csvReader.GetRecords<CsvTransactionItem>().ToList();
                            }
                        }

                    }
                }
            }
        }

    }
}
