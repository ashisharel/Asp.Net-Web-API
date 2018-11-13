using System.Collections.Generic;

namespace LibertyWebAPI.BusinessEntities
{
    public class AccessoryCategory
    {
        /// <summary>
        /// constructor
        /// </summary>
        public AccessoryCategory()
        {
            Groups = new List<AccessoryCategory>();
            Items = new List<AccessoryDetails>();
        }
        /// <summary>
        /// The product code of the accessory.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The name of the accessory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A set of AccessoryCategory, if this category has sub-categories.
        /// </summary>
        public IList<AccessoryCategory> Groups { get; set; }

        /// <summary>
        /// A set of AccessoryDetails, if this category has accessories (accents or one-liners)
        /// </summary>
        public IList<AccessoryDetails> Items { get; set; }
    }

    /// <summary>
    /// //Details about an accessory that a customer may order either to enhance checks, or as a cross sell item. It includes available quantities.
    /// </summary>
    public class AccessoryDetails
    {
        /// <summary>
        /// constructor
        /// </summary>
        public AccessoryDetails()
        {
            Pricing = new List<PricingOption>();
        }

        /// <summary>
        /// The name of the accessory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The product code of the accessory.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A URL to an image representing the accessory.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An option and price e.g. 120 Checks @ USD 22.50
        /// </summary>
        public IList<PricingOption> Pricing { get; set; }

        /// <summary>
        /// The type of the accessory. Only applies to AccentSymbols.
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// whether accessory is selected or not
        /// </summary>
        public bool Selected { get; set; }
    }
}