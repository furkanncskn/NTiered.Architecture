using System.Collections.Generic;
using System.Data;

namespace SS.BusinessLogicLayer.Commen
{
    public interface IBBL<T> where T : class, new()
    {
        bool Insert(T entity);
        List<T> SelectAll();
        T SelectById(int id);
        DataTable SelectByIdToTable(int id);
        bool Update(T entity);
        bool DeleteById(int id);    
        bool DeleteAll();
        int GetCount();
    }
}
