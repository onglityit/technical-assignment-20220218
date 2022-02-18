using v2cshtml.Models;

namespace v2cshtml.Services
{
    public class ValidateFileService
    {
        public IFormFile file1 { get; set; }
        private readonly String[] SUPPORTED_EXTENSIONS = { "CSV", "XML" };

        public ValidateFileResponseModel ValidateFile()
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
            }else if (!SUPPORTED_EXTENSIONS.Contains(ext.ToUpper()))
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "Unknown format\r\n";
            }
            return vfrm;
        }
    }
}
