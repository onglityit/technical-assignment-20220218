using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Darren.Base.Model.XmlModel
{
    [XmlRoot(ElementName = "PaymentDetails")]
    public class PaymentDetailsXML
    {
        [XmlElement(ElementName = "Amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}
