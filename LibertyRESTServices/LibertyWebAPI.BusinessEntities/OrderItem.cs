using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessEntities
{
    public class OrderItem
    {
        public OrderItem()
        {
            Personalization = new Personalization();
            Accent = new Enhancement();
            Phantom = new Enhancement();
            SigCut = new Enhancement();
            Font = new Enhancement();
            FraudArmor = new Enhancement();
            ShippingOption = new ShippingOption();
        }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int StartingCheckNumber { get; set; }
        public int CheckScenes { get; set; }
        public string SigLine1 { get; set; }
        public string SigLine2 { get; set; }
        public string ProductColor { get; set; }
        public string SoftwarePackage { get; set; }
        public Personalization Personalization { get; set; }
        public Enhancement Accent { get; set; }        
        public Enhancement Phantom { get; set; }        
        public Enhancement SigCut { get; set; }
        public Enhancement Font { get; set; }
        public string ExtraSigLine { get; set; }
        public string TitlePlateLogo { get; set; }
        public string FontLine { get; set; }
        //public ShippingAddress ShippingAddress { get; set; }
        public Enhancement FraudArmor { get; set; }
        public int Quantity { get; set; }
        //public double Price { get; set; }
        public ShippingOption ShippingOption { get; set; }
        public double ItemSubTotal { get; set; }
        public double FITotal { get; set; }
        public int PriceStatus { get; set; }
    }

    public class Personalization
    {
        public string PersonalizationLine1 { get; set; }
        public string PersonalizationLine2 { get; set; }
        public string PersonalizationLine3 { get; set; }
        public string PersonalizationLine4 { get; set; }
        public string PersonalizationLine5 { get; set; }
        public string PersonalizationLine6 { get; set; }

    }    

    public class ShippingOption
    {
        public bool? Bundled { get; set; }
        public char Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
        public double Fee { get; set; }
        public bool IsPreselected { get; set; }
    }

    public class Enhancement
    {
        /// <summary>
        /// The product code of the enhancement.
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }        
        public double Price { get; set; }
        /// <summary>
        /// Is false if phantom is mandatory for a custom product
        /// </summary>
        public bool Removable { get; set; }
        /// <summary>
        /// Ex. - Is true if the enhancement (fraudArmor) should be preselected.        
        /// </summary>
        public bool Preselected { get; set; }
        public string Url { get; set; }
    }
}
