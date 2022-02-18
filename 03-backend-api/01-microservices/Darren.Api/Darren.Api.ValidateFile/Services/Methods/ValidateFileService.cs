using Darren.Api.ValidateFile.Services.Interface;
using System;

namespace Darren.Api.ValidateFile.Services.Methods
{
    public class ValidateFileService : IValidateFileService
    {
        public bool CheckFileExtension(String extension)
        {
            return false;
        }
        public bool CheckFileSize(int fileSizeBytes)
        {
            return false;
        }
    }
}
