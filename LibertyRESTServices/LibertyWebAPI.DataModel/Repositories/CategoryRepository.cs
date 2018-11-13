using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public static string StoredProcedureName = "p_PEP_Catalog";
        
        public IEnumerable<Category> GetCategoryData(CategoryRequestDTO categoryRequest, int sessionId)
        {            
            SqlCommand cmd = new SqlCommand(StoredProcedureName);            
            if (categoryRequest.CategoryId != null)
                cmd.Parameters.AddWithValue("@Category_UID", categoryRequest.CategoryId);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@MaxRows", categoryRequest.MaxCount);             
            return base.ExecuteStoredProc(cmd);
        }


        public override Category PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return new Category
            {
                ProductID = Convert.ToString(reader["Catalog_Product_Id"]),
                Price = reader["Price"].Equals(DBNull.Value) ? Decimal.Zero : Convert.ToDecimal(reader["Price"]),
                ProductName = reader["Mktg_Name"].ToString(),
                CategoryName = reader["Category_Description"].ToString(), 
                /*PrimaryCategory = Convert.ToInt32(reader["Primary_Category"]), */
                Check_Scenes = Convert.ToInt32(reader["Check_Scenes"]),
                HarlandProductId = reader["HC_Product_ID"].ToString(),
                StyleId = reader["HC_Style_ID"].ToString(), 
                Attribute = reader["Attrib"].ToString(),
                PopularityScore = Convert.ToInt32(reader["Popularity_Score"]),
                ImageURL = reader["ImageURL"].ToString(),
                ProductType = reader["ProductType"].Equals(DBNull.Value) ? null : (int?)(reader["ProductType"])
            };
        }
    }
}
