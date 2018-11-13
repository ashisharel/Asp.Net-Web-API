using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace LibertyWebAPI.Filters
{
    /// <summary>
    /// Action filter to handle Global application errors.
    /// Logs the error and creates error response which is sent to the Client
    /// </summary>
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());

            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            var exceptionType = context.Exception.GetType();
            //Log the error
            trace.Error(context.Request,
                "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine +
                "Action : " + context.ActionContext.ActionDescriptor.ActionName + Environment.NewLine +
                "Exception Type : " + exceptionType + Environment.NewLine +
                "Parameters : " + context.ActionContext.ActionArguments.ToJSON(), context.Exception);

            IResponseSummaryException responseSummaryException = null;
            //Create the error response for the client
            if (exceptionType == typeof(SqlException))
            {
                var sqlException = context.Exception as SqlException;
                if (sqlException != null)
                    responseSummaryException = new UnhandledException(sqlException);
            }
            else if (typeof(IResponseSummaryException).IsAssignableFrom(exceptionType))
            {
                responseSummaryException = context.Exception as IResponseSummaryException;
            }
            else
            {
                responseSummaryException = new UnhandledException(context.Exception);
            }

            if (responseSummaryException != null)
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.OK, new
                {
                    responseSummary = new ResponseDTO()
                        {
                            Status = responseSummaryException.Status,
                            Messages = responseSummaryException.Messages.Select(m => new MessageDTO()
                            {
                                Code = m.Code,
                                Description = m.Description,
                                Text = m.Text
                            }).ToList()
                        }
                }));
            }
        }
    }
}