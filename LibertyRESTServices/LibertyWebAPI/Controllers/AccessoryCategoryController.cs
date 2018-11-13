using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Filters;
using LibertyWebAPI.Utilities;
using System;
using System.Configuration;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Liberty Product Accessory Category WebAPI service
    /// </summary>
    [RoutePrefix("api/product")]    
    public class AccessoryCategoryController : ApiController
    {
        private readonly IAccessoryCategoryService _accessoryCategoryService;
        //private static int cacheDuration = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["CacheDuration"]) ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["CacheDuration"]);
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="accessoryCategoryService"></param>
        public AccessoryCategoryController(IAccessoryCategoryService accessoryCategoryService)
        {
            _accessoryCategoryService = accessoryCategoryService;
        }

        /// <summary>
        /// Get accent symbols for the indicated product
        /// </summary>
        /// <param name="productId">the liberty product unique identifier</param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("accentsymbols/{productId}/{categoryId}")]
        [Route("accentsymbols/{productId}")]
        [CacheOutput(ServerTimeSpan = 7200, CacheKeyGenerator = typeof(CustomCacheKeyGenerator))] // ServerTimeSpan is in seconds
        public IHttpActionResult GetProductAccents(string productId, string categoryId = "null")
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                throw new InvalidSessionIdException();

            var accentSymbolsDto = _accessoryCategoryService.GetProductAccents(productId, categoryId, sessionId);

            if (accentSymbolsDto != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, accentSymbols = accentSymbolsDto });
            else
                // NotFound
                throw new FailureException("Accent Symbols not found in the database; make sure you're passing valid parameters.");
        }

        /// <summary>
        /// Get Backgrounds = Center Accents = Phantoms for the indicated product.
        /// </summary>
        /// <param name="productId">The product identifier for the check product.</param>
        /// <param name="categoryId">The code of the category used to filter. Use 'all' if there is no filtering using categories.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("backgrounds/{productId}/{categoryId}")]
        [Route("backgrounds/{productId}")]
        [CacheOutput(ServerTimeSpan = 7200, CacheKeyGenerator = typeof(CustomCacheKeyGenerator))] // ServerTimeSpan is in seconds
        public IHttpActionResult GetProductBackgrounds(string productId, string categoryId = "null")
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                throw new InvalidSessionIdException();

            var backgroundsDto = _accessoryCategoryService.GetProductBackgrounds(productId, categoryId, sessionId);

            if (backgroundsDto != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, backgrounds = backgroundsDto });
            else
                // NotFound
                throw new FailureException("Backgrounds not found in the database; make sure you're passing valid parameters.");
        }

        /// <summary>
        /// Get OneLiners (expressions) for the indicated product.
        /// </summary>
        /// <param name="productId">The product identifier for the check product.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("oneliners/{productId}")]
        [CacheOutput(ServerTimeSpan = 7200, CacheKeyGenerator = typeof(CustomCacheKeyGenerator))] // ServerTimeSpan is in seconds
        public IHttpActionResult GetProductOneliners(string productId)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                throw new InvalidSessionIdException();

            var oneLinersDto = _accessoryCategoryService.GetProductOneliners(productId, sessionId);

            if (oneLinersDto != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, oneliners = oneLinersDto });
            else
                // NotFound
                throw new FailureException("One-liners not found in the database; make sure you're passing valid parameters.");
        }
    }
}