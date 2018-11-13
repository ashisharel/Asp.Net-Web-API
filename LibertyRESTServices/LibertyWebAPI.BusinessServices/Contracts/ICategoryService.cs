using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryItemsDTO> GetCategoryItems(CategoryRequestDTO categoryRequest, int sessionId);
        IEnumerable<CategoryItemsDTO> MapDTO(IEnumerable<Category> categoryData);
    }
}
