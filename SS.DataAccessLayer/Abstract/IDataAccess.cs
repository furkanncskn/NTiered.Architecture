using System.Data;
using System.Data.Common;

namespace SS.DataAccessLayer.Abstract
{
    public interface IDataAccess
    {
        int NonQueryCommand(string command, CommandType type, params object[] parameters);

        int NonQueryCommand(DbCommand command);

        DataTable TableFromQuery(string query, CommandType type, params object[] parameters);
        
        DataTable TableFromQuery(DbCommand command);

        object ToScalerValue(string query, CommandType type, params object[] parameters);

        DbCommand CreateCommand(CommandType type);

        DbCommand CreateCommand(CommandType type,string commandText);

        DataTable ToDataTable(DbCommand command);

        DataTable ToDataTable(DbCommand command, string query);

        DataTable ToDataTable(DbCommand command, string query, CommandType type);

        DataTable ToDataTable(string query, CommandType type, params object[] parameters);
    }
}
