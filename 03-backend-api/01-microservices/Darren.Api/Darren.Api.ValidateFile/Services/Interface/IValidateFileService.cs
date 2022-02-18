using System;

namespace Darren.Api.ValidateFile.Services.Interface
{
    public interface IValidateFileService
    {
        public bool CheckFileExtension(String extension);
        public bool CheckFileSize(int fileSizeBytes);

        //public bool CheckFileMandatory(String toBeFurnished);
    }
}
