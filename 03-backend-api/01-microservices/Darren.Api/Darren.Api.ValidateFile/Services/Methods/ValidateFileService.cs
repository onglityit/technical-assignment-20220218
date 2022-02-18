using Darren.Api.ValidateFile.Services.Interface;
using System;
using System.Linq;

namespace Darren.Api.ValidateFile.Services.Methods
{
    public class ValidateFileService : IValidateFileService
    {
        private readonly String[] SUPPORT_FORMAT = { "XML", "CSV" }; 
        public bool CheckFileExtension(String extension)
        {
            if(!SUPPORT_FORMAT.Contains(extension.ToUpper())) return false;
            return true;
        }
        public bool CheckFileSize(int fileSizeBytes)
        {
            return false;
        }
    }
}
