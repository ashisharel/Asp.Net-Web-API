using System;

namespace BusinessEntities
{
    public class PriceItem
    {
        public Int32 Price_Status { get; set; }
        public Int32 Cust_Base { get; set; }
        public Int32 Bank_Base { get; set; }
        public Int32 Cust_del { get; set; }
        public Int32 Bank_Del { get; set; }
        public Int32 Cust_Tax { get; set; }
        public Int32 Bank_tax { get; set; }
        public String Check_Price { get; set; }
        public String Cover_Price { get; set; }
        public String Font_Price { get; set; }
        public String Accent_Price { get; set; }
        public String Phantom_Price { get; set; }
        public String SigCut_Price { get; set; }

        public String Font_Free_Flag { get; set; }
        public String Accent_Free_Flag { get; set; }
        public String Phantom_Free_Flag { get; set; }
        public String Actual_Billcode { get; set; }
    }
}