using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using LibertyWebAPI.Filters;
using LibertyWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// (Re)calculate the prices of the various line items, shipping options, taxes etc. of the order.
    /// </summary>
    [RoutePrefix("api/order")]
    public class OrderPriceController : ApiController
    {
        private readonly IOrderPricingService _orderPricingService;
        public OrderPriceController(IOrderPricingService orderService)
        {
            _orderPricingService = orderService;
        }

        /// <summary>
        /// Takes in the order object, (re)calculates the prices and returns the order object with updated prices
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [Route("price")]
        [HttpPost]
        [ValidatePricingModel]
        public IHttpActionResult GetOrderPrice([FromBody] OrderDTO orderRequest)
        {
            var response = new ResponseDTO() { Status = "SUCCESS" };
            int sessionId = RequestHelper.GetSessionIdFromHeader(Request.Headers);

            if (sessionId == -1)
            {
                //BadRequest
                response = RequestHelper.InvalidSessionIdResponse();
                return Ok(new { responseSummary = response });
            }
            
            var priceConfirmation = _orderPricingService.GetOrderPrice(orderRequest, sessionId);

            if (priceConfirmation != null)
            {
                //Ok
                return Ok(new { responseSummary = response, priceConfirmation });
            }
            {
                // NotFound
                response = RequestHelper.CreateNotFoundResponse("Unable to get order pricing details.");
                return Ok(new { responseSummary = response });
            }
        }
    }
}
