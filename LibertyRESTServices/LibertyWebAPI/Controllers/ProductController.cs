using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Liberty Product WebAPI service
    /// </summary>
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves detailed information from a Liberty product
        /// </summary>
        /// <param name="productId">The Id of the product to retrieve</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productId:product}")] // :product = ProductIdConstraint
        public IHttpActionResult GetProduct(string productId)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            if (string.IsNullOrWhiteSpace(productId))
                //BadRequest
                throw new ValidationException("ProductID can't be null or empty.");

            var productDto = _productService.GetProduct(productId, sessionId);

            if (productDto != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, product = productDto });
            else
                // NotFound
                throw new FailureException("Product not found in the database; please make sure you're passing a valid productId");
        }
    }
}