using CsvHelper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Globalization;
using System.Linq;
using v2cshtml.Middleware.Interface;
using v2cshtml.Services;

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
            String blobStorageConnString = _config["AzureBlobStorage:ConnectionString"].ToString();
            String containerName = _config["AzureBlobStorage:ContainerName"].ToString();
            if (CloudStorageAccount.TryParse(blobStorageConnString, out account))
            {
                cloudBlobClient = account.CreateCloudBlobClient();
                container = cloudBlobClient.GetContainerReference(containerName);
            }
        }

        public async Task<String> WriteToBlob(String fileName, String folderPath, Byte[] fileByte)
        {
            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await container.SetPermissionsAsync(permissions);
            }
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(folderPath + fileName);
            await blockBlob.UploadFromByteArrayAsync(fileByte,0,fileByte.Length);
            return blockBlob.Uri.AbsoluteUri;

        }
    }
}
