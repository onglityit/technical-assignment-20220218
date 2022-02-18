namespace v2cshtml.Services
{
    public class CsvTransactionItem
    {
        public String TransactionId { get; set; }
        public String Amount { get; set; }
        public String CurrencyCode { get; set; }

        public String TransactionDate { get; set; }
        public String Status    { get; set; }
    }
}
