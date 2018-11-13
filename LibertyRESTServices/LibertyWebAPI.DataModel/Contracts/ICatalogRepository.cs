using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Product;
using System.Collections.Generic;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface ICatalogRepository
    {
        IList<Catalog> GetCatalogData(int sessionId);        
    }
}
