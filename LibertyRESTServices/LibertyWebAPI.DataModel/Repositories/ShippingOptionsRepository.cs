using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class ShippingOptionsRepository : BaseRepository<ShippingOption>, IShippingOptionsRepository
    {
        public static string StoredProcedureName = "p_PEP_Shipping";

        public IEnumerable<ShippingOption> GetShippingOptions(ShippingOptionsRequestDTO shippingOptionsRequest, int sessionId)
        {
            IList<SqlParameter> outParams;
            SqlCommand cmd = new SqlCommand(StoredProcedureName);

            cmd.Parameters.AddWithValue("@Product_ID", shippingOptionsRequest.ProductID);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            cmd.Parameters.AddWithValue("@Item_Count", shippingOptionsRequest.Quantity);
            cmd.Parameters.AddWithValue("@Zipcode", shippingOptionsRequest.ZipCode.Substring(0,5));
            cmd.Parameters.AddWithValue("@PO_Box_Flag", shippingOptionsRequest.IsPOBox == true ? "Y" : "N");
            cmd.Parameters.AddWithValue("@International_Flag", shippingOptionsRequest.IsForeign == true ? "Y" : "N");
            SqlParameter outputShipByParam = new SqlParameter("@OUT_Ship_By", SqlDbType.Char)
            {
                Direction = ParameterDirection.Output,
                Size = 1
            };
            cmd.Parameters.Add(outputShipByParam);
            var shippingOptions = base.ExecuteStoredProcWithOutputParameters(cmd, out outParams);
            //shippingOptions.Single(x => x.Code == outParams.FirstOrDefault().Value).IsPreselected = true; //to throw error for testing purpose
            foreach (var option in shippingOptions.Where(x => outParams.FirstOrDefault().Value != DBNull.Value && x.Code == Convert.ToChar(outParams.FirstOrDefault().Value)))
            {
                option.IsPreselected = true;
            }
            return shippingOptions;
        }


        public override ShippingOption PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return new ShippingOption
            {
                Code = Convert.ToChar(reader["Ship_Method_Code"]),
                Name = Convert.ToString(reader["Ship_method_desc"]),
                Note = reader["Web_Tag"].ToString(),
                Fee = Convert.ToDouble(reader["Extra_Charge"]),
                EstimatedDelivery = Convert.ToDateTime(reader["Estimated_Delivery"])

            };
        }
    }
}
