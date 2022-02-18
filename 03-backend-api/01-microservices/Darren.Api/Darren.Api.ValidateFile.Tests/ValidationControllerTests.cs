using Darren.Api.ValidateFile.Services.Methods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Darren.Api.ValidateFile.Tests
{
    public class ValidationControllerTests
    {
        [Fact]
        public async Task TestSupportedFormat()
        {
            String[] supportedFormats 
                = { "csv", "cSv", "cSV", "CSV", "Csv", "CsV",
                    "xml", "xmL", "xML", "XML", "XmL","Xml"};

            ValidateFileService vfs = new ValidateFileService();

            foreach(String sf in supportedFormats)
            {
                Assert.True(vfs.CheckFileExtension(sf));
            }
        }
        [Fact]
        public async Task TestUnsupportedFormat()
        {
            String[] supportedFormats 
                = { "csv2", "pdf", "txt", "jpeg", "png", "gif",
                    "pptx", "docx", "zip", "rar", "xlsx","docx"};

            ValidateFileService vfs = new ValidateFileService();

            foreach(String sf in supportedFormats)
            {
                Assert.False(vfs.CheckFileExtension(sf));
            }
        }
    }
}
