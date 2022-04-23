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
        public static List<T> ToListFromDataTable(DataTable table)
        {
            if (!Util.IsValidTable(table))
            {
                return new List<T>();
            }

            try
            {
                List<T> list = new List<T>();

                Type type = typeof(T);

                PropertyInfo[] properties = type.GetProperties();

                foreach (DataRow row in table.Rows)
                {
                    T instance = new T();
                    foreach (PropertyInfo property in properties)
                    {
                        object value = System.Convert.ChangeType(
                                   row[property.Name],
                                   property.PropertyType
                               );

                        property.SetValue(instance, value, null);
                    }
                    list.Add(instance);
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
