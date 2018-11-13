using LibertyWebAPI.DTO.Common;
using System.Collections.Generic;

namespace LibertyWebAPI.DTO.Product
{
    /// <summary>
    /// Details of a product in the product catalog.
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// An identifier used to identify a product e.g. checks, accessory.
        /// It identifies the product including its binding, etc.
        /// Synonyms are stock keeping unit (SKU), and product code.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description of the product.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// //The style of binding used on the checks e.g. duplicate.
        /// </summary>
        public string Binding { get; set; }

        /// <summary>
        /// The color style of the checks.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The color ID of the checks.
        /// </summary>
        public string ColorId { get; set; }

        /// <summary>
        /// The 'part' style of the checks.
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// The type of product e.g. Check = 1
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// The number of personalization lines available. (minimum:1)
        /// </summary>
        public int? PersonalizationLines { get; set; }

        /// <summary>
        /// Indicates whether the check product is a business product or a personal product.
        /// </summary>
        public bool IsBusinessProduct { get; set; }

        /// <summary>
        /// The URI to the license image.
        /// </summary>
        public string LicenseUri { get; set; }

        /// <summary>
        /// The text for the additional license info.
        /// </summary>
        public string LicenseText { get; set; }

        /// <summary>
        /// A set of default, recommended, or all accent symbols (pride marks) for a product.
        /// </summary>
        public IList<AccentSymbolDTO> RecommendedAccentSymbols { get; set; }

        /// <summary>
        /// A set of default, recommended, or all one-liners (expressions) for a product.
        /// </summary>
        public IList<OneLinerDTO> RecommendedOneLiners { get; set; }

        /// <summary>
        /// The distinctive lettering (fonts) associated with a product.
        /// </summary>
        public IList<DistinctiveLetteringDTO> DistinctiveLettering { get; set; }

        /// <summary>
        /// An option and price e.g. 120 Checks @ USD 22.50
        /// </summary>
        public IList<PricingOptionDTO> PricingOptions { get; set; }

        /// <summary>
        /// Available scenes.
        /// </summary>
        public IList<SceneDTO> Scenes { get; set; }

        /// <summary>
        /// URI's to details of related products e.g. http://api.ordermychecks.com/product/1234
        /// </summary>
        public string[] RelatedProducts { get; set; }

        /// <summary>
        /// Check product styles related to this product.
        /// </summary>
        public IList<RelatedStyleDTO> RelatedStyles { get; set; }

        ///// <summary>
        ///// list of Accounting Software compatible with this product
        ///// </summary>
        //public IList<SoftwarePackageDTO> SoftwarePackages { get; set; } // not used by NMAN, hence removed
        /// <summary>
        /// An accessory that a customer orders either to enhance checks, or as a cross sell item. The price of the accessory is for example 20.00 USD, for a quantity of 120 Checks.
        /// </summary>
        public AccessoryDTO FraudArmor { get; set; }

        /// <summary>
        /// Variables for front-end validation.
        /// </summary>
        public ProductValidationDTO Validation { get; set; }

        /// <summary>
        /// a list that shows what is allowed on the product.
        /// </summary>
        public AllowedDTO Allowed { get; set; }

        public AccessoryDTO AutoLoadAccent { get; set; }
        public AccessoryDTO AutoLoadOneliner { get; set; }
        public AccessoryDTO AutoLoadFont { get; set; }
        public AccessoryDTO AutoLoadBackground { get; set; }

        public string HCProductId { get; set; }
        public string HCStyleId { get; set; }
        public string Breadcrumb { get; set; }
    }

    /// <summary>
    /// A style of check product with a binding.
    /// </summary>
    public class RelatedStyleDTO
    {
        public string Position { get; set; }
        
        /// <summary>
        /// Title for the style of check.
        /// </summary>
        public string Title { get; set; }

        public ColorDTO Color { get; set; }

        public string Binding2 { get; set; }

        /// <summary>
        /// /The style of binding used on the checks e.g. duplicate.
        /// </summary>
        public string Binding { get; set; }

        public string Part { get; set; }

        /// <summary>
        /// The productId of that contains the style.
        /// </summary>
        public string ProductId { get; set; }
    }

    /// <summary>
    /// An option and price e.g. 120 Checks @ USD 22.50
    /// </summary>
    public class PricingOptionDTO
    {
        public MoneyDTO Price { get; set; }

        /// <summary>
        /// A quantity that consists of an amount and a unit of measure.
        /// </summary>
        public QuantityDTO Quantity { get; set; }
    }

    /// <summary>
    /// A one-liner (expression).
    /// </summary>
    public class OneLinerDTO
    {
        /// <summary>
        /// A one-liner (expression).
        /// </summary>
        public AccessoryDetailsDTO Line { get; set; }
    }

    /// <summary>
    /// //Details about an accessory that a customer may order either to enhance checks, or as a cross sell item. It includes available quantities.
    /// </summary>
    public class AccessoryDetailsDTO
    {
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
        public IList<PricingOptionDTO> Pricing { get; set; }

        /// <summary>
        /// The type of the accessory. Only applies to AccentSymbols.
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// //URI's to the small image, thumbnail, and large images of the product identified by productId
    /// </summary>
    public class SceneDTO
    {
        /// <summary>
        /// An identifier used to identify a product e.g. checks, accessory. It identifies the product including its binding, etc. Synonyms are stock keeping unit (SKU), and product code.'
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// URI to the base image of a product.
        /// </summary>
        public string BaseImage { get; set; }

        /// <summary>
        /// URI to the small image of a product.
        /// </summary>
        public string Small { get; set; }

        /// <summary>
        /// URI to the thumbnail image of a product.
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// URI to the large image of a product.
        /// </summary>
        public string Large { get; set; }
    }

    /// <summary>
    /// A font (distinctive lettering)
    /// </summary>
    public class DistinctiveLetteringDTO
    {
        /// <summary>
        /// A font (distinctive lettering)
        /// </summary>
        public AccessoryDetailsDTO Font { get; set; }
    }

    /// <summary>
    /// An accent symbol (pride mark) belonging to a group identified by accentGroup.
    /// </summary>
    public class AccentSymbolDTO
    {
        public AccessoryDetailsDTO Symbol { get; set; }
    }

    public class ProductValidationDTO
    {
        /// <summary>
        /// The max length for the personalization lines in a given product.
        /// </summary>
        public int? PersonalizationLinesMaxLength { get; set; }
    }

    /// <summary>
    /// Accounting Software
    /// </summary>
    public class SoftwarePackageDTO
    {
        public int? TypeId { get; set; }
        public string Description { get; set; }
    }

    public class AllowedDTO
    {
        public bool Accent { get; set; }
        public bool Background { get; set; }
        public bool Oneliners { get; set; }
        public bool Fonts { get; set; }
        public bool FraudArmor { get; set; }
        public bool OverSignature { get; set; }
    }

    /// <summary>
    /// Color options for business products
    /// </summary>
    public class ColorDTO
    {       
        public string Category { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }
}