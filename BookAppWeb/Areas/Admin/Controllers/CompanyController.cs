using BookApp.DataAccess.Repository.IRepository;
using BookApp.Utilites;
using BooksApp.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAppWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Company> Companies = _unitOfWork.Company.GetAll().ToList();

            return View(Companies);
        }
        public IActionResult Upsert(int? id)
        {


            if (id == 0 || id == null)
            {
                //Create Function
                return View(new Company());
            }

            else
            {
                //update functionality
                var companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }


        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(company);
                _unitOfWork.Save();
                TempData["success"] = "Book Has Been Updated !";
                return RedirectToAction("Index");
            }
            else
            {
                return View(new Company());
            };


        }


        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Company CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }


            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting Successful" });
        }
        #endregion
    }
}
