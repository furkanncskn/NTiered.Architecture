using System;
using System.Collections.Generic;
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

        private static readonly Dictionary<Type, DbType> typeMap;

        // Create and populate the dictionary in the static constructor
        static Util()
        {
            typeMap = new Dictionary<Type, DbType>
            {
                [typeof(string)] = DbType.String,
                [typeof(char[])] = DbType.String,
                [typeof(byte)] = DbType.Byte,
                [typeof(short)] = DbType.Int16,
                [typeof(int)] = DbType.Int32,
                [typeof(long)] = DbType.Int64,
                [typeof(byte[])] = DbType.Binary,
                [typeof(bool)] = DbType.Boolean,
                [typeof(DateTime)] = DbType.DateTime2,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(TimeSpan)] = DbType.Time
            };
            /* ... and so on ... */
        }

        // Non-generic argument-based method
        public static DbType GetDbType(Type giveType)
        {
            // Allow nullable types to be handled
            giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

            if (typeMap.ContainsKey(giveType))
            {
                return typeMap[giveType];
            }

            throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
        }

        // Generic version
        public static DbType GetDbType<T>()
        {
            return GetDbType(typeof(T));
        }
    }
}
