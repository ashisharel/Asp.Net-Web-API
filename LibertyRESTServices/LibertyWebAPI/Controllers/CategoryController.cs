using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Product;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Filters;
using LibertyWebAPI.Utilities;
using System.Linq;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Gets the list of products for the specified categoryId
    /// </summary>
    [RoutePrefix("api/product/category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Returns a list of products for the specified categoryId, 
        /// the # of products returned is driven by the maxCount field in the request object
        /// </summary>
        /// <param name="categoryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ValidateModel]
        public IHttpActionResult GetCategoryItems([FromBody] CategoryRequestDTO categoryRequest)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            var categoryItems = _categoryService.GetCategoryItems(categoryRequest, sessionId);

            if (categoryItems != null && categoryItems.Any())
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, categories = categoryItems });
            else
                //NotFound
                throw new FailureException("No products found for this categoryId.");
        }
    }
}