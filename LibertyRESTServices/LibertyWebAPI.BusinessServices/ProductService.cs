using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDTO GetProduct(string productId, int sessionId)
        {
            //Need to split the productId into actual productId and product color for business checks
            var result = productId.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries);            
            var color = result.Count() > 1 ? result[1] : "";

            var product = _productRepository.GetProduct(result[0], color, sessionId);
            if (product != null)
            {
                return MapDTO(product);
            }
            return null;
        }

        public ProductDTO MapDTO(Product product)
        {
            string unitText = GetUnit(product.Type);

            AccessoryDTO accentDefault = null;
            AccessoryDTO backgroundDefault = null;
            AccessoryDTO onelinerDefault = null;
            AccessoryDTO fontDefault = null;

            var allowed = new AllowedDTO();

            if (product.AccentOption == "A" || product.AccentOption == "Y")
                allowed.Accent = true;
            if (product.PhantomOption == "A" || product.PhantomOption == "Y")
                allowed.Background = true;
            if (product.SigCutOption == "R" || product.SigCutOption == "Y")
                allowed.Oneliners = true;
            if (product.DistinctiveLettering != null && product.DistinctiveLettering.Count > 0)
                allowed.Fonts = true;
            if (product.FraudArmor != null && !string.IsNullOrWhiteSpace(product.FraudArmor.Code))
                allowed.FraudArmor = true;
            if (string.Equals(product.SiglineTextFlag, "Y"))
                allowed.OverSignature = true;

            if (product.AccentOption == "A" && !string.IsNullOrWhiteSpace(product.AccentDefault))
                // Automatic add to order, no options to change or remove
                accentDefault = new AccessoryDTO() { Code = product.AccentDefault, Removable = false };
            else if (product.AccentOption == "Y" && !string.IsNullOrWhiteSpace(product.AccentDefault))
                // Available
                accentDefault = new AccessoryDTO() { Code = product.AccentDefault, Removable = true };
            else /* (product.AccentOption == "N" */
                // unknown?
                accentDefault = null;

            //TODO: LCG - 20171121 We need to add SigCutDefault field to proc in SQL ???
            //if (product.SigCutOption == "R" && !string.IsNullOrWhiteSpace(product.SigCutDefault))
            //    // Required!  No products using this currently
            //    onelinerDefault = new AccessoryDTO() { code = product.SigCutDefault, removable = false };
            //else if (product.SigCutOption == "Y" && !string.IsNullOrWhiteSpace(product.SigCutDefault))
            //    // Available
            //    onelinerDefault = new AccessoryDTO() { code = product.SigCutDefault, removable = true };
            //else /* (product.SigCutOption == "N" */
            //    // unknown?
            //    onelinerDefault = null;

            if (product.PhantomOption == "F" && !string.IsNullOrWhiteSpace(product.PhantomFI))
                // This image is not saved with order, but appears on check order (unless customer selects different image)
                backgroundDefault = new AccessoryDTO() { Code = product.PhantomFI, Removable = true };
            else if (product.PhantomOption == "A" && !string.IsNullOrWhiteSpace(product.PhantomDefault))
                // Automatic add to order, no options to change or remove
                backgroundDefault = new AccessoryDTO() { Code = product.PhantomDefault, Removable = false };
            else if (product.PhantomOption == "Y" && !string.IsNullOrWhiteSpace(product.PhantomDefault))
                // Available  (No FI Default image)
                backgroundDefault = new AccessoryDTO() { Code = product.PhantomDefault, Removable = true };
            else
                backgroundDefault = null;

            if (product.DistinctiveLettering != null && product.DistinctiveLettering.Count > 0 && product.DistinctiveLettering.Any(f => f.Font.Selected))
            {
                var defaultFont = product.DistinctiveLettering.FirstOrDefault(f => f.Font.Selected);
                if (defaultFont != null)
                    fontDefault = new AccessoryDTO()
                    {
                        Name = defaultFont.Font.Name,
                        Code = defaultFont.Font.Code,
                        Preselected = true,
                        Removable = true
                    };
            }

            // Temporary fix until we get the HCProductId and HCStyleId for the remaining Liberty Products
            var scenes = new List<SceneDTO>();

            for (var i = 1; i <= product.Scenes; i++)
            {
                var scene = new SceneDTO();
                scene.Product = product.ProductId;
                if (!string.IsNullOrWhiteSpace(product.Url))
                {
                    //scene.BaseImage = product.Url.Replace("1.jpg", ".jpg");
                    scene.Large = product.Url.Replace("1.jpg", i + ".jpg");
                }
                scenes.Add(scene);
            }

            return new ProductDTO()
            {
                Id = product.ProductId,
                Name = product.ProductName.ToNullable(),
                ShortDescription = product.ShortDescription.ToNullable(),
                Description = product.Description.ToNullable(),
                Binding = product.Binding.ToNullable(),
                Color = product.ColorDisp.ToNullable(),
                ColorId = product.ProductColor.ToNullable(),
                Part = product.Part.ToNullable(),
                Type = product.Type,
                PersonalizationLines = product.MaxPersonalizationLines.ToNullableInt(),
                IsBusinessProduct = product.IsBusinessProduct.ToBool(),
                LicenseUri = product.LicenseUri.ToNullable(),
                LicenseText = product.LicenseText.ToNullable(),
                RecommendedAccentSymbols = null,
                RecommendedOneLiners = null,
                DistinctiveLettering = product.DistinctiveLettering.Select(d => new DistinctiveLetteringDTO()
                {
                    Font = new AccessoryDetailsDTO()
                        {
                            Code = d.Font.Code,
                            Name = d.Font.Name,
                            Url = d.Font.Url,
                            Type = d.Font.Type,
                            Pricing = null,
                        }
                }).ToList(),
                PricingOptions = product.QuantityPrice
                    .Select(p => new PricingOptionDTO()
                    {
                        Price = null,
                        Quantity = new QuantityDTO()
                        {
                            Amount = p.Quantity,
                            Unit = unitText,
                        }
                    }).ToList(),
                Scenes = scenes,
                RelatedProducts = null,
                RelatedStyles = product.RelatedStyles
                    .Select(r => new RelatedStyleDTO()
                    {
                        Position = null, //TODO need to add position to product (product.Position)
                        Title = r.Title,
                        Color = new ColorDTO { 
                            Category = r.ColorGroup,
                            Image = r.ColorImage,
                            Name = r.ColorDisplay,
                            Id = r.ProductColor
                        },
                        //Binding = product.Binding,
                        Binding2 = null,
                        Part = r.Part,
                        ProductId = r.ProductId,
                    })
                    .ToList(),
                //SoftwarePackages = product.SoftwarePackages
                //    .Select(a => new SoftwarePackageDTO()
                //    {
                //        TypeId = a.TypeId,
                //        Description = a.Description,
                //    })
                //    .ToList(),
                FraudArmor = new AccessoryDTO()
                {
                    Code = product.FraudArmor.Code,
                    Price = new MoneyDTO() { Amount = product.FraudArmor.Amount },
                    Preselected = product.FraudArmor.Preselected.ToBool(),
                    Removable = product.FraudArmor.Removable.ToBool()
                },
                Validation = new ProductValidationDTO()
                {
                    PersonalizationLinesMaxLength = product.MaxCharactersPerLine.ToNullableInt()
                },
                Allowed = allowed,
                AutoLoadAccent = accentDefault,
                AutoLoadBackground = backgroundDefault,
                AutoLoadOneliner = onelinerDefault,
                AutoLoadFont = fontDefault,
                HCProductId = product.HCProductId,
                HCStyleId = product.HCStyleId,
                Breadcrumb = string.Format("{0}|{1}", product.PrimaryCategory, product.CategoryDescription)
            };
        }

        private string GetUnit(int? productType)
        {
            switch (productType)
            {
                case 9: // business checks
                case 1: // personal checks
                    return "checks";
                default:
                    return null;
            };
        }
    }
}