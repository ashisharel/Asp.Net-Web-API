using LibertyWebAPI.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DTO.Order
{
    public class PriceConfirmationDTO
    {
        public PriceConfirmationDTO()
        {
            Errors = new ErrorsDTO();
        }
        public OrderDTO Order { get; set; }
        public ErrorsDTO Errors { get; set; }
    }
}
