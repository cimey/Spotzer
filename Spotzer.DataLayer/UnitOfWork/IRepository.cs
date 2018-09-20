using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.DataLayer.UnitOfWork
{
    public interface IRepository<T> where T : class
    {
        T Find(object id);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object id);
        IQueryable<T> Queryable(Expression<Func<T, bool>> predicate = null);
    }
}
