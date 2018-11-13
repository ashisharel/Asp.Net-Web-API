using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Order;
using System.Collections.Generic;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface ILastRepricedService
    {
        OrderDTO GetLastRepriced(int sessionId);

        OrderDTO MapDTO(Order order);
    }
}
