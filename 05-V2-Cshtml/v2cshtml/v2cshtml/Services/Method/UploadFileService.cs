﻿using Darren.Base.Model;
using v2cshtml.Middleware.Interface;
using v2cshtml.Middleware.Method;
using v2cshtml.Services.Interface;

namespace v2cshtml.Services.Method
{
    public class UploadFileService : IUploadFileService
    {
        public String FileExtension { get; set; }
        private readonly IStorageManager iStorage;
        private readonly String BAD_FILE_FOLDER = ConstValues.BAD_FILE_FOLDER;
        private readonly String GOOD_FILE_FOLDER = ConstValues.GOOD_FILE_FOLDER;

        public UploadFileService(IStorageManager _iStorage)
        {
            iStorage = _iStorage;
        }

        public async Task<String> WriteToStorageReturnUri(String fileName, bool isGoodFile, IFormFile file1, bool isDisabled = false)
        {
            String blobFolderPath = isGoodFile ? GOOD_FILE_FOLDER : BAD_FILE_FOLDER;
            String returnUri = String.Empty;
            if (!isDisabled)
            {
                using (var ms = new MemoryStream())
                {
                    file1.CopyTo(ms);
                    byte[] fileByte = ms.ToArray();
                    returnUri = await iStorage.WriteToBlob(fileName,
                                                           blobFolderPath,
                                                           fileByte);
                }
            }
            return returnUri;
        }
        public async Task<String> WriteToStorageReturnUri(String fileName, bool isGoodFile, byte[] fileByte, bool isDisabled = false)
        {
            String blobFolderPath = isGoodFile ? GOOD_FILE_FOLDER : BAD_FILE_FOLDER;
            String returnUri = String.Empty;
            if (!isDisabled)
            {
                returnUri = await iStorage.WriteToBlob(fileName,
                                                        blobFolderPath,
                                                        fileByte);
            }
            return returnUri;
        }
    }
}
