using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using v2cshtml.Middleware.Interface;

namespace v2cshtml.Middleware.Method
{
    public class StorageManager : IStorageManager
    {
        private readonly IConfiguration config;
        private CloudStorageAccount account;
        private CloudBlobClient cloudBlobClient;
        private CloudBlobContainer container;
        public StorageManager(IConfiguration _config)
        {
            config = _config;
            string blobStorageConnString = _config["AzureBlobStorage:ConnectionString"].ToString();
            string containerName = _config["AzureBlobStorage:ContainerName"].ToString();
            if (CloudStorageAccount.TryParse(blobStorageConnString, out account))
            {
                cloudBlobClient = account.CreateCloudBlobClient();
                container = cloudBlobClient.GetContainerReference(containerName);
            }
        }

        Task<string> IStorageManager.WriteTo(string fileName, string content)
        {
            throw new NotImplementedException();
        }
    }
}
