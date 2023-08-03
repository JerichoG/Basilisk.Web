using Microsoft.AspNetCore.Mvc;
using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Supplier;
using Basilisk.ViewModel.Product;
using System.Globalization;
using Basilisk.ViewModel.Category;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Basilisk.Provider;
using Microsoft.AspNetCore.Authorization;

namespace Basilisk.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SupplierController : BaseController
    {


        [HttpGet]
        public IActionResult Index(int page=1, string searchName="")
        {
            SetUsernameRole(User.Claims);
            var suppliers = SupplierProvider.GetProvider().GetIndex(page, searchName);
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            SetUsernameRole(User.Claims);
            var model = SupplierProvider.GetProvider().GetDetail(id);
            //Single / SingleOrDefault digunakan agar data hasil query tidak berupa collection
            return View("DetailSupplier", model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            SetUsernameRole(User.Claims);
            var model = SupplierProvider.GetProvider().GetAddForm();
            return View("Upsert", model);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            SetUsernameRole(User.Claims);
            var model = SupplierProvider.GetProvider().GetUpdateData(id);
            return View("Upsert", model);
        }

        [HttpPost]
        public IActionResult Save(AddEditSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SupplierProvider.GetProvider().SaveData(model);

                }
                catch (Exception ex)
                {

                    throw;
                }

                return RedirectToAction("Index");
            }

            return View("Upsert", model);

        }


        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(long id)
        {

            if (SupplierProvider.GetProvider().SoftDeleteData(id))
            {
                return RedirectToAction("Index");
                
            }
            else
            {
                return View("FailDelete");
            }


        }


        #region Popup

        [HttpGet]
        public IActionResult DetailPopup(int id)
        {
            var model = SupplierProvider.GetProvider().GetDetail(id);
            
            return Json(model);
        }



        #endregion
    }
}
