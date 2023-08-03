using Basilisk.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Basilisk.ViewModel.Customer;
using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using System.Data.Common;
using Basilisk.Provider;

namespace Basilisk.Web.Controllers
{
    public class CustomerController : BaseController
    {
        private static BasiliskTFContext _context = new BasiliskTFContext();
        private const int TotalDataPerPage = 5;


        //di .Net bisa membuat object tanpa class 
        //tp hanya sebagai untuk membantu query 
        [HttpGet]
        public IActionResult Index(int page = 1, string searchCust="")
        {
            SetUsernameRole(User.Claims);
            var customers = CustomerProvider.GetProvider().GetIndex(page, searchCust);
                
            return View(customers);
        }

        [HttpGet]
        public IActionResult DetailCart(long id)
        {
            SetUsernameRole(User.Claims);
            var check = GetRole(User.Claims);

            if(check == "Customer")
            {
                var username = Convert.ToInt64(GetUsername(User.Claims));
                var model = CustomerProvider.GetProvider().GetIndexDetailCart(username);
                ViewBag.CustomerId = username;

                return View("DetailCart", model);

            }
            else
            {
                var model = CustomerProvider.GetProvider().GetIndexDetailCart(id);
                return View("DetailCart", model);
            }

        }

        

        [AcceptVerbs("POST", "GET")]
        public IActionResult MinQuantity(long idProd, long idCus)
        {
            CustomerProvider.GetProvider().MinusQty(idProd, idCus);
            return RedirectToAction("DetailCart", "Customer", new { id = idCus });
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult PlusQuantity(long idProd, long idCus)
        {
            CustomerProvider.GetProvider().PlusQty(idProd, idCus);

            return RedirectToAction("DetailCart", "Customer", new { id = idCus });

        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult DeleteProduct(long idProd, long idCus)
        {
            CustomerProvider.GetProvider().DeleteProduct(idProd, idCus);
            return RedirectToAction("DetailCart", "Customer", new { id = idCus });
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult CheckHandler(long idCus, long idProd, bool check)
        {
           CustomerProvider.GetProvider().CheckProduct(idProd, idCus, check);

            return RedirectToAction("DetailCart", new { id = idCus });
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult CheckHandlerAll(long idCus, long idSeller, bool checkedAll)
        {
           CustomerProvider.GetProvider().CheckSupplier(idCus, idSeller, checkedAll);
            return RedirectToAction("DetailCart", new { id = idCus });
        }

        [HttpGet]
        public IActionResult AddProduct(long idCus)
        {
            SetUsernameRole(User.Claims);
            var catalog = CustomerProvider.GetProvider().GetCatalogProduct(idCus);

            ViewBag.CustomerId = idCus;
            return View("CatalogProduct", catalog);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult AddtoCart(long idCus, long idProd)
        {
            SetUsernameRole(User.Claims);
            CustomerProvider.GetProvider().AddtoCart(idCus, idProd);
            return RedirectToAction("AddProduct", "Customer", new { idCus = idCus });
        }

        [HttpPost]
        public IActionResult Checkout(CartViewModel model)
        {
            if (ModelState.IsValid && model.CartDetails.Any(c => c.Products.Any(p => p.Checked)) 
                && CustomerProvider.GetProvider().Checkout(model))
            { 
                return RedirectToAction("Index");
            }
            else
            {
                return View("DetailCart", model);
            }

        }

        
    }
}
