using Spotzer.DataLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.DataLayer.UnitOfWork
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private IDataBaseContext  _databaseContext;
        private DbSet<T> _dbSet;

        public Repository(IDataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = (_databaseContext).Set<T>();

        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public T Find(object id)
        {
            return _dbSet.Find(id);
        }

        public T Insert(T entity)
        {
            return _dbSet.Add(entity);
        }

        public IQueryable<T> Queryable(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null
               ? _dbSet.AsQueryable().Where(predicate)
               : _dbSet.AsQueryable();
        }

        public void Update(T entity)
        {
            (_databaseContext as DataBaseContext).Set<T>().Attach(entity);
            (_databaseContext as DataBaseContext).Entry(entity).State = EntityState.Modified;
        }
    }
}
