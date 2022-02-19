namespace v2cshtml.Services.Interface
{
    public interface IEnqueueService
    {
        public Task<bool> EnqueueFile(string fileUri, string oriFilename, string ext, bool isGoodFile, bool isDisabled = false);
    }
}
