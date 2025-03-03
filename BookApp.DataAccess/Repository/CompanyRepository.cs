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
    public class CompanyRepository : Repository<Company>,ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _dbset.Update(obj);
        }
    }
}
