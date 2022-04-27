using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Entity.Attribute;
using SS.DataAccessLayer.Concrete;
using System.Linq;

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

                using (SqlCommand command = DBProvider.DB.CreateCommand(
                        CommandType.StoredProcedure,
                        String.Format("sp_insert_{0}", name)
                   ))
                {
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        Type proType = property.GetType();

                        object value = property.GetValue(entity);

                        if (property.IsThereAnAttribute(typeof(KeyAttribute)) == true) // primary key check
                        {
                            continue;
                        }

                        SqlDbType propertyType = Entity.Util.GetDbType(property.PropertyType);

                        command.AddSqlParameter(property.Name, ParameterDirection.Input, propertyType, value ?? DBNull.Value);
                    }

                    return DBProvider.DB.NonQueryCommand(command, DBProvider.connectionString) > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(T entity)
        {
            Type type = typeof(T);

            string name = type.GetAttributeValue((TableAttribute info) => info.Name);

            PropertyInfo[] properties = type.GetProperties();

            if (properties.Length == 0) { return false; }

            using (SqlCommand command = DBProvider.DB.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: string.Format("sp_update_{0}_by_id", name
                    )))
            {
                Type[] propertyTypes = properties.Select(p => p.PropertyType).ToArray();

                object[] propertyValues = properties.Select(p => p.GetValue(entity)).ToArray();

                for (int i = 0; i < propertyValues.Length; i++)
                {
                    DBCommand.AddSqlParameter(
                            command, properties[i].Name, 
                            ParameterDirection.Input, 
                            Entity.Util.GetDbType(propertyTypes[i]),
                            propertyValues[i] ?? DBNull.Value
                        );
                }

                if (!(DBProvider.DB.NonQueryCommand(command, DBProvider.connectionString) > 0)) { return false; }
            }
            
            return true;
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
