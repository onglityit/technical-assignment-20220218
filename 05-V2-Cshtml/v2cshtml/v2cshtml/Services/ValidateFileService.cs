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
            ValidateFileResponseModel vfrm = ValidateFileExt();
            vfrm = ValidateSize(vfrm);
            return vfrm;
        }
        public ValidateFileResponseModel ValidateFileExt()
        {
            ValidateFileResponseModel vfrm = new ValidateFileResponseModel()
            {
                Success = true,
                ErrorMessage = String.Empty,
            };
            String ext = file1.FileName.Split('.').Last();
            if (file1 == null)
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "File is empty!\r\n";
            }
            if (file1 != null && !SUPPORTED_EXTENSIONS.Contains(ext.ToUpper()))
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
    }
}
