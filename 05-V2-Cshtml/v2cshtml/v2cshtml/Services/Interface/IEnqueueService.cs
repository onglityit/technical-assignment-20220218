namespace v2cshtml.Services.Interface
{
    public interface IEnqueueService
    {
        public Task EnqueueFile(string fileUri, string ext, bool isGoodFile, bool isDisabled = false);
    }
}
