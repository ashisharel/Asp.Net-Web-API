using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Linq;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Liberty Catalog Service, returns the list of product categories available for the customer
    /// </summary>
    [RoutePrefix("api/product/catalog")]
    public class CatalogController : ApiController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        /// <summary>
        /// Given the sessionId, this method returns the list of categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCatalog()
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            var catalog = _catalogService.GetCatalog(sessionId);

            if (catalog != null && catalog.Items != null && catalog.Items.Any())
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, catalog });
            else
                // NotFound
                throw new FailureException("Catalog not found.");
        }
    }
}