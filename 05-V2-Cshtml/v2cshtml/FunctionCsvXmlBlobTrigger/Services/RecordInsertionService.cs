using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCsvXmlBlobTrigger.Services
{
    public class RecordInsertionService
    {
        private readonly IConfigurationRoot config;
        public RecordInsertionService(IConfigurationRoot _config)
        {
            config = _config;
        }
    }
}
