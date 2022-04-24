using SS.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SS.DataAccessLayer.Concrete
{
    public class DataAccess : IDataAccess
    {
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

        public int NonQueryCommand(SqlCommand command, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    command.Connection = connection;

                    int affectedRow = command.ExecuteNonQuery();

                    if (connection.State == ConnectionState.Open) connection.Close();

                    return affectedRow;
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

        public DataTable TableFromQuery(SqlCommand command, string connectionString)
        {
            return ToDataTable(command, connectionString);
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

        public DataTable ToDataTable(SqlCommand command, string connectionString, string query = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if(query != null)
                        command.CommandText = query;

                    if(type != CommandType.Text)
                        command.CommandType = type;

                    command.Connection = connection;    
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (Util.IsValidTable(table))
                            return table;

                        return new DataTable();
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
