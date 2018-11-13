using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class CatalogRepository : BaseRepository<Catalog>, ICatalogRepository
    {
        private static string StoredProcedureName = "p_PEP_Catalog";
        public IList<Catalog> GetCatalogData(int sessionId)
        {            
            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId); 
            return base.ExecuteStoredProc(cmd); 
        }
                
        public override Catalog PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return new Catalog
            {
                CategoryId = Convert.ToInt32(reader["Category_UID"]),
                PrimaryCategory = Convert.ToInt32(reader["Primary_Category"]),
                CategoryName = reader["description"].ToString(),                
                PrimaryDescription = reader["Primary_Description"].ToString(),
                CategoryAttributes = reader["Category_Attributes"].ToString()
            };
        }
    }
}
