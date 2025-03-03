using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // Get All Data From <T>
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter =null, string? includeProperty = null);

        // Get One Record
        T Get(Expression<Func<T, bool>> filter, string? includeProperty = null, bool tracked = false);

        // Insert Data
        void Add(T entity);

        // Delete
        void Remove(T entity);

        // Delete Multiple Records
        void RemoveRange(IEnumerable<T> entities);
    }
}
