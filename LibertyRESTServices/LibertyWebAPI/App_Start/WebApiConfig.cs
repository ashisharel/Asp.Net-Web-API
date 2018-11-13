using LibertyWebAPI.Filters;
using LibertyWebAPI.Utilities;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Routing;
using WebApi.OutputCache.V2;

namespace LibertyWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            if (ConfigurationManager.AppSettings["EnableRequestLogging"] == "Y")
                config.Filters.Add(new RequestResponseLoggingAttribute());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // Converts all PascalCase property names to camelCase in the response JSON
            config.Filters.Add(new GlobalExceptionAttribute());
            config.CacheOutputConfiguration().RegisterCacheKeyGeneratorProvider(() => new CustomCacheKeyGenerator());
            
            // Web API routes
            //config.MapHttpAttributeRoutes();
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("product", typeof(ProductIdConstraint)); // resolve routing constraint for endpoints with path starting with "api/product"
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}