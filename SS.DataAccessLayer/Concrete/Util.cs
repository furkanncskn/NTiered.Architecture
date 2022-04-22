using System.Data;

namespace SS.DataAccessLayer.Concrete
{
    public static class Util
    {
        public static bool IsValidTable(DataTable table)
        {
            if (table == null || table.Rows.Count < 1 || table.Columns.Count < 1)
                return false;

            return true;
        }
    }
}
