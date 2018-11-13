using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DTO.FOA
{
    public class FOAAccountValidationRequestDTO
    {
        /// <summary>
        /// The TR and Branch selected by the customer.
        /// </summary>
        public string AbabrId { get; set; }
        /// <summary>
        /// The account number of the logged-in customer.
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
