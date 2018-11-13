using System.Collections.Generic;

namespace LibertyWebAPI.DTO.Product
{
    public class CatalogDTO
    {
        public CatalogDTO()
        {
            Items = new List<CatalogItemsDTO>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string DefaultCategoryId { get; set; }
        public IList<CatalogItemsDTO> Items { get; set; }

    }

    public class CatalogItemsDTO
    {
        public CatalogItemsDTO()
        {
            Items = new List<CatalogItemsDTO>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }        
        public string CategoryAttributes { get; set; }                
        public IList<CatalogItemsDTO> Items { get; set; }
    }

}
