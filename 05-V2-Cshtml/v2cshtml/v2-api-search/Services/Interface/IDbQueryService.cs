using Darren.Base.Model;

namespace v2_api_search.Services.Interface
{
    public interface IDbQueryService
    {
        public Task<TransactionResultListInfo> sp_transactionrecord_query(
               string querymode, string value01, string value02
               );
    }
}
