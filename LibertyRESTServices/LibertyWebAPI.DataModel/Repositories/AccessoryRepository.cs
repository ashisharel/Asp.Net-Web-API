using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class AccessoryRepository : BaseRepository<Accessory>, IAccessoryRepository
    {
        private static string StoredProcedureName = "p_PEP_Product_ACC";

        public IList<Accessory> GetProductAccessories(string productId, int sessionId)
        {
            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@Product_ID", productId);
            return base.ExecuteStoredProc(cmd);
        }

        public override Accessory PopulateRecord(IDataReader reader, int resultCount)
        {
            return new Accessory()
            {
                Code = reader["productID"].ToString(),
                Name = reader["Name"].ToString(),
                Amount = Convert.ToDouble(reader["Price"].Equals(DBNull.Value) ? 0 : reader["Price"]),
                Type = reader["ProductType"].Equals(DBNull.Value) ? null : (int?)(reader["ProductType"]),
                HCProductId = reader["HC_Product_ID"].ToString(),
                Url = reader["ImageURL"].ToString(),
                Quantity = Convert.ToInt32(reader["Unit_Size"].Equals(DBNull.Value) ? 0 : reader["Unit_Size"]),
                maxQuantity = Convert.ToInt32(reader["MaxQuantity"].Equals(DBNull.Value) ? 0 : reader["MaxQuantity"])
            };
            
        }
    }
}