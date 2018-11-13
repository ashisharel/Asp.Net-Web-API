using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessEntities
{
    public class OrderSubmit
    {
        public DateTime? orderDate { get; set; }
        public char ServiceIndicator { get; set; }
        public int ACHDelay { get; set; }
        public char Success { get; set; }
    }
}
