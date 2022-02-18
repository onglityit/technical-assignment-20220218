using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darren.Base.BusinessLogic
{
    public interface ITransactionFileItem
    {
        public String FileItemGuid { get; set; }
        public String FileName { get; set; }
        public String FileType { get; set; }
    }
}
