using Azure.Storage.Queues;
using Newtonsoft.Json;
using v2cshtml.Middleware.Interface;

namespace v2cshtml.Middleware.Method
{
    public class QueueManager : IQueueManager
    {
        private readonly IConfiguration config;
        private QueueClient queueC;

        public QueueManager(IConfiguration _config)
        {
            config = _config;
        }
            
        public async Task<string> WriteToQueue(string queue, object jsonObject)
        {
            string ret = string.Empty;
            String azureQueueConnString = config["AzureBlobStorage:ConnectionString"].ToString();
            queueC = new QueueClient(azureQueueConnString, queue, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
            try
            {
                if (null != await queueC.CreateIfNotExistsAsync())
                {
                }

                await queueC.SendMessageAsync(JsonConvert.SerializeObject(jsonObject));
            }
            catch (Exception ex)
            {
                ret = "Error: " + ex.Message;
            }
            return ret;
        }
    }
}
