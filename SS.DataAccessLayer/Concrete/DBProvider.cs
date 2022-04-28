using SS.DataAccessLayer.Abstract;
using System.Data.Common;

namespace SS.DataAccessLayer.Concrete
{
    public class DbProvider
    {
        private static string _connectionString = "";

        private static IDataAccess _dataAccess = null;

        private static DbConnection _connection = null;

        private static DbDataAdapter _dataAdapter = null;
        
        private static DbProviderType _dbProviderType = DbProviderType.NullProvider;

        public static DbProviderType ProviderType { get { return _dbProviderType; } set { _dbProviderType = value; } }
        
        public static string ConnectionString 
        { 
            get 
            {
                return _connectionString; 
            } 
            set 
            { 
                _connectionString = value; 
            } 
        }

        public static DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = DbFactory.CreateConnection(ProviderType);
                }
                
                return _connection;
            }
        }

        public static DbDataAdapter DataAdapter
        {
            get
            {
                if (_dataAdapter == null)
                {
                    _dataAdapter = DbFactory.CreateDataAdapter(ProviderType);
                }

                return _dataAdapter;
            }
        }

        public static IDataAccess Db 
        { 
            get 
            { 
                if (_dataAccess == null) 
                {
                    _dataAccess = new DataAccess();
                } 
                
                return _dataAccess; 
            } 
            set 
            { 
                _dataAccess = value; 
            } 
        }
    }
}
