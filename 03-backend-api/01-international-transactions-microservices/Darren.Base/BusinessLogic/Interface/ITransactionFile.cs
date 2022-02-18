using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darren.Base.BusinessLogic
{
    public interface ITransactionFile
    {
        public String FileGuid { get; set; }
        public List<ITransactionFileItem> Files { get; set; }
    }
}
