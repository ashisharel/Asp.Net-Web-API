using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private Product product;
        private static string StoredProcedureName = "p_PEP_Product";
        //private bool isEnabledRelease2 = ConfigurationManager.AppSettings["EnableRelease2"] == "Y";

        public Product GetProduct(string productId, string colorId, int sessionId)
        {
            //if (isEnabledRelease2)
            //    StoredProcedureName = "p_PEP_Product2";

            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@Product_ID", productId);
            cmd.Parameters.AddWithValue("@Color_ID", colorId);
            base.ExecuteStoredProc(cmd);
            return product;
        }

        public override Product PopulateRecord(System.Data.IDataReader reader, int resultCount)
        {
            // General product info
            if (resultCount == 1)
            {
                product = new Product()
                {
                    ProductId = reader["ProductId"].ToString(),
                    ProductName = reader["name"].ToString(),
                    ShortDescription = reader["shortDescription"].ToString(),
                    Description = reader["Description"].ToString(),
                    HCProductId = reader["HC_Product_ID"].ToString(),
                    HCStyleId = reader["HC_Style_ID"].ToString(),
                    Url = reader["ImageURL"].ToString(),
                    Scenes = Convert.ToInt32(reader["Scenes"]),
                    Binding = reader["Binding"].ToString(),
                    ColorDisp = reader["Color_Disp"].ToString(),
                    ProductColor = reader["Product_Color"].ToString(),
                    ColorFlag = reader["ColorFlag"].ToString(),
                    SoftwareFlag = reader["SoftwareFlag"].ToString(),
                    Part = reader["Part"].ToString(),
                    MaxPersonalizationLines = reader["personalizationLines"].ToString(),
                    MaxCharactersPerLine = reader["personalizationLinesMaxLength"].ToString(),
                    Type = reader["ProductType"].Equals(DBNull.Value) ? null : (int?)(reader["ProductType"]),
                    IsBusinessProduct = reader["isBusinessProduct"].ToString(),
                    LicenseUri = reader["licenseUri"].ToString(),
                    LicenseText = reader["licenseText"].ToString(),
                    AccentOption = reader["AccentOption"].ToString(),
                    AccentDefault = reader["AccentDefault"].ToString(),
                    PhantomOption = reader["PhantomOption"].ToString(),
                    PhantomDefault = reader["PhantomDefault"].ToString(),
                    PhantomFI = reader["PhantomFI"].ToString(),
                    SigCutOption = reader["SigCutOption"].ToString(),
                    SiglineTextFlag = reader["Sigline_Text_Flag"].ToString(),
                    FraudArmor = new Accessory()
                    {
                        Code = reader["FraudArmorID"].ToString(),
                        Amount = Convert.ToDouble(reader["FraudArmorPrice"].Equals(DBNull.Value) ? 0 : reader["FraudArmorPrice"]),
                        Preselected = reader["FraudArmor_preselected"].ToString(), // FA+ default ON changes
                        Removable = reader["FraudArmor_removable"].ToString()
                    },
                    PrimaryCategory = reader["Category_Primary"].ToString(),
                    CategoryDescription = reader["Category_Description"].ToString()
                };
                return null;
            }

            //Result set 2: Available Quantities
            if (product != null && resultCount == 2)
            {
                product.QuantityPrice.Add(new PricingOption()
                {
                    Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                    Units = Convert.ToInt32(reader["Units"].ToString()),
                });
                return null;
            }

            //-- Result Set 3: Related Styles
            if (product != null && resultCount == 3)
            {
                product.RelatedStyles.Add(new RelatedStyle()
                {
                    ProductId = reader["productId"].ToString(),
                    Title = reader["title"].ToString(),
                    Part = reader["Part"].ToString(),
                    ProductColor = reader["Product_Color"].ToString(),
                    ColorDisplay = reader["Color_Disp"].ToString(),
                    ColorGroup = reader["Color_Group"].ToString(),
                    ColorImage = reader["Color_Image"].ToString()                    
                });
                return null;
            }            

            //-- Result Set 4: Font List   (Font_ID, Name, Selected)  Selected indicates default font on the product.
            if (product != null && resultCount == 4)
            {
                product.DistinctiveLettering.Add(
                    new DistinctiveLettering()
                    {
                        Font = new AccessoryDetails()
                        {
                            Code = reader["Font_ID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Pricing = null,
                            Url = reader["Font_URL"].ToString(),
                            Selected = reader["Selected"].ToString().Equals("Selected", StringComparison.OrdinalIgnoreCase)
                        }
                    });
                return null;
            }

            //-- Result Set 5:  Coordinating Accessories
            if (resultCount == 5)
            {
                return null;
            }

            ////-- Result Set 6:   Product color options // LC1VHS (has colors and software) LC1B
            //if (!isEnabledRelease2 && product != null && product.ColorFlag == "Y" && resultCount == 6)
            //{
            //    product.RelatedStyles.Add(new RelatedStyle()
            //    {
            //        //ProductId = reader["Order_Product_id"].ToString(),
            //        ProductColor = reader["Product_Color"].ToString(),
            //        ColorDisplay = reader["Color_Disp"].ToString(),
            //    });
            //    return null;
            //}

            ////-- Result Set 6:   Software package options // not needed by NMAN, hence removed
            //if (product != null && product.SoftwareFlag == "Y" && resultCount ==6)
            //{
            //    product.SoftwarePackages.Add(new SoftwarePackage()
            //    {
            //        TypeId = Convert.ToInt32(reader["Software_Type_UID"]),
            //        Type = reader["Software_Type"].ToString(),
            //        Description = reader["Description"].ToString(),
            //    });
            //    return null;
            //}

            return null;
        }
    }
}