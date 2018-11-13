using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.FOA;
using System.Collections.Generic;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    /// <summary>
    /// FOA Branch List Service
    /// </summary>
    public interface IFOABranchListService
    {
        /// <summary>
        /// Get FOA Branch List
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        IList<FOABranchDTO> GetBranchList(BranchRequestDTO branchListRequest);

        /// <summary>
        /// map entity to DTO class
        /// </summary>
        /// <param name="branchList"></param>
        /// <returns></returns>
        IList<FOABranchDTO> MapDTO(IEnumerable<FOABranch> branchList);
    }
}