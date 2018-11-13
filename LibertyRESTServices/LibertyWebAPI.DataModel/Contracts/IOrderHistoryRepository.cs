using LibertyWebAPI.BusinessEntities;
using System.Collections.Generic;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IOrderHistoryRepository
    {
        IEnumerable<OrderSummary> GetOrderHistory(int sessionId, int? maxCount);
    }
}