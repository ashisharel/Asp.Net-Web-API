using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class FOABranchListRepository : BaseRepository<FOABranch>, IFOABranchListRepository
    {
        public IEnumerable<FOABranch> GetBranchList(BranchRequestDTO branchListRequest)
        {
            SqlCommand cmd = new SqlCommand("TSUtilities.dbo.p_FOA_BranchList");
            cmd.Parameters.AddWithValue("@RT", branchListRequest.RtNumber ?? string.Empty);
            cmd.Parameters.AddWithValue("@Account", branchListRequest.AccountNumber ?? string.Empty);
            return base.ExecuteStoredProc(cmd);
        }

        public override FOABranch PopulateRecord(System.Data.IDataReader reader, int resultCount = 1)
        {
            return new FOABranch()
            {
                AbabrId = reader["Ababr_id"].ToString(),
                City = reader["City"].ToString(),
                State = reader["State"].ToString(),
                ZipCode = reader["ZipCode"].ToString(),
                Address1 = reader["Address1"].ToString(),
                Address2 = reader["Address2"].ToString(),
                MainBrFlag = reader["MainBr_Flag"].ToString(),
            };
        }
    }
}