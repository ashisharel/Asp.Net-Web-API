using LibertyWebAPI.BusinessEntities;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IProductRepository
    {
        Product GetProduct(string productId, string colorId, int sessionId);
    }
}