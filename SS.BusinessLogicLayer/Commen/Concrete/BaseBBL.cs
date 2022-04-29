using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Data.Common;
using System.Collections.Generic;

using Entity.Attribute;
using SS.DataAccessLayer.Concrete;

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

                string name = GetEntityName(type);   

                using (DbCommand command = DbProvider.Db.CreateCommand(
                              type: CommandType.Text,
                              commandText: String.Format("select * from v_get_all_{0}", type.Name)
                          ))
                {
                    if (command == null) throw new NullReferenceException();

                    var table = DbProvider.Db.TableFromQuery(command);
                    
                    return DataConvert<T>.ToListFromDataTable(table);
                };
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

                string name = GetEntityName(type);

                using (DbCommand command = DbProvider.Db.CreateCommand(
                            type: CommandType.StoredProcedure,
                            commandText: String.Format("sp_get_{0}_by_id", name)
                        ))
                {
                    if (command == null) throw new NullReferenceException();

                    string KeyName = type.GetProperties()
                        .Where(p => p.IsThereAnAttribute(typeof(KeyAttribute)) == true)
                        .FirstOrDefault().Name;

                    if (KeyName == null) { throw new NullReferenceException(); }

                    command.AddDbParameter(
                            name: KeyName,
                            direction: ParameterDirection.Input,
                            type: DbType.Int64,
                            value: id
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
            return UpdateAndInsert(entity, "sp_insert_{0}");
        }

        /// <summary>
        /// String.Format("sp_update_{0}_by_id", name), Stored Procedure
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            return UpdateAndInsert(entity, "sp_update_{0}_by_id");
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

                string name = GetEntityName(type);   

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_delete_{0}_by_id", name)
                    ))
                {
                    if (command == null) throw new NullReferenceException();

                    PropertyInfo property = type.GetProperties()
                        .Where(p => p.IsThereAnAttribute(typeof(KeyAttribute)) == true)
                        .FirstOrDefault();

                    if (property == null) { throw new Exception(); }

                    command.AddDbParameter(
                            name: property.Name,
                            direction: ParameterDirection.Input,
                            type: Util.GetDbType(property.PropertyType),
                            value: id
                        );

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

                string name = GetEntityName(type);

                using(DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_delete_all_{0}", name)
                    ))
                {
                    if (command == null) throw new NullReferenceException();

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

                string name = GetEntityName(type);

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format("sp_get_count_{0}", name)
                    ))
                {
                    if (command == null) throw new NullReferenceException();

                    var returnValue = command.AddDbParameter(
                            direction: ParameterDirection.ReturnValue, 
                            type:DbType.Int32
                        );

                    DbProvider.Db.NonQueryCommand(command);

                    return (int)returnValue.Value;
                };
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Commen insert and update code 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="templateCommandText">
        /// Update: ["sp_update_{0}_by_id", name]
        /// Insert: ["sp_insert_{0}", name]
        /// </param>
        /// <returns></returns>
        private static bool UpdateAndInsert(T entity, string templateCommandText)
        {
            try
            {
                Type type = typeof(T);

                string name = GetEntityName(type);

                using (DbCommand command = DbProvider.Db.CreateCommand(
                        type: CommandType.StoredProcedure,
                        commandText: String.Format(templateCommandText, name)
                    ))
                {
                    if (command == null) throw new NullReferenceException();
                    
                    PropertyInfo[] properties = type.GetProperties();
                    
                    if (properties.Length == 0) { throw new ArgumentOutOfRangeException(); }

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
        /// Get entity name
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string GetEntityName(Type type)
        {
            string name = type.GetAttributeValue((TableAttribute info) => info.Name);

            if (name == null) { throw new Exception(); }

            return name;
        }
    }
}
