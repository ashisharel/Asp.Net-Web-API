using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Net;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// Fetch the customers last order, repriced, and with suitable substitutes for any unavailable products. 
    /// This service fetches the full details the last order, repriced, with any unavailable products 
    /// substituted with suitable replacements.
    /// </summary>
    [RoutePrefix("api/order")]
    public class LastRepricedController : ApiController
    {
        private readonly ILastRepricedService _orderService;
        public LastRepricedController(ILastRepricedService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Prepare an order with the Liberty sessionId provided in the request header
        /// </summary>
        /// <returns></returns>
        [Route("lastrepriced")]
        [HttpGet]
        public IHttpActionResult GetLastRepriced()
        {
            var response = new ResponseDTO() { Status = "SUCCESS" };
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
            {
                //BadRequest
                response = RequestHelper.InvalidSessionIdResponse();
                return Ok(new { responseSummary = response });
            }

                var order = _orderService.GetLastRepriced(sessionId);

                if (order != null)
            {
                //Ok
                return Ok(new { responseSummary = response, order });
            }
            {
                // NotFound
                response = RequestHelper.CreateNotFoundResponse("Could not retrieve last order.");
                return Ok(new { responseSummary = response });
            }
        }
    }
}
