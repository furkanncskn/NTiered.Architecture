using System.Data;
using System.Data.Common;

namespace SS.DataAccessLayer.Concrete
{
    public static class DbCommandExtensions
    {
        public static DbParameter AddDbParameter(
                this DbCommand command, 
                string name, 
                ParameterDirection direction,
                DbType type, 
                object value
            )
        {
            return AddDbParameter(command, name, direction, type, value, -1);
        }

        public static DbParameter AddDbParameter(
                this DbCommand command, 
                string name, 
                ParameterDirection direction, 
                DbType type, 
                object value, 
                int size
            )
        {
            var param = command.CreateParameter();

            param.Direction = direction;
            param.ParameterName = name;
            param.Value = value;
            param.DbType = type;

            if (size != -1)
                param.Size = size;

            command.Parameters.Add(param);

            return param;
        }

        public static DbParameter AddDbParameter(
                this DbCommand command, 
                string name, 
                ParameterDirection direction, 
                object value
            )
        {
            var param = command.CreateParameter();
            param.Direction = direction;
            param.ParameterName = name;
            param.Value = value;

            command.Parameters.Add(param);

            return param;
        }

        public static DbParameter AddDbParameter(
            this DbCommand command,
            ParameterDirection direction,
            DbType type
            )
        {
            var param = command.CreateParameter();
            param.Direction = direction;

            command.Parameters.Add(param);

            return param;
        }
    }
}
