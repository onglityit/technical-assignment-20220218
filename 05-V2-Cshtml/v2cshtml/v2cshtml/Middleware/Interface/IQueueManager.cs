namespace v2cshtml.Middleware.Interface
{
    public interface IQueueManager
    {
        public Task<string> WriteToQueue(string queue, object jsonObject);
    }
}
