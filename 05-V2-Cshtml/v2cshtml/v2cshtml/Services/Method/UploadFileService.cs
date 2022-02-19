using v2cshtml.Middleware.Interface;
using v2cshtml.Middleware.Method;
using v2cshtml.Services.Interface;

namespace v2cshtml.Services.Method
{
    public class UploadFileService : IUploadFileService
    {
        public String FileExtension { get; set; }
        private readonly IStorageManager iStorage;

        public UploadFileService(StorageManager _iStorage)
        {
            iStorage = _iStorage;
        }

        public async Task<String> WriteToStorageReturnUri(String fileName, String blobFolderPath, IFormFile file1)
        {
            String returnUri = String.Empty;
            using (var ms = new MemoryStream())
            {
                file1.CopyTo(ms);
                byte[] fileByte = ms.ToArray();
                returnUri = await iStorage.WriteToBlob(fileName,
                                                       blobFolderPath,
                                                       fileByte);
            }
            return returnUri;
        }
    }
}
