using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibertyWebAPI.DTO.Institution
{
    /// <summary>
    /// Institution specific branding information for a customer.
    /// </summary>
    public class InstitutionDTO
    {
        /// <summary>
        /// The constructor
        /// </summary>
        public InstitutionDTO()
        {
            //histryPersonalization = new Dictionary<string, string>();
            //histryPersonalizationBold = new Dictionary<string, string>();
            Address = new AddressDTO();
            //orderAdditions = new List<OrderAdditionDTO>();
            Validation = new ValidationDTO();
            Navigation = new List<InstitutionLinkDTO>();
            ContactUsContent = new ContactUsDTO();
        }
        /// <summary>
        /// Liberty session ID created during login
        /// </summary>
        public int? SessionID { get; set; }
        /// <summary>
        /// The name of the institution e.g. Navy Federal Credit Union.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The name given to the FI for analytics purposes.
        /// </summary>
        public string AnalyticsName { get; set; }
        /// <summary>
        /// The transit routing number of the institution e.g. '256074974'
        /// </summary>
        public string TransitRouting { get; set; }
        /// <summary>
        /// The account of the customer requesting the institution information.
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// whether is a First Order Acceptance (FOA) or not
        /// </summary>
        public bool FOAFlag { get; set; }
        /// <summary>
        /// the default ABA/Branch number; if not empty should be used otherwise, 
        /// UI should call /foa/branches to let the user select a valid ABA/Branch#
        /// </summary>
        public string FOADefaultAbabrID { get; set; }
        /// <summary>
        /// the default FOA Account Type
        /// </summary>
        public string FOADefaultAcctType { get; set; }
        /// <summary>
        /// The code identifying the club which in turn determines the catalog.
        /// </summary>
        public string ClubCode { get; set; }
        /// <summary>
        /// The name of the club.
        /// </summary>
        public string ClubName { get; set; }
        /// <summary>
        /// The branch code associated with this TR.
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// A URL to the institutions logo used for branding.
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// A URL to the institutions home page.
        /// </summary>
        public string ClientHomePage { get; set; }
        /// <summary>
        /// A customer service telephone number. This may be a Harland Clarke number 
        /// or a financial institution number depending contractual arrangements.
        /// </summary>
        public string CustomerServiceNumber { get; set; }
        /// <summary>
        /// Liberty specify, not yet passed to the Streamline API
        /// </summary>
        //public bool IsAllowedCheckOrder { get; set; }
        /// <summary>
        /// Liberty specify, not yet passed to the Streamline API
        /// </summary>
        public bool IsAllowedOrderStatus { get; set; }
        /// <summary>
        /// Liberty specify, not yet passed to the Streamline API
        /// </summary>
        public bool IsAllowedProductChange { get; set; }
        /// <summary>
        /// This is true if the customer is allowed to determine their own starting check number.
        /// </summary>
        public bool IsAllowedEditCheckNumber { get; set; }
        /// <summary>
        /// This is true if the institution offers PromoCodes that the user can enter.
        /// </summary>
        public bool IsAllowedEnterPromoCode { get; set; }
        /// <summary>
        /// This is true if the institution allows the user to edit the personalization name lines.
        /// </summary>
        public bool IsAllowedEditPersonalizationNames { get; set; }
        /// <summary>
        /// This is true if the institution allows the user to edit the personalization address lines.
        /// </summary>
        public bool IsAllowedEditPersonalizationAddress { get; set; }
        /// <summary>
        /// This is true if the institution allows the user to edit the shipping address.
        /// </summary>
        public bool IsAllowedEditShipping { get; set; }
        /// <summary>
        /// This is true if the institution allows the user to edit the email address.
        /// </summary>
        public bool IsAllowedEditEmail { get; set; }
        /// <summary>
        /// This is true if the institution allows the use of a foreign shipping address.
        /// </summary>
        public bool IsAllowedEnterForeignShipping { get; set; }
        /// <summary>
        /// This is true if the institution offers a default ShippingOption but does not want it to be applied but selected.
        /// </summary>
        public bool IsPreselectShippingOption { get; set; }
        /// <summary>
        /// This is true if the institution offers a default ShippingOption.
        /// </summary>
        public bool IsDefaultShippingOption { get; set; }
        /// <summary>
        /// This is false when item price needs to be masked in the UI
        /// </summary>
        public bool IsPricingAvailable { get; set; }
        /// <summary>
        /// This is true if the institution requires an email to be entered.
        /// </summary>
        public bool IsRequiredEmail { get; set; }
        /// <summary>
        /// This is true if the account is a business account.
        /// </summary>
        public bool IsBusinessAccount { get; set; }
        /// <summary>
        /// This is the number of accessories to display. 
        /// </summary>
        public int? AccessoriesAllowed { get; set; }
        /// <summary>
        /// Liberty specific - false if the customer has a pending check order
        /// </summary>
        public bool IsAllowedOrderChecks { get; set; }
        /// <summary>
        /// This is true if the user interface must force the user to verify the personalization before continuing.
        /// </summary>
        public bool ForcePersonalizationVerification { get; set; }
        /// <summary>
        /// This is the URL given to use to keep the client side application session alive, only used for integration purposes.
        /// </summary>
        public string IntegrationKeepAliveURL { get; set; }
        /// <summary>
        /// This is the URL given to use to kill the client side application session, only used for integration purposes.
        /// </summary>
        public string IntegrationKillClientSessionURL { get; set; }
        /// <summary>
        /// 0 - Order valid
        /// 1 - order in production, accessories may be ordered
        /// 2 - check order errors, accessories may be ordered
        /// 3 - no orders allowed
        /// </summary>
        public string LoginStatus { get; set; }
        /// <summary>
        /// This is true if the user interface should offer the user the opportunity to fill out a survey.
        /// </summary>
        public bool ShowSurvey { get; set; }
        /// <summary>
        /// This is true if the FI allows the user to see the personalization.
        /// </summary>
        public bool ShowPersonalization { get; set; }
        /// <summary>
        /// This is true if the FI allows the user to see the personalization that 
        /// was sent over and compare it to the last order personalization.
        /// </summary>
        public bool ShowPersonalizationCompare { get; set; }
        /// <summary>
        /// This is true if the FI allows the user to see the shipping address.
        /// </summary>
        public bool ShowShippingAddress { get; set; }
        /// <summary>
        /// This is true if the FI allows to show the email.
        /// </summary>
        public bool ShowEmail { get; set; }
        /// <summary>
        /// This is true if the FI renders our page in an iFrame.
        /// </summary>
        public bool ShowIframe { get; set; }
        /// <summary>
        /// This is true if the FI renders our page in an iFrame.
        /// </summary>
        public bool ShowIFrameInsecure { get; set; }
        /// <summary>
        /// This is true if the FI allows the participation of SMS alerts, this will show the phone number entry field.
        /// </summary>
        public bool ShowPhoneNumberField { get; set; }
        /// <summary>
        /// This is a specific color to be used as to personalize the UI for the FI.
        /// </summary>
        public string StyleColor1 { get; set; }
        /// <summary>
        /// This will return the appropriate channel associated with the trasitRouting number sent. e.g. 'CCP' || 'CHOICE'
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// Returns 'DEFAULT' if 'powered by Harland Clarke' is to be shown, '' if nothing is to be shown.
        /// </summary>
        public string PoweredBy { get; set; }
        /// <summary>
        /// This is URL of the background image to be used for the UI.
        /// </summary>
        public string BackgroundImage { get; set; }
        /// <summary>
        /// This is text that would be displayed in the background.
        /// </summary>
        public string BackgroundTextImage { get; set; }
        /// <summary>
        /// This is the catalogId that the user can see.
        /// </summary>
        public string CatalogId { get; set; }
        /// <summary>
        /// This is the customCatalogId that is presented if the FI has a custom catalog.
        /// </summary>
        public string CustomCatalogId { get; set; }
        ///// <summary>
        ///// historyPersonalization1 History Personalization line 1.
        ///// historyPersonalization2 History Personalization line 2.
        ///// historyPersonalization3 History Personalization line 3.
        ///// historyPersonalization4 History Personalization line 4.
        ///// historyPersonalization5 History Personalization line 5.
        ///// historyPersonalization6 History Personalization line 6.
        ///// </summary>
        //public IDictionary<string, string> histryPersonalization { get; set; }
        ///// <summary>
        ///// historyPersonalizationBold1 This is true if History Personalization line 1 is bold.
        ///// historyPersonalizationBold2 This is true if History Personalization line 2 is bold.
        ///// historyPersonalizationBold3 This is true if History Personalization line 3 is bold.
        ///// </summary>
        //public IDictionary<string, bool> histryPersonalizationBold { get; set; }
        /// <summary>
        /// The Institution Address
        /// </summary>
        public AddressDTO Address { get; set; }
        ///// <summary>
        ///// Order additions.
        ///// </summary>
        //public IList<OrderAdditionDTO> orderAdditions { get; set; }
        public ValidationDTO Validation { get; set; }
        /// <summary>
        /// A set of InstitutionLinks, one for 'logout', 'online banking' and 'survey'
        /// </summary>
        public IList<InstitutionLinkDTO> Navigation { get; set; }
        /// <summary>
        /// Content for contact us link.
        /// </summary>
        public ContactUsDTO ContactUsContent { get; set; }
        /// <summary>
        /// A set of client host/referrer URLs needed for iFrames support
        /// </summary>
        public IList<string> IFrameHostURLs { get; set; }
    }

    ///// <summary>
    ///// Additional information for the order object.
    ///// </summary>
    //public class OrderAdditionDTO
    //{
    //    /// <summary>
    //    /// JSON location of input.
    //    /// </summary>
    //    public string input { get; set; }
    //    /// <summary>
    //    /// JSON location of output.
    //    /// </summary>
    //    public string output { get; set; }
    //}

    public class AddressDTO
    {
        /// <summary>
        /// Address line 1 of the address.
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Address line 2 of the address.
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// City of the address.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State of the address.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Zip code of the address.
        /// </summary>
        public string Zip { get; set; }
    }

    /// <summary>
    /// Content for contact us link.
    /// </summary>
    public class ContactUsDTO
    {
        /// <summary>
        /// The text of the modal.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The title of the modal.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The modal to be shown.
        /// </summary>
        public string Modal { get; set; }
    }

    public class InstitutionLinkDTO
    {
        /// <summary>
        /// The hardcoded link name. e.g. 'logout', 'onlinebanking' and 'survey'
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// The label to display on the UI. If none is given then the UI will use the default.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// The URL to use with this link. If none is given then the UI will use the default.
        /// </summary>
        public string Url { get; set; }
    }

    public class ValidationDTO
    {
        public int? EmailMaxLength { get; set; }
        public int? PromoCodeMaxLength { get; set; }
        public int? ShippingLinesMaxLength { get; set; }
        public int? StartingCheckMinValue { get; set; }
        public int? StartingCheckMaxValue { get; set; }
    }

    public struct InstitutionResponseDTO
    {
        public LoginSessionDTO LoginSession { get; set; }
        public InstitutionDTO Institution { get; set; }
    }

    public class LoginSessionDTO
    {
        public int? SessionID { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string AllowRetryFlag { get; set; }
        public string FOAFlag { get; set; }
        public string FOADefaultAbabrID { get; set; }
        public string FOADefaultAcctType { get; set; }
        public string FOART { get; set; }
        public string FOAAccount { get; set; }
        public string AnalyticsName { get; set; }
        public string StatusDetail { get; set; }
    }


}