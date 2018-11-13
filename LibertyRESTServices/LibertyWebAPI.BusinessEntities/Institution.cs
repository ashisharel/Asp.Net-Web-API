namespace LibertyWebAPI.BusinessEntities
{
    public class Institution
    {        
        public int? SessionID { get; set; }
        public string Name { get; set; }
        //public string AnalyticsName { get; set; }
        public string TransitRouting { get; set; }
        public string Account { get; set; }
        public string ClubCode { get; set; }
        public string ClubName { get; set; }
        public string BranchCode { get; set; }
        public string Logo { get; set; }
        public string ClientHomePage { get; set; }
        public string CustomerServiceNumber { get; set; }
        public string IsAllowedCheckOrder { get; set; }
        public string IsAllowedOrderStatus { get; set; }
        public string IsAllowedProductChange { get; set; }
        public string IsAllowedEditCheckNumber { get; set; }
        public string IsAllowedEnterPromoCode { get; set; }
        public string IsAllowedEditPersonalizationNames { get; set; }
        public string IsAllowedEditPersonalizationAddress { get; set; }
        public string IsAllowedEditShipping { get; set; }
        public string IsAllowedEnterForeignShipping { get; set; }
        public string IsPreselectShippingOption { get; set; }
        public string IsDefaultShippingOption { get; set; }
        public string IsPricingAvailable { get; set; }
        public string IsRequiredEmail { get; set; }
        public string IsBusinessAccount { get; set; }
        public string AccessoriesAllowed { get; set; }
        public string ForcePersonalizationVerification { get; set; }
        public string IntegrationKeepAliveURL { get; set; }
        public string IntegrationKillClientSessionURL { get; set; }
        public string ShowSurvey { get; set; }
        public string ShowPersonalization { get; set; }
        public string ShowPersonalizationCompare { get; set; }
        public string ShowShippingAddress { get; set; }
        public string ShowEmail { get; set; }
        public string ShowIFrame { get; set; }
        public string ShowIFrameInsecure { get; set; }
        public string ShowPhoneNumberField { get; set; }
        public string StyleColor1 { get; set; }
        public string Channel { get; set; }
        public string PoweredBy { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundTextImage { get; set; }
        public string CatalogID { get; set; }
        public string CustomCatalogId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string MinCheckNumber { get; set; }
        public string MaxCheckNumber { get; set; }
        public string ShippingLinesMaxLength { get; set; }
        public string EmailMaxLength { get; set; }
        public string AccLine1 { get; set; }
        public string AccLine2 { get; set; }
        public string AccLine3 { get; set; }
        public string AccLine4 { get; set; }
        public string AccLine5 { get; set; }
        public string AccLine6 { get; set; }
        public string IFrameHostURLs { get; set; }
    }
}
