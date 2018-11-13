using LibertyWebAPI.DTO.Order;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibertyWebAPI.DTO.Common
{
    public class MoneyDTO
    {
        /// <summary>
        /// The amount of money e.g. 5.55
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// This is an ISO 4217 currency code denoting the currency of the money e.g. USD
        /// </summary>
        public string Currency { get; set; }
    }

    /// <summary>
    /// A quantity that consists of an amount and a unit of measure.
    /// </summary>
    public class QuantityDTO
    {
        /// <summary>
        /// The unqualified amount e.g. 10
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The qualifier e.g. 'checks'
        /// </summary>
        public string Unit { get; set; }
    }

    /// <summary>
    /// A delivery option.
    /// </summary>
    public class ShippingOptionDTO
    {
        /// <summary>
        /// Is true if this item is bundled for shipping.
        /// </summary>
        public bool Bundled { get; set; }

        /// <summary>
        /// The shipping code e.g. 'G'
        /// </summary>
        public char Code { get; set; }

        /// <summary>
        /// Name of the shipping option e.g. "UPS Next Day Air"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the shipping option e.g. "1 Business Day"
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Additional descriptive text (for emphasis) e.g. "Recommended"
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// The estimated date of delivery. ISO 8601 date e.g. '11-21-2016'.
        /// </summary>
        public string EstimatedDelivery { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        public MoneyDTO Fee { get; set; }

        /// <summary>
        /// Is true if this item is to be preselected
        /// </summary>
        public bool IsPreselected { get; set; }
    }

    /// <summary>
    /// An accessory that a customer orders either to enhance checks, or as a cross sell item.
    /// The price of the accessory is for example 20.00 USD, for a quantity of 120 Checks.
    /// </summary>
    public class AccessoryDTO
    {
        public AccessoryDTO()
        {
            Price = new MoneyDTO();
            Quantity = new QuantityDTO();
            //ShippingOption = new ShippingOptionDTO();
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

        /// <summary>
        /// money
        /// </summary>
        public MoneyDTO Price { get; set; }

        /// <summary>
        /// A quantity that consists of an amount and a unit of measure.
        /// </summary>
        public QuantityDTO Quantity { get; set; }

        /// <summary>
        /// The maximum quantity allowed for an accessory.
        /// </summary>
        public QuantityDTO maxQuantity { get; set; }

        /// <summary>
        /// maxLength:3
        /// </summary>
        public string Monogram { get; set; }

        /// <summary>
        /// Is true if the accessory (fraudArmor) should be preselected.
        /// </summary>
        public bool Preselected { get; set; }

        /// <summary>
        /// A delivery option.
        /// </summary>
        public ShippingOptionDTO ShippingOption { get; set; }

        /// <summary>
        /// Determines whether this accessory may be removed.
        /// </summary>
        public bool Removable { get; set; }

        /// <summary>
        /// the HarlandClarke product ID
        /// </summary>
        public string HCProductId { get; set; }

        /// <summary>
        /// The personalization details that appear on a accessory.
        /// </summary>
        public PersonalizationDTO Personalization { get; set; }
    }

    public class ErrorsDTO
    {
        public IList<ErrorDTO> Errors { get; set; }
    }

    /// <summary>
    /// An error message and code.
    /// </summary>
    public class ErrorDTO
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