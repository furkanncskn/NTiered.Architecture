using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SS.DataAccessLayer.Abstract
{
    public interface IDataAccess
    {
        int NonQueryCommand(string connectionString, string command, CommandType type, params object[] parameters);

        int NonQueryCommand(SqlCommand command, string connectionString);

        DataTable TableFromQuery(string connectionString, string query, CommandType type, params object[] parameters);
        
        DataTable TableFromQuery(SqlCommand command, string connectionString);

        object ToScalerValue(string connectionString, string query, CommandType type, params object[] parameters);
       
        SqlCommand CreateCommand(CommandType type, SqlConnection connection);
       
        SqlCommand CreateCommand(CommandType type, SqlConnection connection, string commandText);
          
        DataTable ToDataTable(string connectionString, string query, CommandType type, params object[] parameters);

        DataTable ToDataTable(SqlCommand command, string connectionString = null, string query = null, CommandType type = CommandType.Text);

    }
}
