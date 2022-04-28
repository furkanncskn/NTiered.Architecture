using System.Data;
using System.Data.Common;

namespace SS.DataAccessLayer.Concrete
{
    public static class DbCommandExtensions
    {
        public static void AddDbParameter(
            this DbCommand command, 
            string name, 
            ParameterDirection direction,
            DbType type, 
            object value)
        {
            AddDbParameter(command, name, direction, type, value, -1);
        }

        public static void AddDbParameter(
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
        }

        public static void AddDbParameter(
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
        }
    }
}
