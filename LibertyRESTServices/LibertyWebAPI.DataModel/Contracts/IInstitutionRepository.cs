using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Institution;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IInstitutionRepository
    {
        InstitutionResponse GetInstitution(InstitutionRequestDTO institutionRequest);
    }

    public class LoginSession
    {
        public int? SessionID { get; set; }
        public string Status { get; set; }
        public string StatusDetail { get; set; }
        public string Message { get; set; }
        public string AllowRetryFlag { get; set; }
        public string FOAFlag { get; set; }
        public string FOADefaultAbabrID { get; set; }
        public string FOADefaultAcctType { get; set; }
        public string FOALogo { get; set; }
        public string FOART { get; set; }
        public string FOAAccount { get; set; }
        public string AnalyticsName { get; set; }
    }

    public struct InstitutionResponse
    {
        public LoginSession loginSession { get; set; }
        public Institution Institution { get; set; }
    }

}
