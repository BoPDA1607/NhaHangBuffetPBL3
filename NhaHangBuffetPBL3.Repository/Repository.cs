using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using System.Linq.Expressions;

namespace NhaHangBuffetPBL3.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NhaHangBuffetContext _db;
        internal DbSet<T> dbSet;
        
        public Repository(NhaHangBuffetContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Find(object ID)
        {
            return dbSet.Find(ID);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public bool Any(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

     

        public IEnumerable<T> Where(Func<T, bool> value)
        {
            return dbSet.Where(value);
        }

        IEnumerable<object> IRepository<T>.Select(Func<T, object> filter)
        {
            return dbSet.Select(filter);
        }

        public IEnumerable<T> GetRecords(Func<T, bool> filter)
        {
            return dbSet.Where(filter);
        }
        public int Count(Func<T, bool> filter)
        {
            return dbSet.Count(filter);
        }
    }
}
