using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IShippingOptionsService
    {
        IEnumerable<ShippingOptionDTO> GetShippingOptions(ShippingOptionsRequestDTO shippingOptionsRequest, int sessionId);
        IEnumerable<ShippingOptionDTO> MapDTO(IEnumerable<ShippingOption> shippingOptions);
    }
}
