using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessEntities
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        /// <summary>
        /// Customer name (Pull from Billing process routine for ACH transactions)
        /// </summary>
        public string Customername { get; set; }
        /// <summary>
        /// Last order date
        /// </summary>
        public string OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public double SubTotal { get; set; } 
        public double Tax { get; set; } // Not needed for Last repriced
        public double Total { get; set; } // Not needed for Last repriced
        public string PriceMessage { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }

    public class ShippingAddress
    {
        public string ShippingLine1 { get; set; }
        public string ShippingLine2 { get; set; }
        public string ShippingLine3 { get; set; }
        public string ShippingLine4 { get; set; }
        public string ShippingLine5 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public bool? IsForeign { get; set; }

    }
}
