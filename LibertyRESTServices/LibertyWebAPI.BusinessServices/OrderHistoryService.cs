using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class OrderHistoryService : IOrderHistoryService
    {
        public readonly IOrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryService(IOrderHistoryRepository orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository;
        }

        public IList<OrderSummaryDTO> GetOrderHistory(int sessionId, int? maxCount)
        {
            var orderHistory = _orderHistoryRepository.GetOrderHistory(sessionId, maxCount);
            if (orderHistory != null && orderHistory.Any())
            {
                return MapDTO(orderHistory);
            }
            return null;
        }

        public IList<OrderSummaryDTO> MapDTO(IEnumerable<OrderSummary> orders)
        {
            var orderSummary = orders.Select(o => new OrderSummaryDTO()
            {
                OrderId = o.OrderId,
                ProductSummary = new ProductSummaryDTO()
                {
                    Amount = new QuantityDTO() 
                    { 
                        Amount = o.Products.Amount,
                        Unit = (o.Products.ProductType == 1 || o.Products.ProductType == 9) ? "checks" : null
                    },
                    DeliveryDate = o.Products.DeliveryDate,
                    Description = o.Products.Description,
                    Id = o.Products.Id,
                    Image = o.Products.Image,
                    OrderStatus = o.Products.OrderStatus,
                    Price = new MoneyDTO() { Amount = o.Products.Price },
                    TrackingNumber = o.Products.TrackingNumber,
                    TrackingURL = o.Products.TrackingUrl,
                    HcProductId = o.Products.HcProductId,
                    ProductType = o.Products.ProductType,
                    ShippingOption = o.Products.ShippingOption,
                    PlacedOn = o.Products.OrderDate,
                    ShippedOn = o.Products.ShippedDate,
                    IsTrackable = o.Products.IsTrackable,
                    Part = o.Products.Part
                },
            });

            return orderSummary.ToList();
        }
    }
}