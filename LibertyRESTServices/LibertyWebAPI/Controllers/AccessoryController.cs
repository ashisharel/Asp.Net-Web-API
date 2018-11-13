using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Liberty Product Accessory Category WebAPI service
    /// </summary>
    [RoutePrefix("api/product")]
    public class AccessoryController : ApiController
    {
        private readonly IAccessoryService _accessoryService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="accessoryService"></param>
        public AccessoryController(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }

        /// <summary>
        /// Get accessories for the indicated product.
        /// </summary>
        /// <param name="productId">The productId</param>
        /// <returns>list of accessories related to the product</returns>
        [HttpGet]
        [Route("accessories/{productId}")]
        public IHttpActionResult GetProductAccessories(string productId)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            var accessoryDto = _accessoryService.GetProductAccessories(productId, sessionId);

            if (accessoryDto != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, accessories = accessoryDto });
            else
                // NotFound
                throw new FailureException("Accessories not found in the database; please make sure you're passing a valid productId");
        }
    }
}