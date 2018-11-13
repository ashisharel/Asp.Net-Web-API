using LibertyWebAPI.DTO.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibertyWebAPI.DTO.Order
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            Items = new List<OrderItemDTO>();
        }

        public CustomerDTO Customer { get; set; }

        //[Required(ErrorMessage = "The Order must have items.")]
        public IList<OrderItemDTO> Items { get; set; }

        public string PlacedOn { get; set; }
        public bool? PlacedRecent { get; set; }
        public bool Subscribe { get; set; }
        public char Language { get; set; }
        public PromotionDTO Promotion { get; set; }

        //[Required(ErrorMessage="The Order must have a shipping address.")]
        public AddressDTO ShippingAddress { get; set; }

        public MoneyDTO SubTotal { get; set; }
        public MoneyDTO TotalSavings { get; set; }
        public MoneyDTO TotalShipping { get; set; }
        public MoneyDTO Tax { get; set; }
        public MoneyDTO Total { get; set; }
    }

    public class PromotionDTO
    {
        public string Code { get; set; }
        public string Text { get; set; }
    }

    //[AtLeastOnePropertyAttribute("ShipLine1", "ShipLine2", "ShipLine3", "ShipLine4", "ShipLine5", ErrorMessage = "You must supply a shipping Address.")]
    public class AddressDTO
    {
        public string ShipLine1 { get; set; }
        public string ShipLine2 { get; set; }
        public string ShipLine3 { get; set; }
        public string ShipLine4 { get; set; }
        public string ShipLine5 { get; set; }

        //[Required(ErrorMessage = "You must supply a shipping City.")]
        public string City { get; set; }

        //[Required(ErrorMessage = "You must supply a shipping State.")]
        public string State { get; set; }

        //[Required]
        public string PostalCode { get; set; }

        public bool? IsForeign { get; set; }
    }
}