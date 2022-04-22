using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SS.DataAccessLayer.Abstract
{
    public interface IDataAccess<T> where T : class, new()
    {
        int NonQueryCommand(string connectionString, string command, CommandType type, params object[] parameters);
        
        DataTable TableFromQuery(string connectionString, string query, CommandType type, params object[] parameters);
        
        List<T> ListFromQuery(string connectionString, string query, CommandType type, params object[] parameters);
       
        object ToScalerValue(string connectionString, string query, CommandType type, params object[] parameters);
       
        SqlCommand CreateCommand(CommandType type, SqlConnection connection);
       
        SqlCommand CreateCommand(CommandType type, string commandText, SqlConnection connection);
        
        void AddSqlParameter(SqlCommand command, string name, ParameterDirection direction, SqlDbType type, object value, int size);
       
        void AddSqlParameter(SqlCommand command, string name, ParameterDirection direction, SqlDbType type, object value);
        
        DataTable ToDataTable(string connectionString, string query, CommandType type, params object[] parameters);
       
        List<T> ToListFromDataTable(DataTable table);
    }
}
