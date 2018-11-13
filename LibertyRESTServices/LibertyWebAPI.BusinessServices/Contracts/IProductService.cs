using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Product;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IProductService
    {
        ProductDTO GetProduct(string productId, int sessionId);

        ProductDTO MapDTO(Product product);
    }
}