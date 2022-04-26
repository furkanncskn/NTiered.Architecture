using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

using Entity.Attribute;
using SS.DataAccessLayer.Concrete;

namespace SS.BusinessLogicLayer.Commen
{
    public class BaseBBL<T> : IBBL<T> where T : class, new()
    {
        public List<T> SelectAll()
        {
            try
            {
                Type type = typeof(T);

                string name = typeof(T).GetAttributeValue((TableAttribute info) => info.Name);

                SqlCommand command = DBProvider.DB.CreateCommand(
                        CommandType.Text,
                        String.Format("select * from v_get_all_{0}", type.Name));

                DataTable table = DBProvider.DB.TableFromQuery(command, DBProvider.connectionString);

                return DataConvert<T>.ToListFromDataTable(table);
            }
            catch 
            {

                return new List<T>();
            }
        }
        
        public DataTable SelectByIdToTable(int id)
        {
            try
            {
                Type type = typeof(T);

                string name = typeof(T).GetAttributeValue((TableAttribute info) => info.Name);

                SqlCommand command = DBProvider.DB.CreateCommand(
                        CommandType.StoredProcedure,
                        String.Format("sp_get_{0}_by_id", name));

                string propName = null;

                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (property.IsThereAnAttribute(typeof(KeyAttribute)))
                    {
                        propName = property.Name;
                    }
                }

                if (propName == null)
                {
                    return new DataTable();
                }

                command.AddSqlParameter(
                        propName,
                        ParameterDirection.Input,
                        SqlDbType.Int,
                        id
                    );

                return DBProvider.DB.TableFromQuery(command, DBProvider.connectionString);
            }
            catch
            {
                return new DataTable();
            }
        }

        public T SelectById(int id)
        {
            DataTable table = SelectByIdToTable(id);

            return DataConvert<T>.TableFirstRowToClass(table);
        }

        public bool Insert(T entity)
        {
            try
            {
                Type type = typeof(T);

                string name = type.GetAttributeValue((TableAttribute info) => info.Name);

                SqlCommand command = DBProvider.DB.CreateCommand(
                        CommandType.StoredProcedure,
                        String.Format("sp_insert_{0}", name)
                   );

                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Type proType = property.GetType();

                    if (Nullable.GetUnderlyingType(property.PropertyType) != null) // nullable check
                    {
                        if (property.GetValue(entity) == null)
                        {
                            continue;
                        }
                    }

                    object value = property.GetValue(entity);
                    if (value == null)
                    {
                        continue;
                    }

                    if (property.IsThereAnAttribute(typeof(KeyAttribute)) == true) // key check
                    {
                        continue;
                    }

                    command.AddSqlParameter(property.Name, ParameterDirection.Input, value);
                }

                return DBProvider.DB.NonQueryCommand(command, DBProvider.connectionString) > 0;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
        
        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
