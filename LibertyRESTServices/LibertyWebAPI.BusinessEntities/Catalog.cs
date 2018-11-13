
namespace LibertyWebAPI.BusinessEntities
{
    public class Catalog
    {
        public int CategoryId { get; set; }
        public int PrimaryCategory { get; set; }
        public string CategoryName { get; set; }        
        public string PrimaryDescription { get; set; }
        public string CategoryAttributes { get; set; }    	
    }
}
