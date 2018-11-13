using System;

namespace BusinessEntities
{
    public class PriceItemRequest
    {
        public String AbaBr_ID { get; set; }               
        public String AccountType { get; set; }                
        public String Product { get; set; }        
        public String Cover { get; set; }
        public String BillCode { get; set; }
        public String Font_ID { get; set; }
        public String Accent { get; set; }
        public String Phantom { get; set; }
        public String SigCut { get; set; }
        public String Zipcode { get; set; }
        public Int32? Quantity { get; set; }
        public Int32? Promo_Price_uid { get; set; }
        public Decimal? Override_Tax_Rate { get; set; }
        public Int32? AnnualPromoQuant { get; set; }
        public String AnnualPromoDate { get; set; }
        public String NewOrder { get; set; }
        public String Order_Source { get; set; }
        public String BillDate { get; set; }
    }
}