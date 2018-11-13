using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using WebApi.OutputCache.V2;

namespace LibertyWebAPI.Utilities
{
    public class CustomCacheKeyGenerator : DefaultCacheKeyGenerator
    {
        public override string MakeCacheKey(HttpActionContext context, MediaTypeHeaderValue mediaType, bool excludeQueryString = false)
        {
            return context.ActionDescriptor.ActionName;
        }
    }
}