using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Filters;
using LibertyWebAPI.Utilities;
using System.Linq;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Get Shipping Options for the Product 
    /// </summary>
    [RoutePrefix("api/shippingoptions")]
    public class ShippingOptionsController : ApiController
    {
        private readonly IShippingOptionsService _shippingOptionsService;

        public ShippingOptionsController(IShippingOptionsService shippingOptionsService)
        {
            _shippingOptionsService = shippingOptionsService;
        }

        /// <summary>
        /// Given the product and quantity, along with the shipping address, get the possible shipping options.
        /// </summary>
        /// <param name="shippinOptionRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productId}/{quantity}")]
        [ValidateModel]
        public IHttpActionResult GetShippingOptions([FromUri]ShippingOptionsRequestDTO shippinOptionRequest)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            var shippingOptions = _shippingOptionsService.GetShippingOptions(shippinOptionRequest, sessionId);

            if (shippingOptions != null && shippingOptions.Any())
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, shippingOptions });
            else
                //NotFound
                throw new FailureException("No shipping options found for this product.");
        }
    }
}