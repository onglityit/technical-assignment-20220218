using v2cshtml.Models;

namespace v2cshtml.Services.Interface
{
    public interface IValidateFileService
    {
        public Task<ValidateFileResponseModel> ValidateFile(IFormFile file1);
    }
}
