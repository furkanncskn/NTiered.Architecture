using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.DataAccessLayer.Concrete
{
    public static class DBCommand
    {
        public static void AddSqlParameter(this SqlCommand command, string name, ParameterDirection direction, SqlDbType type, object value)
        {
            AddSqlParameter(command, name, direction, type, value, -1);
        }

        public static void AddSqlParameter(this SqlCommand command, string name, ParameterDirection direction, SqlDbType type, object value, int size)
        {
            SqlParameter parameter = new SqlParameter()
            {
                Direction = direction,
                ParameterName = name,
                Value = value,
                SqlDbType = type,
            };

            if (size != -1)
                parameter.Size = size;

            command.Parameters.Add(parameter);
        }

    }
}
