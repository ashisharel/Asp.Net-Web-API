using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Product;
using System.Collections.Generic;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategoryData(CategoryRequestDTO categoryRequest, int sessionId);
    }
}
