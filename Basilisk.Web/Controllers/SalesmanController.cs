using Basilisk.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Basilisk.ViewModel.Salesman;
using Microsoft.AspNetCore.Mvc.Rendering;
using Basilisk.ViewModel.Product;
using System.Net;
using Basilisk.ViewModel.Region;
using Basilisk.Provider;

namespace Basilisk.Web.Controllers
{

    public class SalesmanController : BaseController
    {
       
        [HttpGet]
        public IActionResult Index(int page=1, string searchID="")
        {
            SetUsernameRole(User.Claims);
            var salesman = SalesmanProvider.GetProvider().GetIndex(page, searchID);

            return View(salesman);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = SalesmanProvider.GetProvider().GetAddForm();
            
            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(string id) //nama parameter harus id karena itu dari view
        {

            var model = SalesmanProvider.GetProvider().GetUpdateData(id);
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Add(AddEditSalesmanViewModel model) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SalesmanProvider.GetProvider().AddData(model);
                }
                catch
                {
                    return View("Add", model);
                }
                return RedirectToAction("Index");

            }

            model.DropdownSuperiorEmp = SalesmanProvider.GetProvider().GetSales();
            return View("Add", model);
        }

        [HttpPost]
        public IActionResult Edit(AddEditSalesmanViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SalesmanProvider.GetProvider().EditData(model);
                }
                catch
                {
                    return View("Edit", model);
                }
                return RedirectToAction("Index");

            }

            model.DropdownSuperiorEmp = SalesmanProvider.GetProvider().GetSales();
            return View("Edit", model);
        }

        
        [HttpGet]
        public IActionResult Details(string id)
        {

            var model = SalesmanProvider.GetProvider().GetIndexDetail(id);

            return View("DetailSalesman", model);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(string id)
        {
            if (SalesmanProvider.GetProvider().DeleteData(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("FailDelete");
            }
            
        }
        
    }
}
