using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Web.Http;
using System.Linq;
using LibertyWebAPI.Filters;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Order Submit Controller
    /// </summary>
    [RoutePrefix("api/order")]
    public class OrderSubmitController : ApiController
    {
        private readonly IOrderSubmitService _orderSubmitService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="orderSubmitService"></param>
        public OrderSubmitController(IOrderSubmitService orderSubmitService)
        {
            _orderSubmitService = orderSubmitService;
        }

        /// <summary>
        /// Submits order
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ValidateOrderSubmitModelAttribute]
        public IHttpActionResult OrderSubmit([FromBody] OrderDTO orderRequest)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            if (orderRequest == null || orderRequest.Items == null || !orderRequest.Items.Any())
                throw new ValidationException("You must provide an order with items!");

            var response = _orderSubmitService.OrderSubmit(sessionId, orderRequest);

            if (response != null)
                //Ok
                return Ok(new { 
                    responseSummary = new ResponseDTO() { Status = "SUCCESS" }, 
                    orderDate = response.orderDate.Value.ToString("MM/dd/yyyy"),
                    ACHDelay = response.ACHDelay,
                    ServiceIndicator = response.ServiceIndicator
                });
            else
                //NotFound
                throw new FailureException("Failure submitting the order!");
        }
    }
}