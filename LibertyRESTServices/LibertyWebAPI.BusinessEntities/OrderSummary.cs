using System.Collections.Generic;

namespace LibertyWebAPI.BusinessEntities
{
    ///// <summary>
    ///// Order summary and error information.
    ///// </summary>
    //public class Confirmation
    //{
    //    /// <summary>
    //    /// Summary of a single order or line item converted into an order.
    //    /// </summary>
    //    public OrderSummary Summary { get; set; }

    //    /// <summary>
    //    /// A list of error messages.
    //    /// </summary>
    //    public IList<Error> Errors { get; set; }
    //}

    /// <summary>
    /// Summary of a single order or line item converted into an order.
    /// </summary>
    public class OrderSummary
    {
        public OrderSummary()
        {
            Products = new ProductSummary();
        }
        /// <summary>
        /// An identifier used to identify an order.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Summary of a product that was ordered.
        /// </summary>
        public ProductSummary Products { get; set; }
    }

    /// <summary>
    /// Summary of a product that was ordered.
    /// </summary>
    public class ProductSummary
    {
        public string Id { get; set; }

        /// <summary>
        /// The image of the product.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The order status of this historical order.
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// The tracking number of this historical order.
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// The tracking url to the shipping company of this historical order.
        /// </summary>
        public string TrackingUrl { get; set; }

        /// <summary>
        /// The delivery date of the order.
        /// </summary>
        public string DeliveryDate { get; set; }

        /// <summary>
        /// A quantity that consists of an amount and a unit of measure.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Price { get; set; }

        public string ShippingOption { get; set; }

        public string HcProductId { get; set; }

        /// <summary>
        /// Type of product. e.g. 'Duplicate' or 'Cover'
        /// </summary>
        public int? ProductType { get; set; }

        /// <summary>
        /// The order date of the order.
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// The shipped date of the order.
        /// </summary>
        public string ShippedDate { get; set; }

        /// <summary>
        /// "true" if the order is trackable.
        /// </summary>
        public bool IsTrackable { get; set; }

        /// <summary>
        /// Check style - Single/Duplicate etc.
        /// </summary>
        public string Part { get; set; }
    }

    /// <summary>
    /// An error message and code.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// The text message describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A code that can be used to lookup alternative text e.g. language, mobile friendly.
        /// </summary>
        public string Code { get; set; }
    }
}