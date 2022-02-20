using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Darren.Base.Model.XmlModel
{
    [XmlRoot(ElementName = "Transaction")]
    public class TransactionXML
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }
}
