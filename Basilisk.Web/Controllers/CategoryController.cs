using Microsoft.AspNetCore.Mvc;
using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Category;
using System.Security.Cryptography;
using Basilisk.ViewModel.Product;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Basilisk.Provider;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Basilisk.ViewModel;
using Microsoft.AspNetCore.Components.Forms;

namespace Basilisk.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : BaseController
    {

        

        [HttpGet]
        public IActionResult Index(int page=1, string searchName = "")
        {
            SetUsernameRole(User.Claims);
            var model = CategoryProvider.GetProvider().GetIndex( searchName, page);

            return View(model);
            
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            SetUsernameRole(User.Claims);
            var detailModel = CategoryProvider.GetProvider().GetDetailIndex(id);

            return View("DetailProduct", detailModel);

        }

        [HttpGet]
        public IActionResult DetailPopup(long id)
        {
            SetUsernameRole(User.Claims);
            var detailModel = CategoryProvider.GetProvider().GetDetailIndex(id);

            return Json(detailModel);

        }

        [HttpGet]
        public IActionResult Add()
        {
            SetUsernameRole(User.Claims);
            var model = CategoryProvider.GetProvider().GetAddForm();
          
            return View("Upsert", model);
        }

        [HttpGet]
        public IActionResult AddModal()
        {
            SetUsernameRole(User.Claims);
            var model = CategoryProvider.GetProvider().GetAddForm();

            return View("Upsert", model);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            SetUsernameRole(User.Claims);
            var model = CategoryProvider.GetProvider().GetUpdateData(id);
            return View("Upsert", model);
        }

        [HttpGet]
        public IActionResult EditPopup(long id)
        {
            
            var model = CategoryProvider.GetProvider().GetUpdateData(id);
            return Json(model);
        }

        //        [ValidateAntiForgeryToken]
        //        [HttpPost]
        //        public IActionResult Save([FromBody]CreateUpdateViewModel model)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    CategoryProvider.GetProvider().SaveData(model);
        //                }
        //                catch
        //                {

        //                    throw;
        //                }

        //                return RedirectToAction("Index");
        //            }
        //;           
        //            return View("Upsert", model);

        //        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SavePopup([FromBody] CreateUpdateViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    CategoryProvider.GetProvider().SaveData(model);
                    return Json(new {success = true, message = "Insert Berhasil" });
                }
                catch
                {
                   
                    return Json(new { success = false, message = "Insert gagal" });
                }

                
            }

            var validationMessage = GetValidationMessage(ModelState);
            return Json(new{ success = false, message = "Insert gagal", validations = validationMessage });

        }


        //tag a lempar data dalam bentuk satuan atau parameter
        //tag button submit lempar data dalam bentuk model

        //tag a hanya bisa dalam bentuk HttpGet

        //Agar bisa menerima method Post atau Get
        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(long id)
        {
            //sebelum melakukan delete, cek dulu tabelnya terhubung ke tabel lain atau tidak
            //cek product by category id 

            var deleteCat = CategoryProvider.GetProvider().Delete(id);

            if (deleteCat)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("FailDelete");

            }

            //ada 2 tipe konsep delete, yaitu soft delete dan hard delete
            //hard delete benar2 terhapus dan tidak bisa recovery
            //soft delete, data seakan2 terhapus tetapi bisa direcovery
            
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult DeleteModal(long id)
        {
           var response = CategoryProvider.GetProvider().Delete(id);

            return Json(response);

            
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult DeleteTest(long id)
        {
            var response = CategoryProvider.GetProvider().Delete(id);

            return Json(response);

        }

        [HttpGet]
        public IActionResult GetRoles() 
        {
            var model = ProductProvider.GetProvider().GetSupplierCustom();
            return Json(model);
        }

    }
}
