using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Institution;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class InstitutionRepository : BaseRepository<Institution>, IInstitutionRepository
    {
        private LoginSession sessionInfo;
        private static string StoredProcedureName = "p_PEP_Login";

        public InstitutionResponse GetInstitution(InstitutionRequestDTO institutionRequest)
        {
            SqlCommand cmd = new SqlCommand(StoredProcedureName);

            if (institutionRequest.FOAFlag == null || institutionRequest.FOAFlag == false)
            {
                cmd.Parameters.AddWithValue("@RT", institutionRequest.RtNumber ?? string.Empty);
                cmd.Parameters.AddWithValue("@Account", institutionRequest.AccountNumber ?? string.Empty);
                //cmd.Parameters.AddWithValue("@Order_Type", (institutionRequest.isBusiness != null && institutionRequest.isBusiness == true) ? "B" : "P"); // When we skip this param - if the account exists, use its info. Otherwise, both catalogs offered.
                cmd.Parameters.AddWithValue("@WebServer_Session_ID", null);
                cmd.Parameters.AddWithValue("@IP_Address", null);
                cmd.Parameters.AddWithValue("@Preauth_Session", institutionRequest.Payload);
                cmd.Parameters.AddWithValue("@Logon_Zip", institutionRequest.LogonZip);
            }
            else // FOA flow
            {
                cmd.Parameters.AddWithValue("@RT", string.Empty);
                cmd.Parameters.AddWithValue("@Account", string.Empty);
                //cmd.Parameters.AddWithValue("@Order_Type", (institutionRequest.isBusiness != null && institutionRequest.isBusiness == true) ? "B" : "P"); // When we skip this param - if the account exists, use its info. Otherwise, both catalogs offered.
                cmd.Parameters.AddWithValue("@Preauth_Session", institutionRequest.PreAuthSession);
                cmd.Parameters.AddWithValue("@WebServer_Session_ID", null);
                cmd.Parameters.AddWithValue("@IP_Address", null);
                cmd.Parameters.AddWithValue("@FOA_Ababr_ID", institutionRequest.FOAAbaBrId);
                cmd.Parameters.AddWithValue("@FOA_Acct_Type", institutionRequest.FOAAcctType);
            }
            var list = base.ExecuteStoredProc(cmd);
            var institution = list.FirstOrDefault();
            if (institution != null) institution.SessionID = sessionInfo.SessionID;
            InstitutionResponse response = new InstitutionResponse();
            response.Institution = institution;
            response.loginSession = sessionInfo;
            return response;
        }

        public override Institution PopulateRecord(IDataReader reader, int dataSetCount)
        {
            if (dataSetCount == 1)
            {
                // Create login session object from first DataSet
                sessionInfo = new LoginSession()
                {
                    SessionID = (int?)(reader["Session_ID"].Equals(DBNull.Value) ? 0 : reader["Session_ID"]),
                    Status = reader["Status"].ToString(),
                    StatusDetail = reader["StatusDetail"].ToString(),
                    Message = reader["Message"].ToString(),
                    AllowRetryFlag = reader["Allow_Retry_Flag"].ToString(),
                    FOAFlag = reader["FOA_Flag"].ToString(),
                    FOADefaultAbabrID = reader["FOA_Default_Ababr_ID"].ToString(),
                    FOADefaultAcctType = reader["FOA_Default_AcctType"].ToString(),
                    FOALogo = reader["FOA_Titleplate_Logo"].ToString(),
                    FOART = reader["FOA_RT"].ToString(),
                    FOAAccount = reader["FOA_Acct"].ToString(),
                    AnalyticsName = reader["AnalyticsClientName"].ToString()
                };
                // can't return "Institution"; return "null" to prevent object from been added to base list
                return null;
            }
            else if (dataSetCount == 2)
            {
                // this should be the "Institution" dataset
                return new Institution()
                {
                    Name = reader["Name"].ToString(),
                    //AnalyticsName = reader["AnalyticsName"].ToString(),
                    TransitRouting = reader["TransitRouting"].ToString(),
                    Account = reader["Account"].ToString(),
                    ClubCode = reader["ClubCode"].ToString(),
                    ClubName = reader["ClubName"].ToString(),
                    BranchCode = reader["BranchCode"].ToString(),
                    Logo = reader["Logo"].ToString(),
                    ClientHomePage = reader["ClientHomePage"].ToString(),
                    CustomerServiceNumber = reader["customerServiceNumber"].ToString(),
                    IsAllowedCheckOrder = reader["isAllowedCheckOrder"].ToString(),
                    IsAllowedOrderStatus = reader["isAllowedOrderStatus"].ToString(),
                    IsAllowedProductChange = reader["isAllowedProductChange"].ToString(),
                    IsAllowedEditCheckNumber = reader["isAllowedEditCheckNumber"].ToString(),
                    IsAllowedEnterPromoCode = reader["isAllowedEnterPromoCode"].ToString(),
                    IsAllowedEditPersonalizationNames = reader["isAllowedEditPersonalizationNames"].ToString(),
                    IsAllowedEditPersonalizationAddress = reader["isAllowedEditPersonalizationAddress"].ToString(),
                    IsAllowedEditShipping = reader["isAllowedEditShipping"].ToString(),
                    IsAllowedEnterForeignShipping = reader["isAllowedEnterForeignShipping"].ToString(),
                    IsPreselectShippingOption = reader["isPreselectShippingOption"].ToString(),
                    IsDefaultShippingOption = reader["isDefaultShippingOption"].ToString(),
                    IsPricingAvailable = reader["isPricingAvailable"].ToString(),
                    IsRequiredEmail = reader["isRequiredEmail"].ToString(),
                    IsBusinessAccount = reader["isBusinessAccount"].ToString(),
                    AccessoriesAllowed = reader["accessoriesAllowed"].ToString(),
                    ForcePersonalizationVerification = reader["forcePersonalizationVerification"].ToString(),
                    IntegrationKeepAliveURL = reader["integrationKeepAliveURL"].ToString(),
                    IntegrationKillClientSessionURL = reader["integrationKillClientSessionURL"].ToString(),
                    ShowSurvey = reader["showSurvey"].ToString(),
                    ShowPersonalization = reader["showPersonalization"].ToString(),
                    ShowPersonalizationCompare = reader["ShowPersonalizationCompare"].ToString(),
                    ShowShippingAddress = reader["ShowShippingAddress"].ToString(),
                    ShowEmail = reader["ShowEmail"].ToString(),
                    ShowIFrame = reader["ShowIFrame"].ToString(),
                    ShowIFrameInsecure = reader["ShowIFrameInsecure"].ToString(),
                    ShowPhoneNumberField = reader["ShowPhoneNumberField"].ToString(),
                    StyleColor1 = reader["styleColor1"].ToString(),
                    Channel = reader["channel"].ToString(),
                    PoweredBy = reader["poweredBy"].ToString(),
                    BackgroundImage = reader["backgroundImage"].ToString(),
                    BackgroundTextImage = reader["backgroundTextImage"].ToString(),
                    CatalogID = reader["catalogID"].ToString(),
                    CustomCatalogId = reader["customCatalogId"].ToString(),
                    Address1 = reader["Address1"].ToString(),
                    Address2 = reader["Address2"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    Zip = reader["Zip"].ToString(),
                    MinCheckNumber = reader["MinCheckNumber"].ToString(),
                    MaxCheckNumber = reader["MaxCheckNumber"].ToString(),
                    ShippingLinesMaxLength = reader["ShippingLinesMaxLength"].ToString(),
                    EmailMaxLength = reader["EmailMaxLength"].ToString(),
                    IFrameHostURLs = reader["iframe_host_URL"].ToString()
                };
            }
            else
            {
                // any subsequent dataset should be ignored
                return null;
            }
        }
    }
}