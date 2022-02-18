using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darren.Base.ApiModel.Interface
{
    public interface ITransactionFileModel
    {
        public IFormFile FileUpload { get; set; }

        public String FileName { get; set; } //no extension

        public String FileExtension { get; set; }
    }
}
