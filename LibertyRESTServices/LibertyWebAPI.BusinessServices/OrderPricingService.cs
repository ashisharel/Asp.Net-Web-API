using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices
{
    public class OrderPricingService : IOrderPricingService
    {
        public readonly IOrderPricingRepository _orderPricingRepository;
        public OrderPricingService(IOrderPricingRepository orderPricingRepository)
        {
            _orderPricingRepository = orderPricingRepository;
        }
        public PriceConfirmationDTO GetOrderPrice(OrderDTO orderRequest, int sessionId)
        {
            var objPriceConfirmation = new PriceConfirmationDTO();
            objPriceConfirmation.Errors = _orderPricingRepository.GetOrderPrice(orderRequest, sessionId);
            objPriceConfirmation.Order = orderRequest;
            return objPriceConfirmation;
            
        }

        
    }
}
