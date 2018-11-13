using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.Common;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    public class AccessoryService : IAccessoryService
    {
        private readonly IAccessoryRepository _accessoryRepository;

        public AccessoryService(IAccessoryRepository accessoryRepository)
        {
            _accessoryRepository = accessoryRepository;
        }

        public IList<AccessoryDTO> GetProductAccessories(string productId, int sessionId)
        {
            var accessories = _accessoryRepository.GetProductAccessories(productId, sessionId);
            if (accessories != null)
            {
                return MapDTO(accessories);
            }
            return null;
        }

        public IList<AccessoryDTO> MapDTO(IEnumerable<Accessory> accessories)
        {
            var accessoriesDto = accessories.Select(a => new AccessoryDTO()
            {
                Code = a.Code,
                Name = a.Name,
                Price = new MoneyDTO() { Amount = a.Amount },
                Quantity = new QuantityDTO() { Amount = a.Quantity },
                Url = a.Url,
                Type = a.Type,
                HCProductId = a.HCProductId,
                maxQuantity = new QuantityDTO() { Amount = a.maxQuantity }
            }).ToList();

            return accessoriesDto;
        }
    }
}