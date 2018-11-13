using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Order;
using System;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    /// <summary>
    /// Service contract for OrderSubmit
    /// </summary>
    public interface IOrderSubmitService
    {
        /// <summary>
        /// Submits an order in Liberty
        /// </summary>
        /// <param name="sessionId">the session Id</param>
        /// <param name="order">the order to submit</param>
        /// <returns></returns>
        OrderSubmitDTO OrderSubmit(int sessionId, OrderDTO order);

        /// <summary>
        /// Map the order entity to the order DTO
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        OrderSubmitDTO MapDTO(OrderSubmit order);
    }
}