using LibertyWebAPI.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace LibertyWebAPI.Filters
{
    /// <summary>
    /// Log request and response JOSN, along with elapsed time for all the service calls
    /// </summary>
    public class RequestResponseLoggingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Log Request JSON
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {            
            var stopwatch = new Stopwatch(); // Create stopwatch for tracking the time taken to execute the services
            filterContext.Request.Properties["Stopwatch"] = stopwatch; 
            stopwatch.Start();
            
            filterContext.Request.Properties["Trace"] = string.Format(
                "Controller : {0}" + Environment.NewLine + "Action : {1}" + Environment.NewLine + "Request : {2}", 
                filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName, 
                filterContext.ActionDescriptor.ActionName, filterContext.ActionArguments.ToJSON());
        }
        /// <summary>
        /// Log Response JSON
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {            
            var stopwatch = (Stopwatch)context.Request.Properties["Stopwatch"];
            stopwatch.Stop();
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            var traceText = Convert.ToString(context.Request.Properties["Trace"]);
            var response = string.Empty;
            if (context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName == "AccessoryCategory")
                response = "Skipped";
            else
                response = context.Response!= null && context.Response.Content != null ? Convert.ToString(context.Response.Content.ReadAsStringAsync().Result) : "null";
            traceText = traceText + Environment.NewLine + "Response : " + response + Environment.NewLine + "Elapsed Time in milliseconds : " + stopwatch.ElapsedMilliseconds.ToString();
            trace.Info(context.Request, traceText, "", context.Response != null ? context.Response.Content : null); 
            
        }
    }
}