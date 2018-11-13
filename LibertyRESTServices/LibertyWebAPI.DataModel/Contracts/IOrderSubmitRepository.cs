using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Order;
using System;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IOrderSubmitRepository
    {
        //IList<Confirmation> OrderSubmit(int sessionId, Order order);
        OrderSubmit SubmitOrder(int sessionId, OrderDTO order);
    }
}