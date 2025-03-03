using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookApp.DataAccess.Data;
using BookApp.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbset = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperty = null, bool tracked = false)
        {
            IQueryable<T> query;

            if (tracked)
            {
                query = _dbset;
            }
            else
            {
                query = _dbset.AsNoTracking();
            }

            query = _dbset.Where(filter);

            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter,string? includeProperty = null)
        {
            IQueryable<T> query = _dbset;
            if(filter != null)
            {
                query = _dbset.Where(filter);
            }


            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
    }
}
