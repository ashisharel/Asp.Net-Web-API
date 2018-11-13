namespace LibertyWebAPI.BusinessEntities
{
    /// <summary>
    /// An accessory that a customer orders either to enhance checks, or as a cross sell item.
    /// The price of the accessory is for example 20.00 USD, for a quantity of 120 Checks.
    /// </summary>
    public class Accessory
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Accessory()
        {
            ShippingOption = new ShippingOption();
        }
        /// <summary>
        /// The name of the accessory.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The product code of the accessory.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// A URL to an image representing the accessory.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// A code that describes the type of the product.
        /// </summary>
        public int? Type { get; set; }
        ///// <summary>
        ///// money
        ///// </summary>
        //public Money price { get; set; }
        /// <summary>
        /// The amount of money e.g. 5.55
        /// </summary>
        public double Amount { get; set; }
        ///// <summary>
        ///// The maximum quantity allowed for an accessory.
        ///// </summary>
        public int maxQuantity { get; set; }
        /// <summary>
        /// The quantity e.g. 100, 5
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The qualifier e.g. 'checks', 'boxes'
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// maxLength:3
        /// </summary>
        public string Monogram { get; set; }
        /// <summary>
        /// Is true if the accessory should be preselected.
        /// </summary>
        public string Preselected { get; set; }
        /// <summary>
        /// A delivery option.
        /// public bool? Bundled { get; set; }
        /// public string Code { get; set; }
        /// public string Name { get; set; }
        /// public string Description { get; set; }
        /// public string Note { get; set; }
        /// public DateTime? EstimatedDelivery { get; set; }
        /// public double Fee { get; set; }
        /// </summary>
        public ShippingOption ShippingOption { get; set; }
        /// <summary>
        /// Determines whether this accessory may be removed.
        /// </summary>
        public string Removable { get; set; }

        /// <summary>
        /// the Harland Clarke product Id
        /// </summary>
        public string HCProductId { get; set; }
    }
}