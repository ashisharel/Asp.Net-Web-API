using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LibertyWebAPI.DataModel.Repositories
{
    /// <summary>
    /// submit OrderDTO to database
    /// </summary>
    public class OrderSubmitRepository : BaseRepository<Order>, IOrderSubmitRepository
    {
        public OrderSubmit SubmitOrder(int sessionId, OrderDTO order)
        {
            // save items into the cart first
            var errors = SaveCart(sessionId, order);

            if (errors == null || errors.Count == 0) // SaveCart was successful
            {
                try
                {
                    // submit order to database
                    return OrderSubmitDb(sessionId, order);                    
                }
                catch (SqlException ex)
                {
                    if(ex.Message.Contains("deadlock victim"))
                    {
                        return OrderSubmitDb(sessionId, order);
                    }
                    throw ex;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private OrderSubmit OrderSubmitDb(int sessionId, OrderDTO order)
        {
            IList<SqlParameter> param;
            SqlCommand cmd = new SqlCommand("p_PEP_Order_Submit");
            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@Session_Id", Value = sessionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input },
                new SqlParameter() { ParameterName = "@Email", Value = order.Customer.EmailAddress, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Precision = 80 },
                new SqlParameter() { ParameterName = "@SMS_Phone", Value = order.Customer.Telephone != null ? order.Customer.Telephone.Home : "", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Precision = 12 },
                new SqlParameter() { ParameterName = "@Subscribe_Flag", Value = order.Subscribe ? 'Y' : 'N', SqlDbType = SqlDbType.Char, Direction = ParameterDirection.Input },
                new SqlParameter() { ParameterName = "@Language_Code", Value = order.Language, SqlDbType = SqlDbType.Char, Direction = ParameterDirection.Input },
                new SqlParameter() { ParameterName = "@Success_Flag", Value = "", SqlDbType = SqlDbType.Char, Direction = ParameterDirection.Output, Precision = 1},
                new SqlParameter() { ParameterName = "@Order_Dt", Value = "", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Output},
                new SqlParameter() { ParameterName = "@Service_Indicator", Value = "", SqlDbType = SqlDbType.Char, Direction = ParameterDirection.Output, Precision = 1},
                new SqlParameter() { ParameterName = "@Ach_Delay", Value = "", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output}
            });

            base.ExecuteStoredProcWithOutputParameters(cmd, out param);

            if (param == null)
                return null;

            var success = param.FirstOrDefault(r => r.ParameterName == "@Success_Flag");
            if (success == null || Convert.ToChar(success.Value) != 'Y')
                return null;

            return PopulateOutputs(param);
        }

        private OrderSubmit PopulateOutputs(IList<SqlParameter> param)
        {
            return new OrderSubmit
            {
                ACHDelay = Convert.ToInt32(param.FirstOrDefault(r => r.ParameterName == "@Ach_Delay").Value),
                orderDate = Convert.ToDateTime(param.FirstOrDefault(r => r.ParameterName == "@Order_Dt").Value),
                ServiceIndicator = Convert.ToChar(param.FirstOrDefault(r => r.ParameterName == "@Service_Indicator").Value),
                Success = Convert.ToChar(param.FirstOrDefault(r => r.ParameterName == "@Success_Flag").Value)
            };
        }

        private IList<Error> SaveCart(int sessionId, OrderDTO order)
        {
            var errors = new List<Error>();
            string firstItem = "Y";

            // move the "check" items to the top
            var sort = order.Items.OrderByDescending(i => i.Check);

            foreach (var item in sort)
            {
                var result = SaveItemToCart(item, order.ShippingAddress, sessionId, firstItem);
                if (result == null || result.Equals("N"))
                {
                    //  :-( failure ) to save item into the cart
                    var productId = item.Check != null ? item.Check.ProductId : item.Accessory.Code;
                    errors.Add(new Error() { Message = string.Format("Failure saving item into the cart; productId - {0}", productId) });
                }
                firstItem = "N";
            }
            return errors;
        }

        private string SaveItemToCart(OrderItemDTO item, AddressDTO shipping, int sessionId, string firstItem)
        {
            IList<SqlParameter> outputParam;
            var cmd = new SqlCommand("p_PEP_Cart_Save");

            if (item.Check != null && !string.IsNullOrWhiteSpace(item.Check.ProductId))
            {
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Session_Id", Value = sessionId, SqlDbType = SqlDbType.Int });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@First_Flag", Value = firstItem, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Check_Flag", Value = "Y", SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Success_Flag", Value = "", SqlDbType = SqlDbType.Char, Direction = ParameterDirection.Output, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Product_ID", Value = item.Check.ProductId, SqlDbType = SqlDbType.VarChar, Precision = 12 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Cover_ID", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 12 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Item_Count", Value = item.Check.Quantity != null ? item.Check.Quantity.Amount : 0, SqlDbType = SqlDbType.Int });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Pri_Font", Value = item.Check.Font != null && !string.IsNullOrWhiteSpace(item.Check.Font.Code) ? item.Check.Font.Code : "", SqlDbType = SqlDbType.Char, Precision = 2 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CornerAccent", Value = item.Check.Accent != null && !string.IsNullOrWhiteSpace(item.Check.Accent.Code) ? item.Check.Accent.Code : "", SqlDbType = SqlDbType.VarChar, Precision = 8 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CenterAccent", Value = item.Check.Background != null && !string.IsNullOrWhiteSpace(item.Check.Background.Code) ? item.Check.Background.Code : "", SqlDbType = SqlDbType.VarChar, Precision = 8 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@SigCut", Value = item.Check.OneLiner != null && !string.IsNullOrWhiteSpace(item.Check.OneLiner.Code) ? item.Check.OneLiner.Code : "", SqlDbType = SqlDbType.VarChar, Precision = 8 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Service_ID", Value = (item.Check.FraudArmor == null || string.IsNullOrWhiteSpace(item.Check.FraudArmor.Code)) ? string.Empty : item.Check.FraudArmor.Code, SqlDbType = SqlDbType.VarChar, Precision = 12 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Font_Line", Value = CreateFontLine(item.Check.Personalization), SqlDbType = SqlDbType.Char, Precision = 6 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line1", Value = item.Check.Personalization != null && item.Check.Personalization.PersLine1 != null && !string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine1.Text) ? item.Check.Personalization.PersLine1.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line2", Value = item.Check.Personalization != null && item.Check.Personalization.PersLine2 != null && !string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine2.Text) ? item.Check.Personalization.PersLine2.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line3", Value = item.Check.Personalization != null && item.Check.Personalization.PersLine3 != null && !string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine3.Text) ? item.Check.Personalization.PersLine3.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line4", Value = item.Check.Personalization != null && item.Check.Personalization.PersLine4 != null && !string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine4.Text) ? item.Check.Personalization.PersLine4.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line5", Value = item.Check.Personalization != null && item.Check.Personalization.PersLine5 != null && !string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine5.Text) ? item.Check.Personalization.PersLine5.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line6", Value = item.Check.Personalization != null && item.Check.Personalization.PersLine6 != null && !string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine6.Text) ? item.Check.Personalization.PersLine6.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_To", Value = "S", SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship1", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine1) ? shipping.ShipLine1 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship2", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine2) ? shipping.ShipLine2 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship3", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine3) ? shipping.ShipLine3 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship4", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine4) ? shipping.ShipLine4 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship5", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine5) ? shipping.ShipLine5 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_City", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.City) ? shipping.City : "", SqlDbType = SqlDbType.VarChar, Precision = 30 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_State", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.State) ? shipping.State : "", SqlDbType = SqlDbType.VarChar, Precision = 2 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_Zip", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.PostalCode) ? shipping.PostalCode : "", SqlDbType = SqlDbType.VarChar, Precision = 10 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@SigLine1", Value = item.Check.OverSignature != null && item.Check.OverSignature.Length >= 1 ? item.Check.OverSignature[0] : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@SigLine2", Value = item.Check.OverSignature != null && item.Check.OverSignature.Length >= 2 ? item.Check.OverSignature[1] : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                /*new SqlParameter() { ParameterName = "@Imprint_Text", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 40 },*/
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Start_Check_Number", Value = item.Check.StartAt.ToString(), SqlDbType = SqlDbType.Char, Precision = 6 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Product_Color", Value = string.IsNullOrWhiteSpace(item.Check.Color) ? "" : item.Check.Color, SqlDbType = SqlDbType.VarChar, Precision = 15 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Software_Package", Value = string.IsNullOrWhiteSpace(item.Check.SoftwarePackage) ? "" : item.Check.SoftwarePackage, SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Bill_To", Value = item.ActualBillCode != null && item.ActualBillCode.Length > 0 ? item.ActualBillCode[0].ToString() : string.Empty, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_By", Value = item.ActualBillCode != null && item.ActualBillCode.Length > 1 ? item.ActualBillCode[1].ToString() : string.Empty, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_Charge", Value = item.ActualBillCode != null && item.ActualBillCode.Length > 2 ? item.ActualBillCode[2].ToString() : string.Empty, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Price", Value = item.ItemTotal != null ? item.ItemTotal.Amount * 100 : 0, SqlDbType = SqlDbType.Int });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@FI_Price", Value = item.FITotal != null ? item.FITotal.Amount * 100 : 0, SqlDbType = SqlDbType.Int });
                /*new SqlParameter() { ParameterName = "@Promo_Price_UID", Value = "0", SqlDbType = SqlDbType.Int },*/
            }

            if (item.Accessory != null && !string.IsNullOrWhiteSpace(item.Accessory.Code))
            {
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Session_Id", Value = sessionId, SqlDbType = SqlDbType.Int });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@First_Flag", Value = firstItem, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Check_Flag", Value = "N", SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Success_Flag", Value = "", SqlDbType = SqlDbType.Char, Direction = ParameterDirection.Output, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Product_ID", Value = item.Accessory.Code, SqlDbType = SqlDbType.VarChar, Precision = 12 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Cover_ID", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 12 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Item_Count", Value = item.Accessory.Quantity != null ? item.Accessory.Quantity.Amount : 0, SqlDbType = SqlDbType.Int });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Pri_Font", Value = "", SqlDbType = SqlDbType.Char, Precision = 2 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CornerAccent", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 8 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@CenterAccent", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 8 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@SigCut", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 8 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Service_ID", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 12 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Font_Line", Value = CreateFontLine(item.Accessory.Personalization), SqlDbType = SqlDbType.Char, Precision = 6 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line1", Value = item.Accessory.Personalization != null && item.Accessory.Personalization.PersLine1 != null && !string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine1.Text) ? item.Accessory.Personalization.PersLine1.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line2", Value = item.Accessory.Personalization != null && item.Accessory.Personalization.PersLine2 != null && !string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine2.Text) ? item.Accessory.Personalization.PersLine2.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line3", Value = item.Accessory.Personalization != null && item.Accessory.Personalization.PersLine3 != null && !string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine3.Text) ? item.Accessory.Personalization.PersLine3.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line4", Value = item.Accessory.Personalization != null && item.Accessory.Personalization.PersLine4 != null && !string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine4.Text) ? item.Accessory.Personalization.PersLine4.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line5", Value = item.Accessory.Personalization != null && item.Accessory.Personalization.PersLine5 != null && !string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine5.Text) ? item.Accessory.Personalization.PersLine5.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Line6", Value = item.Accessory.Personalization != null && item.Accessory.Personalization.PersLine6 != null && !string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine6.Text) ? item.Accessory.Personalization.PersLine6.Text : "", SqlDbType = SqlDbType.VarChar, Precision = 50 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_To", Value = "S", SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship1", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine1) ? shipping.ShipLine1 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship2", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine2) ? shipping.ShipLine2 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship3", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine3) ? shipping.ShipLine3 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship4", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine4) ? shipping.ShipLine4 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship5", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.ShipLine5) ? shipping.ShipLine5 : "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_City", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.City) ? shipping.City : "", SqlDbType = SqlDbType.VarChar, Precision = 30 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_State", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.State) ? shipping.State : "", SqlDbType = SqlDbType.VarChar, Precision = 2 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_Zip", Value = shipping != null && !string.IsNullOrWhiteSpace(shipping.PostalCode) ? shipping.PostalCode : "", SqlDbType = SqlDbType.VarChar, Precision = 10 });                
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@SigLine1", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@SigLine2", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 40 });
                /*new SqlParameter() { ParameterName = "@Imprint_Text", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 40 },*/
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Start_Check_Number", Value = "", SqlDbType = SqlDbType.Char, Precision = 6 });
                /*new SqlParameter() { ParameterName = "@Product_Color", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 15 },*/
                /*new SqlParameter() { ParameterName = "@Software_Package", Value = "", SqlDbType = SqlDbType.VarChar, Precision = 50 },*/
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Bill_To", Value = item.ActualBillCode != null && item.ActualBillCode.Length > 0 ? item.ActualBillCode[0].ToString() : string.Empty, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_By", Value = item.ActualBillCode != null && item.ActualBillCode.Length > 1 ? item.ActualBillCode[1].ToString() : string.Empty, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Ship_Charge", Value = item.ActualBillCode != null && item.ActualBillCode.Length > 2 ? item.ActualBillCode[2].ToString() : string.Empty, SqlDbType = SqlDbType.Char, Precision = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Price", Value = item.ItemTotal != null ? item.ItemTotal.Amount * 100 : 0, SqlDbType = SqlDbType.Int });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@FI_Price", Value = item.FITotal != null ? item.FITotal.Amount * 100 : 0, SqlDbType = SqlDbType.Int });
                /*new SqlParameter() { ParameterName = "@Promo_Price_UID", Value = "0", SqlDbType = SqlDbType.Int },*/
            }

            ExecuteStoredProcWithOutputParameters(cmd, out outputParam);

            if (outputParam == null)
                return null;

            var output = outputParam.FirstOrDefault(r => r.ParameterName == "@Success_Flag");

            if (output == null)
                return null;

            return output.Value.ToString();
        }

        private static string CreateFontLine(PersonalizationDTO personalization)
        {
            if (personalization == null)
                return "";

            var fontLine = new StringBuilder();

            if (personalization.PersLine1 != null)
            {
                fontLine.Append(personalization.PersLine1.IsBold == true ? "N" : "A");
            }
            if (personalization.PersLine2 != null)
            {
                fontLine.Append(personalization.PersLine2.IsBold == true ? "N" : "A");
            }
            if (personalization.PersLine3 != null)
            {
                fontLine.Append(personalization.PersLine3.IsBold == true ? "N" : "A");
            }
            if (personalization.PersLine4 != null)
            {
                fontLine.Append(personalization.PersLine4.IsBold == true ? "N" : "A");
            }
            if (personalization.PersLine5 != null)
            {
                fontLine.Append(personalization.PersLine5.IsBold == true ? "N" : "A");
            }
            if (personalization.PersLine6 != null)
            {
                fontLine.Append(personalization.PersLine6.IsBold == true ? "N" : "A");
            }
            return fontLine.ToString();
        }

        public override Order PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            // we're not expecting a dataset
            return null;
        }
    }
}