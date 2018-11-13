using LibertyWebAPI.DTO.Order;
using LibertyWebAPI.ErrorHelper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LibertyWebAPI.Filters
{
    /// <summary>
    /// Validates the order before the pricing call
    /// </summary>
    public class ValidatePricingModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var orderRequest = (OrderDTO)actionContext.ActionArguments["orderRequest"];
            var objErrors = new List<Message>();
            if (orderRequest == null)
            {
                var errorText = "Request cannot be null or empty.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
            }
            else
            {
                foreach (var item in orderRequest.Items)
                {
                    if (item.Check != null)
                    {
                        if (string.IsNullOrWhiteSpace(item.Check.ProductId))
                        {
                            var errorText = "Check product Id is missing.";
                            objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                        }
                        if (item.Check.Quantity == null || item.Check.Quantity.Amount == 0)
                        {
                            var errorText = "Check quantity is missing.";
                            objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                        }
                        //if (item.Check.ShippingOption != null && item.Check.ShippingOption.Code == '\0')
                        //{
                        //    var errorText = "Check Shipping option code is missing.";
                        //    objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                        //}
                    }
                    else if (item.Accessory != null)
                    {
                        if (string.IsNullOrWhiteSpace(item.Accessory.Code))
                        {
                            var errorText = "Accessory product code is missing.";
                            objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                        }
                        if (item.Accessory.Quantity == null || item.Accessory.Quantity.Amount == 0)
                        {
                            var errorText = "Accessory quantity is missing.";
                            objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                        }
                        //if (item.Accessory.ShippingOption != null && item.Accessory.ShippingOption.Code == '\0')
                        //{
                        //    var errorText = "Accessory Shipping option code is missing.";
                        //    objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                        //}
                    }
                }
            }
            if (objErrors.Any())
                throw new ValidationException(objErrors);
        }
    }
}