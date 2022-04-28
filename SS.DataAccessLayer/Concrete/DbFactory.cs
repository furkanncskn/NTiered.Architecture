using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace SS.DataAccessLayer.Concrete
{
    public class DbFactory
    {
        public static DbConnection CreateConnection(DbProviderType dbProviderType)
        {
            switch (dbProviderType)
            {
                case DbProviderType.NullProvider:
                    throw new NotSupportedException("Not selected provider");
                case DbProviderType.SqlServer:
                    return new SqlConnection();
                case DbProviderType.PostgreSql: 
                    throw new NotImplementedException("PostgreSql is not supported yet");

                default: throw new NotSupportedException("Not supported provider");
            }
        }

        public static DbDataAdapter CreateDataAdapter(DbProviderType dbProviderType)
        {
            switch (dbProviderType)
            {
                case DbProviderType.NullProvider:
                    throw new NotSupportedException("Not selected provider");
                case DbProviderType.SqlServer:
                    return new SqlDataAdapter();
                case DbProviderType.PostgreSql:
                    throw new NotImplementedException("PostgreSql is not supported yet");

                default:
                    throw new NotSupportedException("Not supported provider");
            }
        }
    }

    public enum DbProviderType
    {
        NullProvider,
        SqlServer,
        PostgreSql
    }
}
