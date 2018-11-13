using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Common;
using System.Collections.Generic;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IAccessoryService
    {
        IList<AccessoryDTO> GetProductAccessories(string productId, int sessionId);

        IList<AccessoryDTO> MapDTO(IEnumerable<Accessory> accessories);

    }
}