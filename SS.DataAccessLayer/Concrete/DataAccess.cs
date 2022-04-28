﻿using System.Data;
using System.Data.Common;
using SS.DataAccessLayer.Abstract;

namespace SS.DataAccessLayer.Concrete
{
    public class DataAccess : IDataAccess
    {
        public int NonQueryCommand(string commandText, CommandType type, params object[] parameters)
        {
            try
            {
                DbConnection connection = DbProvider.Connection;

                connection.ConnectionString = DbProvider.ConnectionString;

                using (DbCommand comm = CreateCommand(type))
                {
                    comm.CommandText = string.Format(commandText, parameters);

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    int affectedRow = comm.ExecuteNonQuery();

                    if (connection.State == ConnectionState.Open) connection.Close();

                    return affectedRow;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int NonQueryCommand(DbCommand command)
        {
            try
            {
                DbConnection connection = DbProvider.Connection;
                 
                connection.ConnectionString = DbProvider.ConnectionString;

                if (connection.State == ConnectionState.Closed) connection.Open();

                command.Connection = connection;

                int affectedRow = command.ExecuteNonQuery();

                if (connection.State == ConnectionState.Open) connection.Close();

                return affectedRow;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable TableFromQuery(DbCommand command)
        {
            return ToDataTable(command);
        }
        
        public DataTable TableFromQuery(string query, CommandType type, params object[] parameters)
        {
            return ToDataTable(query, type, parameters);
        }

        public object ToScalerValue(string query, CommandType type, params object[] parameters)
        {
            try
            {
                DbConnection connection = DbProvider.Connection;
                
                connection.ConnectionString = DbProvider.ConnectionString;

                using (DbCommand comm = CreateCommand(type))
                {
                    comm.CommandText = string.Format(query, parameters);

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    object value = comm.ExecuteScalar();

                    if (connection.State == ConnectionState.Open) connection.Close();

                    return value;
                }
            }
            catch
            {
                return new object();
            }
        }

        public DbCommand CreateCommand(CommandType type)
        {
            return CreateCommand(type, null);
        }

        public DbCommand CreateCommand(CommandType type, string commandText)
        {
            var cmd = DbProvider.Connection.CreateCommand();

            cmd.CommandType = type;
            
            if(commandText != null) cmd.CommandText = commandText;  
            
            return cmd;
        }

        public DataTable ToDataTable(DbCommand command)
        {
            return ToDataTable(command, null, CommandType.Text);
        }

        public DataTable ToDataTable(DbCommand command, string query)
        {
            return ToDataTable(command, query, CommandType.Text);
        }

        public DataTable ToDataTable(DbCommand command, string query, CommandType type)
        {
            try
            {
                using (DbConnection connection = DbProvider.Connection)
                {
                    connection.ConnectionString = DbProvider.ConnectionString;

                    if (query != null)
                        command.CommandText = query;

                    if (type != CommandType.Text)
                        command.CommandType = type;

                    command.Connection = connection;

                    using (DbDataAdapter adapter = DbProvider.DataAdapter)
                    {
                        adapter.SelectCommand = command;

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

        public DataTable ToDataTable(string query, CommandType type, params object[] parameters)
        {
            try
            {
                using (DbConnection connection = DbProvider.Connection)
                {
                    connection.ConnectionString = DbProvider.ConnectionString;

                    using (DbCommand command = CreateCommand(type))
                    {
                        command.CommandText = string.Format(query, parameters);

                        using (DbDataAdapter adapter = DbProvider.DataAdapter)
                        {
                            adapter.SelectCommand = command;
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (Util.IsValidTable(table))
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
