using Newtonsoft.Json;
using v2cshtml.Middleware.Interface;
using v2cshtml.Services.Interface;

namespace v2cshtml.Services.Method
{
    public class EnqueueService : IEnqueueService
    {
        private readonly IQueueManager iqm;
        private readonly string 
            CSV_BAD_FILE = "csv-bad-file", CSV_GOOD_FILE = "csv-good-file",
            XML_BAD_FILE = "xml-bad-file", XML_GOOD_FILE = "xml-good-file";
        public EnqueueService(IQueueManager _iqm)
        {
            iqm = _iqm;
        }
        public async Task<bool> EnqueueFile(string fileUri, string oriFilename, string ext, bool isGoodFile, bool isDisabled)
        {
            string queueName = string.Empty;
            bool isQueueSuccess = false;
            if(ext.ToUpper() == "CSV")
            {
                if (isGoodFile) queueName = CSV_GOOD_FILE;
                else queueName = CSV_BAD_FILE;
            }
            else if (ext.ToUpper() == "XML")
            {
                if (isGoodFile) queueName= XML_GOOD_FILE;
                else queueName= XML_BAD_FILE;
            }
            try
            {
                iqm.WriteToQueue(queueName, new
                {
                    fileUri = fileUri,
                    oriFilename = oriFilename,
                });
                isQueueSuccess = true;
            }
            catch(Exception ex)
            {
                isQueueSuccess = false;
            }
            return isQueueSuccess;
        }
    }
}
