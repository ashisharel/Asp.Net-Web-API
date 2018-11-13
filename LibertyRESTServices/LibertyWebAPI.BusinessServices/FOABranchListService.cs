using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;
using System.Collections.Generic;
using System.Linq;

namespace LibertyWebAPI.BusinessServices
{
    /// <summary>
    /// FOA Branch List Service
    /// </summary>
    public class FOABranchListService : IFOABranchListService
    {
        public readonly IFOABranchListRepository _foaBranchListRepository;

        public FOABranchListService(IFOABranchListRepository foaBranchListRepository)
        {
            _foaBranchListRepository = foaBranchListRepository;
        }

        /// <summary>
        /// Get FOA Branch List
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public IList<FOABranchDTO> GetBranchList(BranchRequestDTO branchListRequest)
        {
            var foaBranchList = _foaBranchListRepository.GetBranchList(branchListRequest);
            if (foaBranchList != null && foaBranchList.Any())
            {
                return MapDTO(foaBranchList);
            }
            return null;
        }

        /// <summary>
        /// map entity to DTO class
        /// </summary>
        /// <param name="branchList"></param>
        /// <returns></returns>
        public IList<FOABranchDTO> MapDTO(IEnumerable<FOABranch> branchList)
        {
            var foaBranchList = branchList.Select(b => new FOABranchDTO()
            {
                AbabrId = b.AbabrId,
                City = string.IsNullOrWhiteSpace(b.City) ? string.Empty : b.City.Trim(),
                State = string.IsNullOrWhiteSpace(b.State) ? string.Empty : b.State.Trim(),
                ZipCode = string.IsNullOrWhiteSpace(b.ZipCode) ? string.Empty : b.ZipCode.Trim(),
                Address1 = string.IsNullOrWhiteSpace(b.Address1) ? string.Empty : b.Address1.Trim(),
                Address2 = string.IsNullOrWhiteSpace(b.Address2) ? string.Empty : b.Address2.Trim(),
                MainBrFlag = b.MainBrFlag
            });
            return foaBranchList.ToList();
        }
    }
}