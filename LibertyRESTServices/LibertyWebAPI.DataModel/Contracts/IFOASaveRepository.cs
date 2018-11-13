using LibertyWebAPI.DTO.FOA;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IFOASaveRepository
    {
        string Save(FOASaveDTO foaSaveDTO);
    }
}