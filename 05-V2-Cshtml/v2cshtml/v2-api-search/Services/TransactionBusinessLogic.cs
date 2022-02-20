using Darren.Base.Model;
using v2_api_search.Services.Interface;

namespace v2_api_search.Services
{
    public class TransactionBusinessLogic : ITransactionBusinessLogic
    {
        public async Task<TransactionResultListInfo> TransactionsByCurrency(string currencycode)
        {
            TransactionResultListInfo lsInfo = new TransactionResultListInfo();

            return lsInfo;
        }
    }
}
