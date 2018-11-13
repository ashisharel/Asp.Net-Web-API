using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.FOA;
using LibertyWebAPI.ErrorHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    public class FOAAccountValidationController : ApiController
    {
        private readonly IFOAAccountValidationService _foaAccountValidationService;

        /// <summary>
        /// FOASaveController constructor
        /// </summary>
        /// <param name="foaSaveService"></param>
        public FOAAccountValidationController(IFOAAccountValidationService foaAccountValidationService)
        {
            _foaAccountValidationService = foaAccountValidationService;
        }

        /// <summary>
        /// Saves FOA request and returns SessionID
        /// </summary>
        /// <param name="foaSaveDto"></param>
        /// <returns></returns>
        [Route("api/foa/accountvalidation")]
        [HttpPost]
        public IHttpActionResult ValidateAccount([FromBody]FOAAccountValidationRequestDTO foaAccountDto)
        {
            if (foaAccountDto == null)
                //BadRequest
                throw new ValidationException("FOA request or payload can't be null or empty.");
            int resultCode;
            string message;
            _foaAccountValidationService.ValidateFOAAccount(foaAccountDto, out resultCode, out message);

            if (resultCode == 0)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, isValid = true });
            else
                //failure
                throw new FailureException(resultCode + "| " + message);
        }
    }
}
