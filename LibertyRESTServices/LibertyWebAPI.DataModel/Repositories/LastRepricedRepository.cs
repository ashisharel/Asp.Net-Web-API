using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class LastRepricedRepository : BaseRepository<Order>, ILastRepricedRepository
    {
        public static string LastRepricedSPName = "p_PEP_Order_Read";


        public Order GetLastRepriced(int sessionId)
        {
            SqlCommand cmd = new SqlCommand(LastRepricedSPName);
            cmd.Parameters.AddWithValue("@Session_ID", sessionId);
            var list = base.ExecuteStoredProc(cmd);
            return list.FirstOrDefault() ?? null;
        }

        public override Order PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            DateTime tempDate;
            DateTime.TryParse(reader["OrderDate"].ToString(), out tempDate);
            var priceStatus = reader["PriceStatus"].Equals(DBNull.Value) ? -1 : Convert.ToInt32(reader["PriceStatus"]);
            if (priceStatus != 0)
                throw new Exception("10001|PriceStatus = " + priceStatus.ToString());
            return new Order
            {
                //OrderDate = tempDate.ToShortDateString(),
                OrderDate = tempDate != DateTime.MinValue ? tempDate.ToString("MM/dd/yyyy") : "",
                EmailAddress = reader["EmailAddress"].ToString(),
                Telephone = reader["Telephone"].ToString(),
                Customername = reader["Member_Name1"].ToString().Trim() + " " + reader["Member_Name2"].ToString().Trim(), //TODO: exception prone
                //City = reader["City"].ToString(),
                //State = reader["State"].ToString(),
                //ZipCode = reader["Zip"].ToString(),
                //SubTotal = Convert.ToDouble(reader["SubTotal"]),
                ShippingAddress = new ShippingAddress()
                {
                    ShippingLine1 = reader["Ship1"].ToString().Trim(),
                    ShippingLine2 = reader["Ship2"].ToString().Trim(),
                    ShippingLine3 = reader["Ship3"].ToString().Trim(),
                    ShippingLine4 = reader["Ship4"].ToString().Trim(),
                    ShippingLine5 = reader["Ship5"].ToString().Trim(),
                    //ShippingLine6 = reader["Shipping_Line6"].ToString() // only 5 lines supported
                    City = reader["Ship_City"].ToString().Trim(),
                    State = reader["Ship_State"].ToString().Trim(),
                    Zipcode = reader["Ship_Zipcode"].ToString().Trim(),
                    IsForeign = String.Equals(reader["isForeign"].ToString(), "Y", StringComparison.OrdinalIgnoreCase) ? true : false
                },
                OrderItems = MapOrderItems(reader),
            };
        }

        private IList<OrderItem> MapOrderItems(IDataReader reader)
        {
            List<OrderItem> items = new List<OrderItem>();
            int i;
            OrderItem objItem = new OrderItem()
            {
                Accent = new Enhancement
                {
                    Id = reader["accent_ID"].ToString(),
                    Name = reader["accent_name"].ToString().Trim(),
                    Price = reader["accent_price"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["accent_price"]),
                    Removable = reader["accent_removable"].ToString() == "Y" ? true : false,
                    Preselected = reader["accent_preselected"].ToString() == "Y" ? true : false,
                    Url = reader["Accent_URL"].ToString()
                },
                Font = new Enhancement
                {
                    Id = reader["font_ID"].ToString(),
                    Name = reader["font_name"].ToString().Trim(),
                    Price = reader["font_price"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["font_price"]),
                    Removable = reader["font_removable"].ToString() == "Y" ? true : false,
                    Preselected = reader["font_preselected"].ToString() == "Y" ? true : false
                },
                Phantom = new Enhancement
                {
                    Id = reader["Phantom_ID"].ToString(),
                    Name = reader["Phantom_name"].ToString().Trim(),
                    Price = reader["Phantom_price"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["Phantom_price"]),
                    Removable = reader["Phantom_removable"].ToString() == "Y" ? true : false,
                    Preselected = reader["Phantom_preselected"].ToString() == "Y" ? true : false,
                    Url = reader["Phantom_URL"].ToString()
                },
                SigCut = new Enhancement
                {
                    Id = reader["SigCut_ID"].ToString(),
                    Name = reader["SigCut_name"].ToString().Trim(),
                    Price = reader["SigCut_price"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["SigCut_price"]),
                    Removable = reader["SigCut_removable"].ToString() == "Y" ? true : false,
                    Preselected = reader["SigCut_preselected"].ToString() == "Y" ? true : false,
                    Url = reader["Sigcut_URL"].ToString()
                },
                FraudArmor = new Enhancement // should not default fraud armor to an order
                {
                    Id = reader["FraudArmor_ID"].ToString(),
                    Name = reader["FraudArmor_name"].ToString().Trim(),
                    Price = reader["FraudArmor_price"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["FraudArmor_price"]),
                    Removable = reader["FraudArmor_removable"].ToString() == "Y" ? true : false,
                    Preselected = reader["FraudArmor_preselected"].ToString() == "Y" ? true : false
                },

                CheckScenes = Convert.ToInt32(reader["Check_Scenes"]),
                ExtraSigLine = reader["Extra_Sigline"].ToString(),
                FontLine = reader["Font_Line"].ToString(),
                ItemSubTotal = reader["ItemSubTotal"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["ItemSubTotal"]),
                Personalization = new Personalization
                {
                    PersonalizationLine1 = reader["Line1"].ToString(),
                    PersonalizationLine2 = reader["Line2"].ToString(),
                    PersonalizationLine3 = reader["Line3"].ToString(),
                    PersonalizationLine4 = reader["Line4"].ToString(),
                    PersonalizationLine5 = reader["Line5"].ToString(),
                    PersonalizationLine6 = reader["Line6"].ToString(),
                },
                ProductId = reader["Product_Id"].ToString(),
                ProductName = reader["Product_name"].ToString().Trim(),
                Quantity = Convert.ToInt32(reader["Item_Count"]),
                ProductColor = reader["Product_Color"].ToString(),
                //SoftwarePackage = reader["Software_Package"].ToString(),
                ShippingOption = new ShippingOption
                {
                    //Bundled = reader["isBundled"].ToString() == "Y" ? true : false,
                    Code = Convert.ToChar(reader["ship_by"]),
                    Name = reader["Ship_Method_Desc"].ToString(),
                    //EstimatedDelivery = Convert.ToDateTime(reader["ship_days"]), // TODO: get correct est delivery date
                    Fee = reader["Ship_Price"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["Ship_Price"]),
                    //Note = reader["Ship_Note"].ToString()
                },
                SigLine1 = reader["SigLine1"].ToString(),
                SigLine2 = reader["SigLine2"].ToString(),
                StartingCheckNumber = int.TryParse(reader["Start_Check_Number"].ToString(), out i) ? Convert.ToInt32(reader["Start_Check_Number"]) : 0,
                TitlePlateLogo = reader["TitlePlateLogo"].ToString(),
                FITotal = reader["FI_Total"].Equals(DBNull.Value) ? 0.00 : Convert.ToDouble(reader["FI_Total"]),
                
            };
            items.Add(objItem);
            return items;
        }
    }
}