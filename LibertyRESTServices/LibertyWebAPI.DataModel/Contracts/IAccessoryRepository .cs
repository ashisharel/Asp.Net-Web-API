using LibertyWebAPI.BusinessEntities;
using System.Collections.Generic;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IAccessoryRepository
    {
        IList<Accessory> GetProductAccessories(string productId, int sessionId);
    }
}