using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibertyWebAPI.DataModel.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {        
        public abstract T PopulateRecord(IDataReader reader, int resultCount = 1);

        /// <summary>
        /// Generic method to execute stored procedure and return the result
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IList<T> ExecuteStoredProc(SqlCommand command, string dbName = "LUD")
        {
            var list = new List<T>();
            var resultCount = 1;

            using (SqlConnection connection = new SqlConnection(Utility.GetConnectionString()))
            {
                using (command)
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // 20171101 Luis - Added logic to read multiple DataSet
                        do
                        {
                            while (reader.Read())
                            {
                                var record = PopulateRecord(reader, resultCount);
                                if (record != null) list.Add(record);
                            }
                            resultCount++;
                        } while (reader.NextResult());
                    }
                }
            }            
            return list;
        }

        /// <summary>
        /// Generic method to execute stored procedure and return the result along with output parameters
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IList<T> ExecuteStoredProcWithOutputParameters(SqlCommand command, out IList<SqlParameter> outputParameters)
        {
            var list = new List<T>();
            outputParameters = new List<SqlParameter>();
            var resultCount = 1;
            using (SqlConnection connection = new SqlConnection(Utility.GetConnectionString()))
            {
                using (command)
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                var record = PopulateRecord(reader, resultCount);
                                if (record != null) list.Add(record);
                            }
                            resultCount++;
                        } while (reader.NextResult());

                        SqlParameterCollection parameterCollection = command.Parameters;
                        foreach (SqlParameter parameter in parameterCollection)
                        {
                            if (ParameterDirection.Output.Equals(parameter.Direction) || ParameterDirection.InputOutput.Equals(parameter.Direction))
                                outputParameters.Add(parameter);
                        }
                    }
                }
            }
            return list;
        }

        public void AddOutputParameter(string name, SqlDbType type, SqlCommand cmd, int size = 0)
        {
            if (type == SqlDbType.Int)
            {
                var outputParam = new SqlParameter(name, SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputParam);
            }
            else if (type == SqlDbType.VarChar)
            {
                var outputParam = new SqlParameter(name, SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Output,
                    Size = size
                };
                cmd.Parameters.Add(outputParam);
            }
            else if (type == SqlDbType.Char)
            {
                var outputParam = new SqlParameter(name, SqlDbType.Char)
                {
                    Direction = ParameterDirection.Output,
                    Size = size
                };
                cmd.Parameters.Add(outputParam);
            }
        }        

        //    /// <summary>
        //    /// Generic method to call stored procedures without return values or output parameters
        //    /// </summary>
        //    /// <param name="storedProcedureName"></param>
        //    /// <param name="arrParam"></param>
        //    /// <returns></returns>
        //    public bool ExecuteNonQuery(string storedProcedureName, SqlParameter[] arrParam = null)
        //    {
        //        try
        //        {
        //            using (_connection)
        //            {
        //                using (SqlCommand command = new SqlCommand(storedProcedureName, _connection))
        //                {
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    _connection.Open();
        //                    if (arrParam != null && arrParam.Any())
        //                    {
        //                        command.Parameters.AddRange(arrParam);
        //                    }
        //                    command.ExecuteNonQuery();
        //                    return true;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //
    }
}