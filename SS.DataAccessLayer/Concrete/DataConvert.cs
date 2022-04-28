using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SS.DataAccessLayer.Concrete
{
    public static class DataConvert<T> where T : class, new()
    {
        public static T TableSelectedIndexToClass(DataTable table, int idx)
        {
            try
            {
                Type type = typeof(T);

                T instance = new T();

                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object value = table.Rows[idx][property.Name];

                    if (value == DBNull.Value)
                        continue;

                    property.SetValue(instance, value);
                }

                return instance;
            }
            catch 
            {
                return new T();
            }
        }

        public static T TableFirstRowToClass(DataTable table)
        {
            return TableSelectedIndexToClass(table, 0);
        }

        public static List<T> ToListFromDataTable(DataTable table)
        {
            if (!Util.IsValidTable(table))
            {
                return new List<T>();
            }

            try
            {
                List<T> list = new List<T>();

                for(int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(TableSelectedIndexToClass(table, i));
                }

                return list;
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
