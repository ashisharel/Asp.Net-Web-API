using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class OrderHistoryRepository : BaseRepository<OrderSummary>, IOrderHistoryRepository
    {
        public static string StoredProcedureName = "p_PEP_Status";

        public IEnumerable<OrderSummary> GetOrderHistory(int sessionId, int? maxCount)
        {
            SqlCommand cmd = new SqlCommand(StoredProcedureName);

            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@OrderCount", maxCount);
            return base.ExecuteStoredProc(cmd);
        }

        public override OrderSummary PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return new OrderSummary()
            {
                OrderId = reader["order_id"].ToString(),
                Products = new ProductSummary()
                {
                    Amount = System.Convert.ToInt32(reader["ItemCount"]),
                    DeliveryDate = reader["Estimated_Delivery"].Equals(DBNull.Value) ? null : Convert.ToDateTime(reader["Estimated_Delivery"]).ToString("MM/dd/yyyy"),
                    Description = reader["product_name"].ToString(),
                    Id = reader["product_id"].ToString(),
                    Image = reader["ImageURL"].ToString(),
                    OrderStatus = reader["OrderStatus"].ToString(),
                    ProductType = Convert.ToInt32(reader["ProductType"]),
                    ShippingOption = reader["ShippingMethod"].ToString(),                    
                    TrackingNumber = reader["TrackingNumber"].ToString(),
                    TrackingUrl = reader["TrackingURL"].ToString(),
                    HcProductId = reader["hc_product_id"].ToString(),
                    ShippedDate = reader["ShipDate"].Equals(DBNull.Value) ? null : Convert.ToDateTime(reader["ShipDate"]).ToString("MM/dd/yyyy"),
                    OrderDate = reader["OrderDate"].Equals(DBNull.Value) ? null : Convert.ToDateTime(reader["OrderDate"]).ToString("MM/dd/yyyy"),
                    IsTrackable = String.Equals(reader["isTrackable"].ToString(), "Y", StringComparison.OrdinalIgnoreCase) ? true : false,
                    Part = reader["Part"].ToString()
                },
            };
        }
    }
}