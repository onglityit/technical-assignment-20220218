namespace Darren.Base
{
    public class CsvTransactionItemBase
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }
    }
}