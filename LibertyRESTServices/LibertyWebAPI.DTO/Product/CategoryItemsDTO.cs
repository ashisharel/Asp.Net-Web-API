namespace LibertyWebAPI.DTO.Product
{
    public class CategoryItemsDTO
    {
        public string LibertyProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ProductType { get; set; }
        public int? CheckScenes { get; set; }
        public decimal? Price { get; set; }
        public string HcProductId { get; set; }
        public string StyleId { get; set; }
        public string Attribute { get; set; }
        public string CategoryName { get; set; }
        public int PopularityScore { get; set; }
    }
}