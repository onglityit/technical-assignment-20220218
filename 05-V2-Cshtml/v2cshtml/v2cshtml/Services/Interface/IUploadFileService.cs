namespace v2cshtml.Services.Interface
{
    public interface IUploadFileService
    {
        public Task<String> WriteToStorageReturnUri(String fileName, String blobFolderPath, IFormFile file1);
    }
}
