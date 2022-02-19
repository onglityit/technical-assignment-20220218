namespace v2cshtml.Services.Interface
{
    public interface IUploadFileService
    {
        public Task<String> WriteToStorageReturnUri(String fileName, bool isGoodFile, IFormFile file1);
    }
}
