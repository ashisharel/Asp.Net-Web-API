using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Product;
using System.Collections.Generic;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface ICatalogService
    {
        CatalogDTO GetCatalog(int sessionId);
        CatalogDTO MapDTO(IList<Catalog> catalogData);
    }
}
