using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.DataAccess.Data;
using BookApp.DataAccess.Repository.IRepository;
using BooksApp.Models.Models;

namespace BookApp.DataAccess.Repository
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _dbset.Update(obj);
        }
    }
}
