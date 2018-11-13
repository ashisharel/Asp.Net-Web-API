using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Order;
using System.Collections.Generic;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    /// <summary>
    /// Service contract for Order History
    /// </summary>
    public interface IOrderHistoryService
    {
        /// <summary>
        /// Summarized information about an order.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        IList<OrderSummaryDTO> GetOrderHistory(int sessionId, int? maxCount);

        /// <summary>
        /// Map the Order Summary entity to the Order Summary DTO
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        IList<OrderSummaryDTO> MapDTO(IEnumerable<OrderSummary> orders);
    }
}