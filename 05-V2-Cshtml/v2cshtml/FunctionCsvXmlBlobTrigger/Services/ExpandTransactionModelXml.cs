using Darren.Base.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FunctionCsvXmlBlobTrigger.Services
{
    public class ExpandTransactionModelXml
    {
        public async Task<List<TransactionXML>> GetXmlArray(Stream strm)
        { 
            List<TransactionXML> ret = new List<TransactionXML>();
            var result = new StringBuilder();
            using (var reader = new StreamReader(strm))
            {
                while (reader.Peek() >= 0)
                {
                    string theLine = reader.ReadLine();
                    result.AppendLine(theLine);
                }
            }
            string wholeXml = result.ToString();

            if (string.IsNullOrWhiteSpace(wholeXml))
            {
                throw new Exception("Wrong xml file format. ");
            }
            else
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Transactions";
                xRoot.IsNullable = true;

                var serializer = new XmlSerializer(typeof(TransactionsXML), xRoot);


                StringReader reader = new StringReader(wholeXml);
                using (XmlReader xmlReader = XmlReader.Create(reader))
                {
                    TransactionsXML resultObj = (TransactionsXML)serializer.Deserialize(xmlReader);
                    if (resultObj != null)
                    {
                        ret = resultObj.Transaction;
                    }
                }
            }
            return ret;
        }
    }
}
