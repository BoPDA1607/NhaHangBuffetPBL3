using System.Linq.Expressions;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        T Find(object ID);
        bool Any(Expression<Func<T, bool>> filter);
        IEnumerable<T> Where(Func<T, bool> value);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<object> Select(Func<T, object> filter);
        IEnumerable<T> GetRecords(Func<T, bool> filter);     
        int Count(Func<T, bool> filter);
    }
}
