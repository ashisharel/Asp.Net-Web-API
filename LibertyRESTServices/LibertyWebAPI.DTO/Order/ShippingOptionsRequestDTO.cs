using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DTO.Order
{
    public class ShippingOptionsRequestDTO
    {
        [Required]
        public string ProductID { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string ZipCode { get; set; }        
        public string ShippingLine1 { get; set; }        
        public string ShippingLine2 { get; set; }        
        public string ShippingLine3 { get; set; }        
        public string ShippingLine4 { get; set; }        
        public string ShippingLine5 { get; set; }        
        [Required]      
        public bool? IsForeign { get; set; }
        public bool? IsPOBox { get; set; }
    }
}
