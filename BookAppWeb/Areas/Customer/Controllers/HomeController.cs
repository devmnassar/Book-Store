using System.Diagnostics;
using System.Security.Claims;
using BookApp.DataAccess.Repository;
using BookApp.DataAccess.Repository.IRepository;
using BookApp.Models.Models;
using BookApp.Utilites;
using BookAppWeb.Models;
using BooksApp.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAppWeb.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Getting Product List 

            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperty: "Category");
            return View(productList.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int productId)
        {
            // Getting The Product With Category (AS Detail) , Based On Product ID

            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperty: "Category"),
                Count = 1,
                ProductId = productId
            };
          
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // Getting User Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);
            
            if (cartFromDb != null) 
            {
                //Update If Shopping Cart Exitst
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {

                //Create If Shopping Cart Not Exists
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();

                // Setting Session For User , To Count The ShoppingCart
                
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            TempData["success"] = "Cart Updated Successfully";
            
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
