using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;

namespace LibertyWebAPI.BusinessServices
{
    public class FOASaveService : IFOASaveService
    {

        private readonly IFOASaveRepository _foaSaveRepository;

        public FOASaveService(IFOASaveRepository foaSaveRepository)
        {
            _foaSaveRepository = foaSaveRepository;
        }

        public string Save(FOASaveDTO foaSaveDto)
        {
            return _foaSaveRepository.Save(foaSaveDto);
        }
    }
}