using System.Data;

namespace LibertyWebAPI.DataModel
{
    public class OutputParameter
    {
        public string Name
        {
            get;
            private set;
        }

        public object Value
        {
            get;
            private set;
        }

        public object SqlValue
        {
            get;
            private set;
        }

        public SqlDbType DbType
        {
            get;
            private set;
        }

        public OutputParameter(string name, object value, SqlDbType dbtype, object sqlValue)
        {
            this.Name = name;
            this.Value = value;
            this.DbType = dbtype;
            this.SqlValue = sqlValue;
        }

    }
}
