using LibertyWebAPI.BusinessEntities;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface ILastRepricedRepository
    {
        Order GetLastRepriced(int sessionId);
    }
}
