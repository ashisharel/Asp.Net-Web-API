using System.Data;

namespace LibertyWebAPI.DataModel
{
    public class Parameter
    {
        public string Name
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }

        public SqlDbType DbType
        {
            get;
            private set;
        }

        public ParameterDirection ParameterDirection
        {
            get;
            private set;
        }

        public int? Size
        {
            get;
            set;
        }

        public Parameter(string name, string value, SqlDbType dbtype, ParameterDirection direction)
        {
            this.Name = name;
            this.Value = value;
            this.DbType = dbtype;
            this.ParameterDirection = direction;
            this.Size = null;
        }

        public Parameter(string name, string value, SqlDbType dbtype, ParameterDirection direction, int size)
        {
            this.Name = name;
            this.Value = value;
            this.DbType = dbtype;
            this.ParameterDirection = direction;
            this.Size = size;
        }
    }
}
