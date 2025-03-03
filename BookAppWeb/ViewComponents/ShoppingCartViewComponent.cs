using System.Security.Claims;
using BookApp.DataAccess.Repository.IRepository;
using BookApp.Utilites;
using Microsoft.AspNetCore.Mvc;

namespace BookAppWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Reset OR Populate ShoppingCart when user is Not (Logged In Or Logged Out)

            // Getting Claim
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Check If Logged In 
            if (claim != null) // User Logged In
            {

                // Check For Session If null ,, We Get From Database and Set To A Session
                if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }

                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }

            // User Not Logged In Or Loged Out
            else
            {
                // Reset Session
                HttpContext.Session.Clear();
                return View(0);
            }
            
        }
    }
}
