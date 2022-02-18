using CsvHelper.Configuration.Attributes;

namespace v2cshtml.Services
{
    public class CsvTransactionItem
    {
        [Index(0)]
        public String TransactionId { get; set; }
        [Index(1)]
        public String Amount { get; set; }
        [Index(2)]
        public String CurrencyCode { get; set; }
        [Index(3)]

        public String TransactionDate { get; set; }
        [Index(4)]
        public String Status    { get; set; }
    }
}
