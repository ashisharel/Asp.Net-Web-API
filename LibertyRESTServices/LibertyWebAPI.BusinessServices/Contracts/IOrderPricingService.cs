using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IOrderPricingService
    {
        PriceConfirmationDTO GetOrderPrice(OrderDTO orderRequest, int sessionId);
        //OrderDTO MapDTO(OrderDTO orderRequest, Order order);
    }
}
