using SS.DataAccessLayer.Abstract;
using SS.DataAccessLayer.Concrete;
using System.Configuration;
using System.Data.Common;

namespace SS.WinForm.UI.Common
{
    public static class DBProvider<T> where T : class, new()
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["SpaceSurgeon"].ToString();

        private static IDataAccess<T> _dataAccess = new DataAccess<T>();
        public static IDataAccess<T> DB { get { return _dataAccess; } set { _dataAccess = value; } }
    }
}
