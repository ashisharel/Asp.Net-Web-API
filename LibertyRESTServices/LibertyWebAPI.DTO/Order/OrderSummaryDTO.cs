using LibertyWebAPI.DTO.Common;

namespace LibertyWebAPI.DTO.Order
{
    /// <summary>
    /// Summary of a single order or line item converted into an order.
    /// </summary>
    public class OrderSummaryDTO
    {
        /// <summary>
        /// An identifier used to identify an order.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Summary of a product that was ordered.
        /// </summary>
        public ProductSummaryDTO ProductSummary { get; set; }
    }

    /// <summary>
    /// Summary of a product that was ordered.
    /// </summary>
    public class ProductSummaryDTO
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string OrderStatus { get; set; }
        public string TrackingNumber { get; set; }
        public string TrackingURL { get; set; }
        public string DeliveryDate { get; set; }
        public QuantityDTO Amount { get; set; }
        public MoneyDTO Price { get; set; }
        public string ShippingOption { get; set; }
        public string HcProductId { get; set; }
        public int? ProductType { get; set; }
        public string ShippedOn { get; set; }
        public string PlacedOn { get; set; }
        /// <summary>
        /// "true" if the order is trackable.
        /// </summary>
        public bool IsTrackable { get; set; }
        public string Part { get; set; }
    }
}