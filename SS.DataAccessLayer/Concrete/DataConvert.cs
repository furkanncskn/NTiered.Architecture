using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SS.DataAccessLayer.Concrete
{
    public static class DataConvert<T> where T : class, new()
    {
        public static T TableSelectedIndexToClass(DataTable table, int idx)
        {
            Type type = typeof(T);

            T instance = new T();

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = table.Rows[idx][property.Name];

                if(value == DBNull.Value)
                    continue;

                property.SetValue(instance, value);
            }

            return instance;
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
            string asda = "";
            try
            {
                List<T> list = new List<T>();

                for(int i = 0; i < table.Rows.Count; i++)
                {
                    if (i == 2150)
                    {
                        int y = 10;
                    }

                    list.Add(TableSelectedIndexToClass(table, i));
                }

                return list;
            }
            catch(Exception ex)
            {
                asda = ex.Message;

                return new List<T>();
            }
        }

        public static T ChangeType(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }
    }
}
