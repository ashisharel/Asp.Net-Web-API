using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public class FOASaveRepository : BaseRepository<string>, IFOASaveRepository
    {
        public string Save(FOASaveDTO foaSaveDTO)
        {
            IList<SqlParameter> outputParam;
            SqlCommand cmd = new SqlCommand("p_FOA_Save_Result");

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@RT", Value = foaSaveDTO.RtNumber, SqlDbType = SqlDbType.Char, Precision = 9 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Account", Value = foaSaveDTO.AccountNumber, SqlDbType = SqlDbType.VarChar, Precision = 18 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@First_Name", Value = foaSaveDTO.FirstName, SqlDbType = SqlDbType.VarChar, Precision = 30 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Initial", Value = (object)(foaSaveDTO.Initial) ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 1 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Last_Name", Value = (object)foaSaveDTO.LastName ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 30 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Address1", Value = (object)foaSaveDTO.Address1 ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 40 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Address2", Value = (object)foaSaveDTO.Address2 ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 40 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@City", Value = (object)foaSaveDTO.City ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 30 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@State", Value = (object)foaSaveDTO.State ?? DBNull.Value, SqlDbType = SqlDbType.Char, Precision = 2 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Zipcode", Value = (object)foaSaveDTO.Zipcode ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 10 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@DOB", Value = (object)foaSaveDTO.DOB ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 10 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Question_Count", Value = (object)foaSaveDTO.QuestionCount ?? DBNull.Value, SqlDbType = SqlDbType.Int });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Result", Value = (object)foaSaveDTO.Result ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 20 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Source_System", Value = (object)foaSaveDTO.SourceSystem ?? DBNull.Value, SqlDbType = SqlDbType.Char, Precision = 1 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@FOA_Session", Direction = ParameterDirection.Output, Value = "0000000000", SqlDbType = SqlDbType.VarChar, Size = 10, Precision = 10 });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Start_Check_Number", Value = (object)foaSaveDTO.StartCheckNumber ?? DBNull.Value, SqlDbType = SqlDbType.VarChar, Precision = 6 });

            ExecuteStoredProcWithOutputParameters(cmd, out outputParam);

            if (outputParam == null)
                return null;

            var output = outputParam.FirstOrDefault(r => r.ParameterName == "@FOA_Session");

            if (output == null)
                return null;

            return output.Value.ToString();
        }

        public override string PopulateRecord(IDataReader reader, int resultCount = 1)
        {
            return null;
        }
    }
}