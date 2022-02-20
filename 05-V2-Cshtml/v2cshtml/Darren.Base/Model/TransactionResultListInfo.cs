using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darren.Base.Model
{
    public class TransactionResultListInfo
    {
        public List<TransactionResultModel> lsTr { get; set; }
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
    }
}
