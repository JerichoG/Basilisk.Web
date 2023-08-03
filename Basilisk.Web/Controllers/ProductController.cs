using Microsoft.AspNetCore.Mvc;
using Basilisk.ViewModel.Product;
using Basilisk.Provider;

namespace Basilisk.Web.Controllers
{
    public class ProductController : BaseController
    {
      

        [HttpGet]
        public IActionResult Index(int page = 1, string prodName = "", string supName = "", string catName = "")
        {
            SetUsernameRole(User.Claims);
            var products = ProductProvider.GetProvider().GetIndex(page, prodName, supName, catName);
            return View(products);
        }


        [HttpGet]
        public IActionResult Add()
        {
            SetUsernameRole(User.Claims);
            var model = ProductProvider.GetProvider().GetAddForm();
            return View("AddEdit", model);
        }


        [HttpGet]
        public IActionResult Edit(long id)
        {
            SetUsernameRole(User.Claims);

            var model = ProductProvider.GetProvider().GetUpdateData(id);
            return View("AddEdit", model);
        }


        [HttpPost]
        public IActionResult Save(UpsertViewModel viewModel)
        {
            try
            {
                ProductProvider.GetProvider().SaveData(viewModel);
            }

            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Index");
        }


        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(long id)
        {
            if (ProductProvider.GetProvider().DeleteData(id))
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
