using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;
using LibertyWebAPI.DTO.Institution;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IFOASaveRepository _foaSaveRepository;

        public InstitutionService(IInstitutionRepository repository, IFOASaveRepository foaSaveRepository)
        {
            _institutionRepository = repository;
            _foaSaveRepository = foaSaveRepository;
        }

        public InstitutionResponseDTO GetInstitution(InstitutionRequestDTO institutionRequest)
        {
            InstitutionResponseDTO response;

            if (!string.IsNullOrWhiteSpace(institutionRequest.Payload) && institutionRequest.Payload.Length > 15)
            {
                var objCipher = new Encrypt.Cipher();
                institutionRequest.Payload = objCipher.Decrypt(institutionRequest.Payload); // decrypt preauthSession
            }

            if (institutionRequest.FOAFlag == true && institutionRequest.FOAResult != null)
            {
                institutionRequest.FOAResult.AccountNumber = institutionRequest.AccountNumber;
                institutionRequest.FOAResult.RtNumber = institutionRequest.RtNumber;
                institutionRequest.FOAResult.SourceSystem = "Y"; // PEP identifier
                institutionRequest.FOAResult.Result = string.IsNullOrWhiteSpace(institutionRequest.FOAResult.Result) ? string.Empty : institutionRequest.FOAResult.Result.ToLower();
                var dob = string.IsNullOrWhiteSpace(institutionRequest.FOAResult.DOB) || string.Equals("--", institutionRequest.FOAResult.DOB) ? 
                    string.Empty : Convert.ToDateTime(institutionRequest.FOAResult.DOB).ToString("MM/dd/yyyy");                
                institutionRequest.FOAResult.DOB = dob;
                institutionRequest.PreAuthSession = _foaSaveRepository.Save(institutionRequest.FOAResult);
                if (!string.Equals(institutionRequest.FOAResult.Result, "pass", StringComparison.OrdinalIgnoreCase))
                    return response;
            }

            var institution = _institutionRepository.GetInstitution(institutionRequest);

            response.LoginSession = new LoginSessionDTO()
            {
                SessionID = institution.loginSession.SessionID,
                Status = institution.loginSession.Status,
                Message = institution.loginSession.Message,
                AllowRetryFlag = institution.loginSession.AllowRetryFlag,
                FOAFlag  = institution.loginSession.FOAFlag,
                FOADefaultAbabrID = institution.loginSession.FOADefaultAbabrID,
                FOADefaultAcctType = institution.loginSession.FOADefaultAcctType,
                FOART = institution.loginSession.FOART,
                FOAAccount = institution.loginSession.FOAAccount,
                AnalyticsName = institution.loginSession.AnalyticsName,
                StatusDetail = institution.loginSession.StatusDetail
            };

            if (institution.loginSession.StatusDetail == "205" && institution.loginSession.FOAFlag == "Y")
            {
                //FOA response
                response.Institution = new InstitutionDTO()
                {
                    TransitRouting = string.IsNullOrWhiteSpace(institutionRequest.RtNumber) ? institution.loginSession.FOART : institutionRequest.RtNumber,
                    Account = string.IsNullOrWhiteSpace(institutionRequest.AccountNumber) ? institution.loginSession.FOAAccount : institutionRequest.AccountNumber,
                    FOAFlag = true,
                    FOADefaultAbabrID = institution.loginSession.FOADefaultAbabrID.Trim(),
                    FOADefaultAcctType = institution.loginSession.FOADefaultAcctType.Trim(),
                    Channel = "Liberty",
                    Logo = institution.loginSession.FOALogo
                };
            }
            else if (institution.Institution != null)
            {
                response.Institution = MapDTO(institution.Institution);
                response.Institution.LoginStatus = institution.loginSession.Status;
                /*
                Field name - "loginStatus"
                Possible values:
                0 - Order valid
                1 – last order in production so check order not allowed, accessories may be ordered
                2 - check order not allowed(acctType restrictions etc.), accessories may be ordered
                3 - no orders allowed
                4 - incorrect logon zipcode
                */                                
            }
            else
            {
                response.Institution = new InstitutionDTO()
                {
                    TransitRouting = institutionRequest.RtNumber,
                    Account = institutionRequest.AccountNumber,
                    Channel = "Liberty",
                    Logo = institution.loginSession.FOALogo,
                    LoginStatus = institution.loginSession.Status
                };
            }
            if (response.Institution != null)
            {                
                response.Institution.AnalyticsName = institution.loginSession.AnalyticsName;
            }

            return response;
        }

        public InstitutionDTO MapDTO(Institution institution)
        {
            var institutionDto = new InstitutionDTO();
            institutionDto.SessionID = institution.SessionID;
            institutionDto.Name = institution.Name.ToNullable();
            //institutionDto.AnalyticsName = institution.AnalyticsName.ToNullable();
            institutionDto.TransitRouting = institution.TransitRouting.ToNullable();
            institutionDto.Account = institution.Account.ToNullable();
            institutionDto.ClubCode = institution.ClubCode.ToNullable();
            institutionDto.ClubName = institution.ClubName.ToNullable();
            institutionDto.BranchCode = institution.BranchCode.ToNullable();
            institutionDto.Logo = institution.Logo.ToNullable();
            institutionDto.ClientHomePage = institution.ClientHomePage.ToNullable();
            institutionDto.CustomerServiceNumber = institution.CustomerServiceNumber.ToNullable();
            institutionDto.IsAllowedOrderChecks = institution.IsAllowedCheckOrder.ToBool(); // New field added in streamline, false if previous order not yet shipped
            institutionDto.IsAllowedOrderStatus = institution.IsAllowedOrderStatus.ToBool();
            institutionDto.IsAllowedProductChange = institution.IsAllowedProductChange.ToBool();
            institutionDto.IsAllowedEditCheckNumber = institution.IsAllowedEditCheckNumber.ToBool();
            institutionDto.IsAllowedEnterPromoCode = institution.IsAllowedEnterPromoCode.ToBool();
            institutionDto.IsAllowedEditPersonalizationNames = institution.IsAllowedEditPersonalizationNames.ToBool();
            institutionDto.IsAllowedEditPersonalizationAddress = institution.IsAllowedEditPersonalizationAddress.ToBool();
            institutionDto.IsAllowedEditShipping = institution.IsAllowedEditShipping.ToBool();
            institutionDto.IsAllowedEnterForeignShipping = institution.IsAllowedEnterForeignShipping.ToBool();
            institutionDto.IsPreselectShippingOption = institution.IsPreselectShippingOption.ToBool();
            institutionDto.IsDefaultShippingOption = institution.IsDefaultShippingOption.ToBool();
            institutionDto.IsPricingAvailable = institution.IsPricingAvailable.ToBool();
            institutionDto.IsRequiredEmail = institution.IsRequiredEmail.ToBool();
            institutionDto.IsBusinessAccount = institution.IsBusinessAccount.ToBool();
            institutionDto.AccessoriesAllowed = institution.AccessoriesAllowed.ToBool() ? 20 : 0; // If accessories not allowed, set count = 0 else 20(big enough number)
            institutionDto.ForcePersonalizationVerification = institution.ForcePersonalizationVerification.ToBool();
            institutionDto.IntegrationKeepAliveURL = institution.IntegrationKeepAliveURL.ToNullable();
            institutionDto.IntegrationKillClientSessionURL = institution.IntegrationKillClientSessionURL.ToNullable();
            institutionDto.ShowSurvey = institution.ShowSurvey.ToBool();
            institutionDto.ShowPersonalization = institution.ShowPersonalization.ToBool();
            institutionDto.ShowPersonalizationCompare = institution.ShowPersonalizationCompare.ToBool();
            institutionDto.ShowShippingAddress = institution.ShowShippingAddress.ToBool();
            institutionDto.ShowEmail = institution.ShowEmail.ToBool();
            institutionDto.ShowIframe = institution.ShowIFrame.ToBool();
            institutionDto.ShowIFrameInsecure = institution.ShowIFrameInsecure.ToBool();
            institutionDto.ShowPhoneNumberField = institution.ShowPhoneNumberField.ToBool();
            institutionDto.StyleColor1 = institution.StyleColor1.ToNullable();
            institutionDto.Channel = institution.Channel.ToNullable();
            institutionDto.PoweredBy = institution.PoweredBy.ToNullable();
            institutionDto.BackgroundImage = institution.BackgroundImage.ToNullable();
            institutionDto.BackgroundTextImage = institution.BackgroundTextImage.ToNullable();
            institutionDto.CatalogId = institution.CatalogID.ToNullable();
            institutionDto.CustomCatalogId = institution.CustomCatalogId.ToNullable();            
            institutionDto.Address = new AddressDTO()
            {
                Address1 = institution.Address1.ToNullable(),
                Address2 = institution.Address2.ToNullable(),
                City = institution.City.ToNullable(),
                State = institution.State.ToNullable(),
                Zip = institution.Zip.ToNullable()
            };
            institutionDto.Validation = new ValidationDTO()
            {
                EmailMaxLength = institution.EmailMaxLength.ToNullableInt(),
                PromoCodeMaxLength = null,
                ShippingLinesMaxLength = institution.ShippingLinesMaxLength.ToNullableInt(),
                StartingCheckMinValue = institution.MinCheckNumber.ToNullableInt(),
                StartingCheckMaxValue = institution.MaxCheckNumber.ToNullableInt(),
            };
            
            var navigation = new InstitutionLinkDTO() // To trigger CS to select the default survey URL
            {
                Link = "survey",
                Label = null,
                Url = null
            };
            institutionDto.Navigation.Add(navigation);
            institutionDto.IsAllowedEditEmail = true; // always true for Liberty
            institutionDto.IFrameHostURLs = institution.IFrameHostURLs.Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
            return institutionDto;
        }
    }
}