using LibertyWebAPI.BusinessEntities;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IAccessoryCategoryRepository
    {
        AccessoryCategory GetProductAccents(string productId, string categoryId, int sessionId);

        AccessoryCategory GetProductBackgrounds(string productId, string categoryId, int sessionId);

        AccessoryCategory GetProductOneliners(string productId, int sessionId);
    }
}