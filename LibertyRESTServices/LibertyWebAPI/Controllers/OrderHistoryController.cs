using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Order History Controller
    /// </summary>
    [RoutePrefix("api/order")]
    public class OrderHistoryController : ApiController
    {
        private readonly IOrderHistoryService _orderHistoryService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="orderHistoryService"></param>
        public OrderHistoryController(IOrderHistoryService orderHistoryService)
        {
            _orderHistoryService = orderHistoryService;
        }

        /// <summary>
        /// Get Historical Orders
        /// </summary>
        /// <returns></returns>
        [Route("history")]
        [HttpGet]
        public IHttpActionResult GetOrderHistory(string maxCount = null)
        {
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
                //BadRequest
                throw new InvalidSessionIdException();

            var orderHistory = _orderHistoryService.GetOrderHistory(sessionId, RequestHelper.TryParse2(maxCount));
            if (orderHistory != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, orderSummaries = orderHistory });
            else // throw error if no history found but do not log in event log
            {
                return Ok(new
                {
                    responseSummary = new ResponseDTO()
                    {
                        Status = "FAILURE",
                        Messages = new List<MessageDTO>()
                        {
                            new MessageDTO()
                            {
                                Code = "LIB1009",
                                Description = "No Orders Found.",
                                Text = "No Orders Found."
                            }
                        }
                    }
                });


            }
        }
    }
}