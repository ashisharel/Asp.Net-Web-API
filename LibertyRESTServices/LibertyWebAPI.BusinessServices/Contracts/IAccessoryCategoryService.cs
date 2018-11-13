using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Product;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IAccessoryCategoryService
    {
        AccessoryCategoryDTO GetProductAccents(string productId, string categoryId, int sessionId);

        AccessoryCategoryDTO GetProductBackgrounds(string productId, string categoryId, int sessionId);

        AccessoryCategoryDTO GetProductOneliners(string productId, int sessionId);

        AccessoryCategoryDTO MapAccentsDTO(AccessoryCategory accessoryCategory, string categoryId);

        AccessoryCategoryDTO MapOnelinersDTO(AccessoryCategory accessoryCategory);
    }
}