using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.DataAccess.Data;
using BookApp.DataAccess.Repository.IRepository;
using BookApp.Models.Models;

namespace BookApp.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCartObj)
        {
            _db.ShoppingCarts.Update(shoppingCartObj);
        }
    }
}
