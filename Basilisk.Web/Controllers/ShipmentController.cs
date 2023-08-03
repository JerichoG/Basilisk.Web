using Basilisk.Provider;
using Basilisk.ViewModel.Shipment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Basilisk.Web.Controllers
{
    public class ShipmentController : Controller
    {
        [HttpGet]
        public IActionResult Index(int page=1, string searchShip="")
        {
            var model = ShipmentProvider.GetProvider().GetIndex(page, searchShip);

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(long idShip)
        {
            var model = ShipmentProvider.GetProvider().GetIndexDetail(idShip);

            return View("DetailShipment", model);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult Send(string invNum, long idShip)
        {
            ShipmentProvider.GetProvider().ShippedData(invNum);
            return RedirectToAction("Detail", "Shipment", new { idShip = idShip});
        }


        [HttpGet]
        public IActionResult Add() 
        {
            var model = ShipmentProvider.GetProvider().GetAddForm();
            return View("Upsert", model);
        }

        [HttpGet]
        public IActionResult Edit(long idShip)
        {
            var model = ShipmentProvider.GetProvider().GetUpdateData(idShip);
            return View("Upsert", model);
        }

        [HttpPost]
        public IActionResult Save(UpsertShipmentViewModel model) 
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ShipmentProvider.GetProvider().SaveData(model);
                }
                catch
                {
                    throw;
                }

                return RedirectToAction("Index");
            }
            return View("Upsert", model);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(long idShip)
        {
            if (ShipmentProvider.GetProvider().DeleteData(idShip))
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
