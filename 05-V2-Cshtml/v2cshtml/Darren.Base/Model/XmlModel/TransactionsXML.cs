using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Darren.Base.Model.XmlModel
{
    [Serializable()]
    [XmlType("Transactions")]
    public class TransactionsXML
    {
        [XmlElement(ElementName = "Transaction")]
        public List<TransactionXML> Transaction;
    }
}
