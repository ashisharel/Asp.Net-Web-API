using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Institution;
using LibertyWebAPI.ErrorHelper;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{

    /// <summary>
    /// Institution/Login Web.API service
    /// </summary>
    public class InstitutionController : ApiController
    {
        private readonly IInstitutionService _institutionService;

        /// <summary>
        /// InstitutionController constructor
        /// </summary>
        /// <param name="institutionService"></param>
        public InstitutionController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;
        }

        /// <summary>
        /// Retrieves the Institution details from Liberty
        /// </summary>
        /// <param name="institutionRequest"></param>
        /// <returns></returns>
        /// </example>
        [Route("api/institution")]
        [HttpPost]
        public IHttpActionResult GetIntitutionDetails([FromBody]InstitutionRequestDTO institutionRequest)
        {
            var requestIsValid = false;

            if (institutionRequest != null)
            {
                if (institutionRequest.FOAFlag == null || institutionRequest.FOAFlag.Value == false)
                {
                    // regular institution validation
                    if (!string.IsNullOrWhiteSpace(institutionRequest.RtNumber) && !string.IsNullOrWhiteSpace(institutionRequest.AccountNumber))
                        requestIsValid = true;

                    if (!requestIsValid && !string.IsNullOrWhiteSpace(institutionRequest.Payload))
                        requestIsValid = true;
                }
                else
                {
                    // FOA institution validation
                    if (!string.IsNullOrWhiteSpace(institutionRequest.FOAAbaBrId))
                        requestIsValid = true;
                }
            }
            
            if (!requestIsValid)
                //BadRequest
                throw new ValidationException("Request, routing and account# or payload can't be null or empty.");

            var response = _institutionService.GetInstitution(institutionRequest);

            if (response.Institution != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, institution = response.Institution });
            else
            {
                if (institutionRequest.FOAResult != null && !string.Equals(institutionRequest.FOAResult.Result, "pass", StringComparison.OrdinalIgnoreCase)) //Skip error for 2nd Institution call in FOA flow if Idology fails
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
                                Code = "LIB1010",
                                Description = "No data returned because Idology failed."                                
                            }
                        }
                        }
                    });
                }
                else if (response.LoginSession != null && response.LoginSession.StatusDetail == "208")
                {
                    throw new InvalidZipcodeException(response.LoginSession.StatusDetail, response.LoginSession.Message);
                }
                else
                {
                    throw new FailureException(response.LoginSession != null ? response.LoginSession.Message : "Unknown error.");
                    
                }
            }
        }
    }
}