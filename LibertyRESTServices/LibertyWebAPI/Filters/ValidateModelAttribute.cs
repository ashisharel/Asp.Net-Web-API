using LibertyWebAPI.ErrorHelper;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LibertyWebAPI.Filters
{
    /// <summary>
    /// Validates the request DTOs as per the attributes attached to the properties
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {       
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var objErrors = new List<Message>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                            objErrors.Add(new Message() { Code = "LIB1003", Text = error.ErrorMessage, Description = error.ErrorMessage });

                        if (error.Exception != null)
                            objErrors.Add(new Message() { Code = "LIB1003", Text = error.Exception.Message, Description = error.Exception.Message });
                    }
                }
                throw new ValidationException(objErrors);
            }
        }
    }
}