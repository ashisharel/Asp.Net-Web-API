using LibertyWebAPI.DTO.Order;
using LibertyWebAPI.ErrorHelper;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LibertyWebAPI.Filters
{
    /// <summary>
    /// Validates an Order right before it gets submitted to the database
    /// </summary>
    public class ValidateOrderSubmitModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var orderRequest = (OrderDTO)actionContext.ActionArguments["orderRequest"];
            var objErrors = new List<Message>();

            if (orderRequest == null)
            {
                var errorText = "The order object is missing.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                // do not continue
                throw new ValidationException(objErrors);
            }

            if (orderRequest.ShippingAddress == null ||
                (
                string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.ShipLine1) &&
                string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.ShipLine2) &&
                string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.ShipLine3) &&
                string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.ShipLine4) &&
                string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.ShipLine5)
                ))
            {
                var errorText = "Order shipping address is missing.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = "Please enter your valid shipping address.", Description = errorText });
            }
            if (orderRequest.ShippingAddress == null || (string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.City)))
            {
                var errorText = "Order shipping city is missing.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = "Please enter your valid shipping address.", Description = errorText });
            }
            if (orderRequest.ShippingAddress == null || (string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.State)))
            {
                var errorText = "Order shipping state is missing.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = "Please enter your valid shipping address.", Description = errorText });
            }
            if (orderRequest.ShippingAddress == null || (string.IsNullOrWhiteSpace(orderRequest.ShippingAddress.PostalCode)))
            {
                var errorText = "Order shipping postal code is missing.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = "Please enter your valid shipping address.", Description = errorText });
            }
            if (orderRequest.Items == null || orderRequest.Items.Count == 0)
            {
                var errorText = "The order must have items.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
            }
            if (orderRequest.Customer == null || string.IsNullOrWhiteSpace(orderRequest.Customer.EmailAddress))
            {
                var errorText = "Email Address is missing.";
                objErrors.Add(new Message() { Code = "LIB1003", Text = "Please enter a valid email address.", Description = errorText });
            }

            foreach (var item in orderRequest.Items)
            {
                if (item.Check == null && item.Accessory == null)
                {
                    var errorText = "You must add a Check or an Accessory.";
                    objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                }

                if (item.Check != null && !string.IsNullOrWhiteSpace(item.Check.ProductId) && 
                    item.Accessory != null && !string.IsNullOrWhiteSpace(item.Accessory.Code) )
                {
                    var errorText = "Check and Accessory can't be together in the same order item line.";
                    objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                }

                if (item.Check != null)
                {
                    if (string.IsNullOrWhiteSpace(item.Check.ProductId))
                    {
                        var errorText = "Check product Id is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                    }
                    if (item.Check.StartAt == null || item.Check.StartAt == 0)
                    {
                        var errorText = "Check start number is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                    }
                    if (item.Check.Quantity == null || item.Check.Quantity.Amount == 0)
                    {
                        var errorText = "Check quantity is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                    }
                    if (item.Check.Font == null || string.IsNullOrWhiteSpace(item.Check.Font.Code))
                    {
                        var errorText = "Check font code is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                    }
                    //if (item.Check.FraudArmor == null || string.IsNullOrWhiteSpace(item.Check.FraudArmor.Code))
                    //{
                    //    var errorText = "Check FraudArmor code is missing.";
                    //    objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                    //} // not needed, Fraud Armor can be null if not opted for
                    if (item.Check.Personalization == null ||
                        (
                        (item.Check.Personalization.PersLine1 == null || string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine1.Text)) &&
                        (item.Check.Personalization.PersLine2 == null || string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine2.Text)) &&
                        (item.Check.Personalization.PersLine3 == null || string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine3.Text)) &&
                        (item.Check.Personalization.PersLine4 == null || string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine4.Text)) &&
                        (item.Check.Personalization.PersLine5 == null || string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine5.Text)) &&
                        (item.Check.Personalization.PersLine6 == null || string.IsNullOrWhiteSpace(item.Check.Personalization.PersLine6.Text))
                        ))
                    {
                        var errorText = "Check personalization is missing";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = "Please enter your personalization information.", Description = errorText });
                    }

                    if (item.Check.ShippingOption == null || (item.Check.ShippingOption != null && item.Check.ShippingOption.Code == '\0'))
                    {
                        var errorText = "Check Shipping option is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = "Please select a shipping method.", Description = errorText });
                    }
                }
                else if (item.Accessory != null)
                {
                    if (string.IsNullOrWhiteSpace(item.Accessory.Code))
                    {
                        var errorText = "Accessory product code is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                    }
                    if (item.Accessory.Quantity == null || item.Accessory.Quantity.Amount == 0)
                    {
                        var errorText = "Accessory quantity is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = string.Empty, Description = errorText });
                    }
                    if (item.Accessory.ShippingOption == null || (item.Accessory.ShippingOption != null && item.Accessory.ShippingOption.Code == '\0'))
                    {
                        var errorText = "Accessory Shipping option is missing.";
                        objErrors.Add(new Message() { Code = "LIB1003", Text = "Please select a shipping method.", Description = errorText });
                    }
                    //if (item.Accessory.Personalization == null || // should only validate personalizable accessories
                    //    (
                    //    (item.Accessory.Personalization.PersLine1 == null || string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine1.Text)) &&
                    //    (item.Accessory.Personalization.PersLine2 == null || string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine2.Text)) &&
                    //    (item.Accessory.Personalization.PersLine3 == null || string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine3.Text)) &&
                    //    (item.Accessory.Personalization.PersLine4 == null || string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine4.Text)) &&
                    //    (item.Accessory.Personalization.PersLine5 == null || string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine5.Text)) &&
                    //    (item.Accessory.Personalization.PersLine6 == null || string.IsNullOrWhiteSpace(item.Accessory.Personalization.PersLine6.Text))
                    //    ))
                    //{
                    //    var errorText = "Accessory personalization is missing";
                    //    objErrors.Add(new Message() { Code = "LIB1003", Text = errorText, Description = errorText });
                    //} // moved the accessory personalization logic to the cart save proc
                }
            }

            if (objErrors.Count > 0)
                throw new ValidationException(objErrors);
        }
    }
}