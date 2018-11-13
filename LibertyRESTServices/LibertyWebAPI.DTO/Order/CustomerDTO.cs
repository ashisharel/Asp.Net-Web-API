using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DTO.Order
{
    public class CustomerDTO
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public TelephoneDTO Telephone { get; set; }
        public string EmailAddress { get; set; }

    }

    public class TelephoneDTO
    {
        public string Home { get; set; }
        public string Business { get; set; }
    }


}
