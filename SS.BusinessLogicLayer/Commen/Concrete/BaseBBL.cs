using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Entity.Attribute;
using SS.DataAccessLayer.Concrete;
using System.Linq;
using System.Data.Common;

namespace SS.BusinessLogicLayer.Commen
{
    public class BaseBBL<T> : IBBL<T> where T : class, new()
    {
        /// <summary>
        /// String.Format("select * from v_get_all_{0}", type.Name), View
        /// </summary>
        /// <returns></returns>
        public List<T> SelectAll()
        {
            try
            {
                var type = typeof(T);

                string name = typeof(T).GetAttributeValue((TableAttribute info) => info.Name);

                if (name == null) throw new Exception();

                DbCommand command = DbProvider.Db.CreateCommand(
                            type: CommandType.Text,
                            commandText: String.Format("select * from v_get_all_{0}", type.Name)
                        );
                
                var table = DbProvider.Db.TableFromQuery(command);

                return DataConvert<T>.ToListFromDataTable(table);
            }
            catch 
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// String.Format("sp_get_{0}_by_id", name), Stored Procedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable SelectByIdToTable(int id)
        {
            try
            {
                Type type = typeof(T);

                string name = typeof(T).GetAttributeValue((TableAttribute info) => info.Name);

                if (name == null) throw new Exception();

                using (DbCommand command = DbProvider.Db.CreateCommand(
                            type: CommandType.StoredProcedure,
                            commandText: String.Format("sp_get_{0}_by_id", name)
                        ))
                {
                    string KeyName = null;

                    PropertyInfo[] properties = type.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        if (property.IsThereAnAttribute(typeof(KeyAttribute)))
                        {
                            KeyName = property.Name;
                        }
                    }

                    if (KeyName == null) { throw new Exception(); }

                    command.AddDbParameter(
                            KeyName,
                            ParameterDirection.Input,
                            DbType.Int64,
                            id
                        );

                    return DbProvider.Db.TableFromQuery(command);
                }
            }
            catch
            {
                return new DataTable();
            }
        }
        
        /// <summary> 
        /// String.Format("sp_get_{0}_by_id", name), Stored Procedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T SelectById(int id)
        {
            DataTable table = SelectByIdToTable(id);

            return DataConvert<T>.ToObjectFromTableFirstRow(table);
        }

        /// <summary>
        /// String.Format("sp_insert_{0}", name), Stored Procedure
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(T entity)
        {
            try
            {
                Type type = typeof(T);

                string name = type.GetAttributeValue((TableAttribute info) => info.Name);

                if (name == null) { throw new Exception(); }

                PropertyInfo[] properties = type.GetProperties();
                
                if (properties.Length == 0) { throw new Exception(); }

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_insert_{0}", name)
                   ))
                {
                    Type[] propertyTypes = properties.Select(p => p.PropertyType).ToArray();

                    object[] propertyValues = properties.Select(p => p.GetValue(entity)).ToArray();

                    for (int i = 0; i < properties.Length; i++)
                    {
                        if(properties[i].IsThereAnAttribute(typeof(KeyAttribute))== true)
                        {
                            continue;
                        }

                        command.AddDbParameter(
                                name: properties[i].Name, 
                                direction: ParameterDirection.Input,
                                type: Util.GetDbType(propertyTypes[i]), 
                                value: propertyValues[i] ?? DBNull.Value
                            );
                    }

                    return DbProvider.Db.NonQueryCommand(command) > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// string.Format("sp_update_{0}_by_id", Stored Procedure
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            try
            {
                Type type = typeof(T);

                string name = type.GetAttributeValue((TableAttribute info) => info.Name);

                if (name == null) { throw new Exception(); }

                PropertyInfo[] properties = type.GetProperties();

                if (properties.Length == 0) { throw new Exception(); }

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_update_{0}_by_id", name)
                    ))
                {
                    Type[] propertyTypes = properties.Select(p => p.PropertyType).ToArray();

                    object[] propertyValues = properties.Select(p => p.GetValue(entity)).ToArray();

                    for (int i = 0; i < propertyValues.Length; i++)
                    {

                        command.AddDbParameter(
                                name: properties[i].Name,
                                direction: ParameterDirection.Input,
                                type: Util.GetDbType(propertyTypes[i]),
                                value: propertyValues[i] ?? DBNull.Value
                            );
                    }

                    return DbProvider.Db.NonQueryCommand(command) > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// String.Format("sp_delete_{0}_by_id", name)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            try
            {
                Type type = typeof(T);

                string name = type.GetAttributeValue((TableAttribute info) => info.Name);

                if (name == null) { throw new Exception(); }

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_delete_{0}_by_id", name)
                    ))
                {
                    PropertyInfo property = type.GetProperties()
                        .Where(p => p.IsThereAnAttribute(typeof(KeyAttribute)) == true).FirstOrDefault();

                    if (property == null) { throw new Exception(); }

                    command.AddDbParameter(
                        name: property.Name,
                        direction: ParameterDirection.Input,
                        type: Util.GetDbType(property.PropertyType),
                        value: id);

                    return DbProvider.Db.NonQueryCommand(command) > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Test edilmedi!!
        /// String.Format("sp_delete_all_{0}", name)
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            try
            {
                Type type = typeof(T);

                string name = type.GetAttributeValue((TableAttribute info) => info.Name);

                if (name == null) { throw new Exception(); }

                using(DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_delete_all_{0}", name)
                    ))
                {
                    return DbProvider.Db.NonQueryCommand(command) > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// String.Format("sp_get_count_{0}", name)
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            try
            {
                Type type = typeof (T);

                string name = type.GetAttributeValue((TableAttribute info) => info.Name);

                if(name == null) { throw new NullReferenceException(); }

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_get_count_{0}", name)
                    ))
                {
                    if (command == null) throw new NullReferenceException();

                    var returnValue = command.AddDbParameter(ParameterDirection.ReturnValue, DbType.Int32);

                    DbProvider.Db.NonQueryCommand(command);

                    return (int)returnValue.Value;
                };
            }
            catch
            {
                return 0;
            }
        }
    }
}
