using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.FOA;
using System.Collections.Generic;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IFOABranchListRepository
    {
        IEnumerable<FOABranch> GetBranchList(BranchRequestDTO branchListRequest);
    }
}