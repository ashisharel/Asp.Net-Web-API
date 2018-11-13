using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class FOAAccountValidationRepository : BaseRepository<string>, IFOAAccountValidationRepository
    {
        public static string StoredProcedureName = "p_FOA_Valid_Account";
        public void ValidateFOAAccount(FOAAccountValidationRequestDTO accountRequest, out int resultCode, out string message)
        {
            IList<SqlParameter> outParams;
            SqlCommand cmd = new SqlCommand(StoredProcedureName);
            cmd.Parameters.AddWithValue("@Ababr_id", accountRequest.AbabrId ?? string.Empty);
            cmd.Parameters.AddWithValue("@Account", accountRequest.AccountNumber ?? string.Empty);
            AddOutputParameter("@IResult", SqlDbType.Int, cmd);
            AddOutputParameter("@SResult", SqlDbType.VarChar, cmd, 500);
            base.ExecuteStoredProcWithOutputParameters(cmd, out outParams);
            resultCode = Convert.ToInt32(outParams.FirstOrDefault(r => r.ParameterName == "@IResult").Value);
            message = Convert.ToString(outParams.FirstOrDefault(r => r.ParameterName == "@SResult").Value);
        }

        public override string PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return null; // Not needed
        }
    }
}
