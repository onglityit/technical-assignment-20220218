using Darren.Base.Model;

namespace v2_api_search.Services.Interface
{
    public interface ITransactionBusinessLogic
    {
        public Task<TransactionResultListInfo> TransactionsByCurrency(string currencycode);
    }
}
