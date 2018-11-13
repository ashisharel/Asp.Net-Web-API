using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using System;

namespace LibertyWebAPI.BusinessServices
{
    public class OrderSubmitService : IOrderSubmitService
    {
        public readonly IOrderSubmitRepository _orderSubmitRepository;

        public OrderSubmitService(IOrderSubmitRepository orderSubmitRepository)
        {
            _orderSubmitRepository = orderSubmitRepository;
        }

        public OrderSubmitDTO OrderSubmit(int sessionId, OrderDTO orderDto)
        {
            var response = _orderSubmitRepository.SubmitOrder(sessionId, orderDto);
            if (response == null)
                return null;
            return MapDTO(response);
        }

        public OrderSubmitDTO MapDTO(OrderSubmit orderSubmit)
        {
            return new OrderSubmitDTO
            {
                ACHDelay = orderSubmit.ACHDelay,
                orderDate = orderSubmit.orderDate,
                ServiceIndicator = orderSubmit.ServiceIndicator
            };
            
        }
   }
}