using LibertyWebAPI.DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace LibertyWebAPI.DTO.Order
{
    //[AtLeastOneProperty("Check", "Accessory", ErrorMessage = "You must provide a Check or Accessory item.")]
    public class OrderItemDTO
    {
        public OrderItemDTO()
        {
        }

        public string Id { get; set; }
        public CheckDTO Check { get; set; }
        public AccessoryDTO Accessory { get; set; }
        public AccessoryDTO Upsell { get; set; }
        public string PriceMessage { get; set; }
        //public MoneyDTO ItemShipping { get; set; }
        public MoneyDTO ItemTax { get; set; }
        public MoneyDTO ItemSubtotal { get; set; } // item price + ship
        public string ActualBillCode { get; set; }
        public MoneyDTO ItemTotal { get; set; } // item price + ship + tax
        public MoneyDTO FITotal { get; set; }
    }

    public class CheckDTO
    {
        /// <summary>
        /// constructor
        /// </summary>
        public CheckDTO() { }
        public string ProductId { get; set; }
        public int? StartAt { get; set; }
        public string[] OverSignature { get; set; }
        public string TitlePlateLogo { get; set; }
        public string Color { get; set; }
        public string SoftwarePackage { get; set; }
        public PersonalizationDTO Personalization { get; set; }
        public AccessoryDTO Accent { get; set; }
        public AccessoryDTO Font { get; set; }
        public AccessoryDTO OneLiner { get; set; }
        public AccessoryDTO Background { get; set; }
        public AccessoryDTO Apron { get; set; }
        public AccessoryDTO FraudArmor { get; set; }
        public QuantityDTO Quantity { get; set; }
        public MoneyDTO Price { get; set; }
        public ShippingOptionDTO ShippingOption { get; set; }
    }

    public class PersonalizationDTO
    {
        public PersonalizationLineDTO PersLine1 { get; set; }
        public PersonalizationLineDTO PersLine2 { get; set; }
        public PersonalizationLineDTO PersLine3 { get; set; }
        public PersonalizationLineDTO PersLine4 { get; set; }
        public PersonalizationLineDTO PersLine5 { get; set; }
        public PersonalizationLineDTO PersLine6 { get; set; }
    }

    public class PersonalizationLineDTO
    {
        public string Text { get; set; }
        public bool? IsBold { get; set; }
    }
}