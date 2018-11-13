using LibertyWebAPI.DTO.Common;
using System;
using System.Collections.Generic;

namespace LibertyWebAPI.DTO.Order
{
    /// <summary>
    /// Order summary and error information.
    /// </summary>
    public class OrderSubmitDTO
    {
        public DateTime? orderDate { get; set; }
        public char ServiceIndicator { get; set; }
        public int ACHDelay { get; set; }
        
    }

    
}