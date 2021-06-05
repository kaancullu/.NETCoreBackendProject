using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

/// <summary>
/// Core katmanı diğer katmanları refereasn almaz çünkü bağımlı olur. 
/// Biz bu katmanı diğer projelerde de kullanmak istediğimiz için diğer katmnalardan bağımsız yaparız.
/// </summary>


namespace Core.DataAccess
{
    //generic constraint
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null); 
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
