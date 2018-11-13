using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.FOA;
using LibertyWebAPI.ErrorHelper;
using System.Web.Http;

namespace LibertyWebAPI.Controllers
{
    /// <summary>
    ///  Save FOA (First Order Acceptance)
    /// </summary>
    public class FOASaveController : ApiController
    {
        private readonly IFOASaveService _foaSaveService;

        /// <summary>
        /// FOASaveController constructor
        /// </summary>
        /// <param name="foaSaveService"></param>
        public FOASaveController(IFOASaveService foaSaveService)
        {
            _foaSaveService = foaSaveService;
        }

        /// <summary>
        /// Saves FOA request and returns SessionID
        /// </summary>
        /// <param name="foaSaveDto"></param>
        /// <returns></returns>
        [Route("api/foa/result")]
        [HttpPost]
        public IHttpActionResult Save([FromBody]FOASaveDTO foaSaveDto)
        {
            if (foaSaveDto == null)
                //BadRequest
                throw new ValidationException("FOA request or payload can't be null or empty.");

            var foaSession = _foaSaveService.Save(foaSaveDto);

            if (foaSession != null)
                //Ok
                return Ok(new { responseSummary = new ResponseDTO() { Status = "SUCCESS" }, PreAuthSession = foaSession });
            else
                //failure
                throw new FailureException("Couldn't create FOA session");
        }
    }
}