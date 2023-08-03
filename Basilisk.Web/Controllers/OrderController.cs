using Basilisk.DataAccess.Models;
using Basilisk.Provider;
using Basilisk.ViewModel.Order;
using Basilisk.ViewModel.Region;
using Basilisk.ViewModel.Salesman;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Basilisk.Web.Controllers
{
    public class OrderController : BaseController
    {
        
        #region Index

        [HttpGet]
        public IActionResult Index(int page=1, string searchInv="", string searchCus="")
        {
            SetUsernameRole(User.Claims);
            var check = GetRole(User.Claims);

            if (check == "Customer")
            {
                var username = Convert.ToInt64(GetUsername(User.Claims));
                var model = CustomerProvider.GetProvider().GetIndexOrder(page, searchInv, username);
                

                return View(model);

            }
            else
            {
                var orders = OrderProvider.GetProvider().GetIndex(page, searchInv, searchCus);

                return View(orders);
            }

        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Detail(string id)
        {
            SetUsernameRole(User.Claims);
            var model = OrderProvider.GetProvider().GetIndexDetail(id);
            return View("DetailOrder", model);
        }

        #endregion

        #region Update DueDate

        [AcceptVerbs("POST", "GET")]
        public IActionResult Accept(string id)
        {
            OrderProvider.GetProvider().AccDelivery(id);

            return RedirectToAction("Detail", "Order", new { id = id });

        }

        #endregion
    }
}
