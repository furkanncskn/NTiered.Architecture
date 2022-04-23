using SS.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SS.DataAccessLayer.Concrete
{
    public class DataAccess: IDataAccess
    {
        public void AddSqlParameter(SqlCommand command, string name, ParameterDirection direction, SqlDbType type, object value)
        {
            AddSqlParameter(command, name, direction, type, value);
        }
        
        public void AddSqlParameter(SqlCommand command, string name, ParameterDirection direction, SqlDbType type, object value, int size = -1)
        {
            command.Parameters.Add(name);

            command.Parameters[name].Direction = direction;
            command.Parameters[name].Value = value;
            command.Parameters[name].SqlDbType = type;

            if (size != -1)
                command.Parameters[name].Size = size;
        }

        public SqlCommand CreateCommand(CommandType type, SqlConnection connection)
        {
            return CreateCommand(type, connection, null);
        }

        public SqlCommand CreateCommand(CommandType type, SqlConnection connection, string commandText = null)
        {
            try
            {
                SqlCommand command = new SqlCommand()
                {
                    CommandText = commandText,
                    CommandType = type,
                    Connection = connection
                };

                return command;
            }
            catch
            {
                return new SqlCommand();
            }
        }
        
        public int NonQueryCommand(string connectionString, string command, CommandType type, params object[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = CreateCommand(type, connection))
                    {
                        comm.CommandText = string.Format(command, parameters);

                        if (connection.State == ConnectionState.Closed) connection.Open();

                        int affectedRow = comm.ExecuteNonQuery();

                        if (connection.State == ConnectionState.Open) connection.Close();

                        return affectedRow;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public DataTable TableFromQuery(string connectionString, string query, CommandType type, params object[] parameters)
        {
            return ToDataTable(connectionString, query, type, parameters);
        }

        public object ToScalerValue(string connectionString, string query, CommandType type, params object[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = CreateCommand(type, connection))
                    {
                        comm.CommandText = string.Format(query, parameters);

                        if (connection.State == ConnectionState.Closed) connection.Open();

                        object value = comm.ExecuteScalar();

                        if (connection.State == ConnectionState.Open) connection.Close();

                        return value;
                    }
                }
            }
            catch
            {
                return new object();
            }
        }

        public DataTable ToDataTable(string connectionString, string query, CommandType type, params object[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = CreateCommand(type, connection))
                    {
                        command.CommandText = string.Format(query, parameters);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if(Util.IsValidTable(table))
                                return table;

                            return new DataTable();
                        }
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
