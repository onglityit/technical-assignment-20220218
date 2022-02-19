using CsvHelper.Configuration.Attributes;
using Darren.Base;

namespace v2cshtml.Services
{
    public class CsvTransactionItem : CsvTransactionItemBase
    {
        [Index(0)]
        public string TransactionId { get; set; }
        [Index(1)]
        public string Amount { get; set; }
        [Index(2)]
        public string CurrencyCode { get; set; }
        [Index(3)]

        public string TransactionDate { get; set; }
        [Index(4)]
        public string Status    { get; set; }
    }
}
