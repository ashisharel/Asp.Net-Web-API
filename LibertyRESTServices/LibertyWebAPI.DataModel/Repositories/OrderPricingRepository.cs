using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class OrderPricingRepository : BaseRepository<Order>, IOrderPricingRepository
    {
        private static string OrderPricingSPName = "p_PEP_Price";
        private MoneyDTO orderSubtotal = new MoneyDTO();
        private MoneyDTO orderShipping = new MoneyDTO();
        private MoneyDTO ordertax = new MoneyDTO();
        private MoneyDTO orderTotal = new MoneyDTO();
        private int priceMessageCode;

        public ErrorsDTO GetOrderPrice(OrderDTO orderRequest, int sessionId)
        {
            
            foreach (var item in orderRequest.Items)
            {
                IList<SqlParameter> outParams;
                SqlCommand cmd = new SqlCommand(OrderPricingSPName);
                if (item.Check != null && !string.IsNullOrWhiteSpace(item.Check.ProductId)) // check
                {
                    cmd.Parameters.AddWithValue("@Session_ID", sessionId);
                    cmd.Parameters.AddWithValue("@Check_Flag", 'Y');
                    cmd.Parameters.AddWithValue("@Product_ID", item.Check.ProductId);
                    //cmd.Parameters.AddWithValue("@Cover", item.Check.); TODO: check about cover field                      
                    cmd.Parameters.AddWithValue("@Ship_By", item.Check.ShippingOption == null || item.Check.ShippingOption.Code == '\0' ? '?' : item.Check.ShippingOption.Code);
                    if (item.Check.Quantity != null)
                        cmd.Parameters.AddWithValue("@Item_Count", item.Check.Quantity.Amount);
                    if (item.Check.Font != null)
                        cmd.Parameters.AddWithValue("@Font_ID", item.Check.Font.Code);
                    if (item.Check.Accent != null)
                        cmd.Parameters.AddWithValue("@Accent", item.Check.Accent.Code);
                    if (item.Check.Background != null)
                        cmd.Parameters.AddWithValue("@Phantom", item.Check.Background.Code);
                    if (item.Check.OneLiner != null)
                        cmd.Parameters.AddWithValue("@SigCut", item.Check.OneLiner.Code);                    
                    cmd.Parameters.AddWithValue("@Service_ID", (item.Check.FraudArmor == null || item.Check.FraudArmor.Code == null) ? string.Empty : item.Check.FraudArmor.Code);
                    if (orderRequest.ShippingAddress != null)
                        cmd.Parameters.AddWithValue("@Zipcode", orderRequest.ShippingAddress.PostalCode);
                }
                else // Accessory
                {
                    cmd.Parameters.AddWithValue("@Session_ID", sessionId);
                    cmd.Parameters.AddWithValue("@Check_Flag", 'N');
                    cmd.Parameters.AddWithValue("@Product_ID", item.Accessory.Code);
                    //cmd.Parameters.AddWithValue("@Cover", item.Check.); TODO: check about cover field  
                    cmd.Parameters.AddWithValue("@Ship_By", item.Accessory.ShippingOption == null || item.Accessory.ShippingOption.Code == '\0' ? '?' : item.Accessory.ShippingOption.Code);
                    if (item.Accessory.Quantity != null)
                        cmd.Parameters.AddWithValue("@Item_Count", item.Accessory.Quantity.Amount);
                    //if (item.Check.Font != null)
                    //    cmd.Parameters.AddWithValue("@Font_ID", item.Check.Font.Code);
                    //if (item.Check.Accent != null)
                    //    cmd.Parameters.AddWithValue("@Accent", item.Check.Accent.Code);
                    //if (item.Check.Background != null)
                    //    cmd.Parameters.AddWithValue("@Phantom", item.Check.Background.Code);
                    //if (item.Check.OneLiner != null)
                    //    cmd.Parameters.AddWithValue("@SigCut", item.Check.OneLiner.Code);
                    //if (item.Check.FraudArmor != null)
                    //    cmd.Parameters.AddWithValue("@Service_ID", item.Check.FraudArmor.Code);
                    if (orderRequest.ShippingAddress != null)
                        cmd.Parameters.AddWithValue("@Zipcode", orderRequest.ShippingAddress.PostalCode);
                    cmd.Parameters.AddWithValue("@Service_ID", string.Empty); // needed to avoid exception from SP
                }
                cmd.Parameters.AddWithValue("@URL_Session", orderRequest.Customer != null ? orderRequest.Customer.CustomerId : ""); // UI session For tracking
                AddOutputParameter("@Price_Status", SqlDbType.Int, cmd);
                AddOutputParameter("@Price_Message_Code", SqlDbType.Int, cmd);
                AddOutputParameter("@Price_Message", SqlDbType.VarChar, cmd, 80);
                AddOutputParameter("@Subtotal", SqlDbType.Int, cmd);
                AddOutputParameter("@Delivery", SqlDbType.Int, cmd);
                AddOutputParameter("@Tax", SqlDbType.Int, cmd);
                AddOutputParameter("@Product_Price", SqlDbType.Int, cmd);
                AddOutputParameter("@Cover_Price", SqlDbType.Int, cmd); // TODO: map this field
                AddOutputParameter("@Font_Price", SqlDbType.Int, cmd);
                AddOutputParameter("@Accent_Price", SqlDbType.Int, cmd);
                AddOutputParameter("@Phantom_Price", SqlDbType.Int, cmd);
                AddOutputParameter("@SigCut_Price", SqlDbType.Int, cmd);
                AddOutputParameter("@Service_Price", SqlDbType.Int, cmd);
                AddOutputParameter("@Actual_Billcode", SqlDbType.Char, cmd, 3);
                AddOutputParameter("@Total", SqlDbType.Int, cmd);
                AddOutputParameter("@FI_Total", SqlDbType.Int, cmd);
                var result = base.ExecuteStoredProcWithOutputParameters(cmd, out outParams);
                MapLineItemPrices(outParams, item, orderRequest);                
            }
            if (priceMessageCode == 3 && orderRequest.Total.Amount > 0)
            {
                var checkItem = orderRequest.Items.FirstOrDefault(x => x.Check != null);
                if (checkItem != null)
                    checkItem.PriceMessage = "This order will be partially paid by your Financial Institution.";
            }
            return null; // TODO: return validation errors
        }

        private void MapLineItemPrices(IList<SqlParameter> outParams, OrderItemDTO item, OrderDTO orderRequest)
        {
            if (item.Check != null && !string.IsNullOrWhiteSpace(item.Check.ProductId)) // check            {
            {
                item.PriceMessage = Convert.ToString(outParams.SingleOrDefault(x => x.ParameterName == "@Price_Message").Value);
                priceMessageCode = Convert.ToInt32(outParams.SingleOrDefault(x => x.ParameterName == "@Price_Message_Code").Value);
                item.Check.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Subtotal"));
                if (item.Check.ShippingOption != null && (item.Check.ShippingOption.Code != '\0' || item.Check.ShippingOption.Code != ' ')) // Do not return shipping price to UI if no shipping option selected
                {
                    item.Check.ShippingOption.Fee = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Delivery"));
                    orderRequest.TotalShipping = PopulatePrice(item.Check.ShippingOption.Fee.Amount, orderShipping);
                }
                else
                    orderRequest.TotalShipping = PopulatePrice(0, orderShipping);
                item.ItemSubtotal = PopulatePrice(item.Check.Price.Amount + (item.Check.ShippingOption == null ? 0 : item.Check.ShippingOption.Fee.Amount), null); //PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Cust_Total"));
                item.ItemTax = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Tax"));
                //item.ItemShipping = item.Check.ShippingOption.Fee;
                if(item.Check.Font != null)
                    item.Check.Font.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Font_Price"));
                if (item.Check.Accent != null)
                    item.Check.Accent.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Accent_Price"));
                if (item.Check.Background != null)
                    item.Check.Background.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Phantom_Price"));
                if (item.Check.OneLiner != null)
                    item.Check.OneLiner.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@SigCut_Price"));
                if (item.Check.FraudArmor != null)
                    item.Check.FraudArmor.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Service_Price"));
                orderRequest.SubTotal = PopulatePrice(item.Check.Price.Amount, orderSubtotal);
                
            }
            else
            {
                item.Accessory.Price = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Subtotal"));
                if (item.Accessory.ShippingOption != null)
                {
                    item.Accessory.ShippingOption.Fee = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Delivery"));
                    orderRequest.TotalShipping = PopulatePrice(item.Accessory.ShippingOption.Fee.Amount, orderShipping);
                }
                else
                    orderRequest.TotalShipping = PopulatePrice(0, orderShipping);
                item.ItemSubtotal = PopulatePrice(item.Accessory.Price.Amount + (item.Accessory.ShippingOption == null ? 0 : item.Accessory.ShippingOption.Fee.Amount), null);
                item.ItemTax = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Tax"));
                //item.ItemShipping = item.Accessory.ShippingOption.Fee;
                orderRequest.SubTotal = PopulatePrice(item.Accessory.Price.Amount, orderSubtotal);                
            }
            item.ActualBillCode = Convert.ToString(outParams.SingleOrDefault(x => x.ParameterName == "@Actual_Billcode").Value); // cache in ESB
            item.ItemTotal = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@Total")); // cache in ESB            
            orderRequest.Tax = PopulatePrice(item.ItemTax.Amount, ordertax);            
            orderRequest.Total = PopulatePrice(item.ItemSubtotal.Amount + item.ItemTax.Amount, orderTotal);
            item.FITotal = PopulatePrice(outParams.SingleOrDefault(x => x.ParameterName == "@FI_Total"));
        }
                

        public override Order PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return null;
        }

        private MoneyDTO PopulatePrice(SqlParameter outParam)
        {
            var objPrice = new MoneyDTO();
            if (outParam != null && outParam.Value != DBNull.Value)
            {
                objPrice.Amount = Convert.ToDouble(Convert.ToInt32(outParam.Value) / 100.00);
            }
            return objPrice;
        }
        private MoneyDTO PopulatePrice(double amount, MoneyDTO objPrice)
        {
            if (objPrice != null)
            {
                objPrice.Amount += amount; 
            }
            else
            {
                var objTemp = new MoneyDTO();
                objTemp.Amount = amount;
                objPrice = objTemp;
            }
            objPrice.Amount = Math.Round(objPrice.Amount, 2); // rounded to 2 decimal places
            return objPrice;
        }
    }
}
