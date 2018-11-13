using System.Collections.Generic;

namespace LibertyWebAPI.BusinessEntities
{
    /// <summary>
    /// /// Details of a product in the Liberty product catalog.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Product()
        {
            QuantityPrice = new List<PricingOption>();
            DistinctiveLettering = new List<DistinctiveLettering>();
            RelatedStyles = new List<RelatedStyle>();
            SoftwarePackages = new List<SoftwarePackage>();
        }

        /// <summary>
        /// Unique identifier used to identify a product
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// A short description of the product.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The style of binding used on the checks e.g. duplicate.
        /// </summary>
        public string Binding { get; set; }

        /// <summary>
        /// The color style of the checks.
        /// </summary>
        public string ColorDisp { get; set; }

        /// <summary>
        /// The color ID of the checks.
        /// </summary>
        public string ProductColor { get; set; }

        /// <summary>
        ///   indicates if color selection is required.
        /// </summary>
        public string ColorFlag { get; set; }
        
        /// <summary>
        /// indicates if Software selection is required
        /// </summary>
        public string SoftwareFlag { get; set; }

        /// <summary>
        /// The 'part' style of the checks.
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// The type of product e.g. Check
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// The Image URL of product
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The number of personalization lines available. (minimum:1)
        /// </summary>
        public string MaxPersonalizationLines { get; set; }

        /// <summary>
        /// The URI to the license image.
        /// </summary>
        public string LicenseUri { get; set; }

        /// <summary>
        /// The text for the additional license info.
        /// </summary>
        public string LicenseText { get; set; }

        /// <summary>
        /// Indicates whether the check product is a business product or a personal product.
        /// </summary>
        public string IsBusinessProduct { get; set; }

        /// <summary>
        /// A set of default, recommended, or all accent symbols for a product.
        /// </summary>
        public IList<AccentSymbol> RecommendedAccentSymbols { get; set; }

        /// <summary>
        /// A set of default, recommended, or all one-liners (expressions) for a product.
        /// </summary>
        public IList<OneLiner> RecommendedOneLiners { get; set; }

        /// <summary>
        /// The distinctive lettering (fonts) associated with a product.
        /// </summary>
        public IList<DistinctiveLettering> DistinctiveLettering { get; set; }

        /// <summary>
        /// An option and price e.g. 120 Checks @ USD 22.50
        /// </summary>
        public IList<PricingOption> QuantityPrice { get; set; }

        /// <summary>
        /// Available scenes.
        /// </summary>
        public int Scenes { get; set; }

        /// <summary>
        /// URI's to details of related products e.g. http://api.ordermychecks.com/product/1234
        /// </summary>
        public IList<string> RelatedProducts { get; set; }

        /// <summary>
        /// Check product styles related to this product.
        /// </summary>
        public IList<RelatedStyle> RelatedStyles { get; set; }


        public IList<SoftwarePackage> SoftwarePackages { get; set; }
        /// <summary>
        /// An accessory that a customer orders either to enhance checks, or as a cross sell item. The price of the accessory is for example 20.00 USD, for a quantity of 120 Checks.
        /// </summary>
        public Accessory FraudArmor { get; set; }

        /// <summary>
        /// Variables for front-end validation.
        /// </summary>
        public string MaxCharactersPerLine { get; set; }

        public IList<string> Allowed { get; set; }
        public string AccentOption { get; set; }
        public string AccentDefault { get; set; }
        public string PhantomOption { get; set; }
        public string PhantomDefault { get; set; }
        public string PhantomFI { get; set; }
        public string SigCutOption { get; set; }

        public string HCProductId { get; set; }
        public string HCStyleId { get; set; }
        public string SiglineTextFlag { get; set; }
        public string PrimaryCategory { get; set; }
        public string CategoryDescription { get; set; }
    }

    public class AccentSymbol
    {
        /// <summary>
        /// An accent symbol (pridemark) belonging to a group identified by accentGroup.
        /// </summary>
        public AccessoryDetails Symbol { get; set; }
    }

    public class OneLiner
    {
    }

    public class DistinctiveLettering
    {
        /// <summary>
        /// A font (distinctive lettering)
        /// </summary>
        public AccessoryDetails Font { get; set; }
    }

    public class PricingOption
    {
        public int Units { get; set; }
        public int Quantity { get; set; }
    }

    public class RelatedStyle
    {
        public string Title { get; set; }
        public string Part { get; set; }        
        public string ProductId { get; set; }
        public string ProductColor { get; set; }
        public string ColorDisplay { get; set; }
        public string ColorGroup { get; set; }
        public string ColorImage { get; set; }

    }

    public class SoftwarePackage
    {
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class FraudArmor
    {
    }
}