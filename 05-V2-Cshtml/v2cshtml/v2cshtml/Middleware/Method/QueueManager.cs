using v2cshtml.Middleware.Interface;

namespace v2cshtml.Middleware.Method
{
    public class QueueManager : IQueueManager
    {
        public async Task<string> WriteToQueue(string queue, object jsonObject)
        {
            throw new NotImplementedException();
        }
    }
}
