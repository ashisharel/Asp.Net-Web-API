using LibertyWebAPI.DTO.FOA;
namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IFOASaveService
    {
        string Save(FOASaveDTO foaSaveDto);
    }
}