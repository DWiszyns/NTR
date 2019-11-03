using System;
using System.Collections;


namespace Z02.Repositories
{
    public interface IRepository<T, ID> 
    {
        T FindById(ID id);
        IEnumerable FindAll();

        void Update(T old_obj, T new_obj);

        void Save(T obj);

        void Delete(ID id);
    }
}