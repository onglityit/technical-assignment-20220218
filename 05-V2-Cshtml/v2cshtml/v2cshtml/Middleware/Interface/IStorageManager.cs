namespace v2cshtml.Middleware.Interface
{
    public interface IStorageManager
    {
        Task<string> WriteTo(string fileName, string content);
    }
}
