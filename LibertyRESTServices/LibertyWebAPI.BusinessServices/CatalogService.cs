using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;
        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public CatalogDTO GetCatalog(int sessionId)
        {
            var catalogData = _catalogRepository.GetCatalogData(sessionId);
            if (catalogData != null && catalogData.Any())
            {
                return MapDTO(catalogData);
            }
            return null;
        }

        public CatalogDTO MapDTO(IList<Catalog> catalogData)
        {
            var catalogDto = new CatalogDTO();
            //catalogDto.id = null;
            //catalogDto.name = null;
            var count = catalogData.Count();

            for (int i = 0; i < count; i++)
            {
                
                var catalogItem = new CatalogItemsDTO();
                
                catalogItem.Id = Convert.ToString(catalogData[i].PrimaryCategory);
                catalogItem.Name = catalogData[i].PrimaryDescription;                 
                catalogItem.Image = null; // TODO
                do
                {
                    var category = new CatalogItemsDTO();
                    category.Id = Convert.ToString(catalogData[i].CategoryId);
                    category.Name = catalogData[i].CategoryName;                    
                    category.Image = null; //TODO                    
                    category.CategoryAttributes = catalogData[i].CategoryAttributes;
                    catalogItem.Items.Add(category);
                    i++;
                }
                while (i < count && catalogData[i - 1].PrimaryCategory == catalogData[i].PrimaryCategory);
                i--; // to negate the i++ done above, as i++ will happen in for loop now onwards
                catalogDto.Items.Add(catalogItem);

            }
            // set the first primaryCategory Id value as defaultcategoryId, for UI to load default products on home page
            catalogDto.DefaultCategoryId = Convert.ToString(catalogDto.Items[0].Id); 
            return catalogDto;
        }
    }
}
