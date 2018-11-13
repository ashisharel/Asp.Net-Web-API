using LibertyWebAPI.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DataModel
{
    public static class ExtensionMethods
    {
        //public static SqlParameter[] ToParameterArray(this Catalog catalog)
        //{
            //return new SqlParameter[] { 
            //    new SqlParameter("@Session_Id", catalog.CatalogID)};
        //}

        //public static SqlParameter[] ToParameterArray(this Employee employee)
        //{
        //    return new SqlParameter[]
        //    {
        //        new SqlParameter("@EmployeeID", employee.EmployeeID)
        //        , new SqlParameter("@LastName", employee.LastName)
        //        , new SqlParameter("@FirstName", employee.FirstName)
        //        , new SqlParameter("@Title", employee.Title ?? (object)DBNull.Value)
        //        , new SqlParameter("@TitleOfCourtesy", employee.TitleOfCourtesy ?? (object)DBNull.Value)
        //        , new SqlParameter("@BirthDate", employee.BirthDate ?? (object)DBNull.Value)
        //        , new SqlParameter("@HireDate", employee.HireDate ?? (object)DBNull.Value)
        //        , new SqlParameter("@Address", employee.Address ?? (object)DBNull.Value)
        //        , new SqlParameter("@City", employee.City ?? (object)DBNull.Value)
        //        , new SqlParameter("@Region", employee.Region ?? (object)DBNull.Value)
        //        , new SqlParameter("@PostalCode", employee.PostalCode ?? (object)DBNull.Value)
        //        , new SqlParameter("@Country", employee.Country ?? (object)DBNull.Value)
        //        , new SqlParameter("@HomePhone", employee.HomePhone ?? (object)DBNull.Value)
        //        , new SqlParameter("@Extension", employee.Extension ?? (object)DBNull.Value)
        //        , new SqlParameter("@Photo", employee.Photo ?? (object)DBNull.Value)
        //        , new SqlParameter("@Notes", employee.Notes ?? (object)DBNull.Value)
        //        , new SqlParameter("@PhotoPath", employee.PhotoPath ?? (object)DBNull.Value)
        //    };
        //}


        public static string ToNullable(this string value)
        { 
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return value.Trim();
        }

        /// <summary>
        /// converts a string from "Y" or "N" to boolean
        /// </summary>
        /// <param name="input"></param>
        /// <returns>"true" if the input is "y" or "Y" otherwise "false"</returns>
        public static bool ToBool(this string input)
        {
            return input.Equals("y", StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// Gets an <see cref="T:System.Int32" /> representation of the current string, or the given default value if the
        /// current string is not an <see cref="T:System.Int32" />.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <param name="defaultValue">The value to return if conversion fails.</param>
        /// <returns>An integer value if the string is an integer; otherwise the given default value.</returns>
        public static int ToInt(this string text, int defaultValue)
        {
            int? parsedValue = text.ToNullableInt();
            return (!parsedValue.HasValue ? defaultValue : parsedValue.Value);
        }
        /// <summary>
        /// Gets an <see cref="T:System.Int32" /> representation of the current string, or <c>null</c> if the
        /// current string is not an <see cref="T:System.Int32" />.
        /// </summary>
        /// <param name="text">the string to convert</param>
        /// <returns>An integer value, or <c>null</c> if the string could not be converted.</returns>
        public static int? ToNullableInt(this string text)
        {
            int parsedValue;
            int? nullable;
            if (!int.TryParse(text, out parsedValue))
            {
                nullable = null;
            }
            else
            {
                nullable = new int?(parsedValue);
            }
            return nullable;
        }

    }
}
