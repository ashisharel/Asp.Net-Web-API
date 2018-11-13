using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace LibertyWebAPI.Utilities
{
    /// <summary>
    /// constraint to be used in the Product Api Service for the productId parameter
    /// </summary>
    public class ProductIdConstraint : IHttpRouteConstraint
    {
        /// <summary>
        /// calls the Product Service if the productId param is not "category" or "catalog"
        /// </summary>
        /// <param name="request"></param>
        /// <param name="route"></param>
        /// <param name="parameterName"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value))
            {
                var valueString = value.ToString();
                // returns "true" if the productId is not equal to "category" or "catalog"
                return !(valueString.Equals("category", StringComparison.InvariantCultureIgnoreCase) || valueString.Equals("catalog", StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }
    }
}