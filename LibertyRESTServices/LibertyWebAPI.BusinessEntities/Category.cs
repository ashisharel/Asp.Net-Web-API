
namespace LibertyWebAPI.BusinessEntities
{
    public class Category
    {
        //public int CategoryId { get; set; }
        public string ProductID { get; set; }        
        public string ProductName { get; set; }        
        public string CategoryName { get; set; }
        public int? PrimaryCategory { get; set; }
        public int? Check_Scenes { get; set; }
        public decimal Price { get; set; }
        public string HarlandProductId { get; set; }
        public string StyleId { get; set; }
        public string Attribute { get; set; }
        public int PopularityScore { get; set; }
        //public int? Deposits { get; set; }
        public string ImageURL { get; set; }
        //public string CheckDesigner { get; set; } 
        public int? ProductType { get; set; }
    }
}
