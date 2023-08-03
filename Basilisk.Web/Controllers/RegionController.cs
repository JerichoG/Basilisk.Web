using Basilisk.DataAccess.Models;
using Basilisk.Provider;
using Basilisk.ViewModel.Region;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Basilisk.Web.Controllers
{

    public class RegionController : BaseController
    {
        #region Index

        [HttpGet]
        public IActionResult Index(int page=1, string searchCity="")
        {
            SetUsernameRole(User.Claims);
            var model = RegionProvider.GetProvider().GetIndex( page, searchCity);

            return View(model);

        }

        #endregion

        #region Add - Edit
        [HttpGet]
        public IActionResult Add()
        {
            var model = RegionProvider.GetProvider().GetAddForm();
            return View("Upsert", model);
        }

        [HttpGet]
        public IActionResult Edit(long idRegion)
        {
            var model = RegionProvider.GetProvider().GetEditData(idRegion);
            return View("Upsert", model);

        }

        [HttpPost]
        public IActionResult Save(UpsertRegionViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {

                    RegionProvider.GetProvider().SaveData(model);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction("Index");

            }
            else
            {
                return View("Upsert",model);
            }
            
        }

        #endregion


        #region Delete

        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(long idRegion)
        {
            if (RegionProvider.GetProvider().DeleteData(idRegion))
            {
                return RedirectToAction("Index");
                
            }
            else
            {
                return View("FailDelete");
            }
          ;
        }

        #endregion
    }
}
