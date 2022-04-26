using SS.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.DataAccessLayer.Concrete
{
    public static class DBProvider
    {
        private static string _connectionString = "";

        private static IDataAccess _dataAccess = null;

        public static string connectionString 
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

        public static IDataAccess DB 
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
