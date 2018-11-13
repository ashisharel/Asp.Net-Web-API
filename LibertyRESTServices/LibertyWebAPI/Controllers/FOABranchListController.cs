using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.FOA;
using LibertyWebAPI.ErrorHelper;
using LibertyWebAPI.Utilities;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    /// FOA Branch List Controller
    /// </summary>
    [RoutePrefix("api/foa")]
    public class FOABranchListController : ApiController
    {
        private readonly IFOABranchListService _foaBranchListService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="foaBranchListService"></param>
        public FOABranchListController(IFOABranchListService foaBranchListService)
        {
            _foaBranchListService = foaBranchListService;
        }

        /// <summary>
        /// Get FOA Branch List
        /// </summary>
        /// <returns></returns>
        [Route("branches")]
        [HttpPost]
        public IHttpActionResult GetBranchList([FromBody]BranchRequestDTO branchRequest)
        {
            if (branchRequest == null || string.IsNullOrWhiteSpace(branchRequest.RtNumber) || string.IsNullOrWhiteSpace(branchRequest.AccountNumber))
                throw new ValidationException("Request, routing and account# can't be null or empty.");

            var foaBranchList = _foaBranchListService.GetBranchList(branchRequest);

            if (foaBranchList != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, FOABranchList = foaBranchList });
            else
                //NotFound
                throw new FailureException("No FOA Branches were found!");
        }
    }
}