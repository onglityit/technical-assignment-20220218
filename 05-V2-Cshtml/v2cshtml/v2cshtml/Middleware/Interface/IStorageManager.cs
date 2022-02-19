using v2cshtml.Services;

namespace v2cshtml.Middleware.Interface
{
    public interface IStorageManager
    {
        Task<String> WriteToBlob(String fileName, String folderPath, Byte[] fileByte);
    }
}
