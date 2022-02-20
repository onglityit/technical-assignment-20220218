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
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "TransactionDate")]
        public string TransactionDate { get; set; }
        [XmlElement(ElementName = "PaymentDetails")]
        public PaymentDetailsXML PaymentDetails { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
    }
}
