using System.Data;
using System.Data.Common;

namespace SS.DataAccessLayer.Abstract
{
    public interface IDataAccess
    {
        int NonQueryCommand(CommandType type, string commandText, params object[] parameters);

        int NonQueryCommand(DbCommand command);

        DataTable TableFromQuery(CommandType type, string query, params object[] parameters);
        
        DataTable TableFromQuery(DbCommand command);

        object ToScalerValue(CommandType type, string query, params object[] parameters);

        object ToScalerValue(DbCommand command);

        DbCommand CreateCommand(CommandType type);

        DbCommand CreateCommand(CommandType type,string commandText);

        DataTable ToDataTable(DbCommand command);

        DataTable ToDataTable(DbCommand command, string query);

        DataTable ToDataTable(DbCommand command, CommandType type, string query);

        DataTable ToDataTable(CommandType type, string query, params object[] parameters);
    }
}
