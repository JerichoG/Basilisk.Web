using Basilisk.Provider;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {

        [HttpGet]
        public IEnumerable<GridCategoryViewModel> Get()
        {
            var result = CategoryProvider.GetProvider().GetDataIndex("");
            return result;
        }

        [HttpGet]
        [Route("GetCategory")] // ip/{controller/{name}/{param} di API dibaca route-nya
        public IEnumerable<GridCategoryViewModel> Get(string? name)
        {
            var result = CategoryProvider.GetProvider().GetDataIndex(name);
            return result;
        }

        [HttpGet]
        [Route("Edit/{id}")] // ip/{controller/{name}/{param} di API dibaca route-nya
        public JSONResultViewModel Get(long id)
        {
            var result = CategoryProvider.GetProvider().GetById(id);
            return result;
        }

        [HttpPost]
        public IActionResult Insert(CreateUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    CategoryProvider.GetProvider().SaveData(model);
                    return Ok("Berhasil Tambah Data");
                }
                catch(Exception)
                {
                    return Ok("Gagal Tambah Data");
                }

            }
            return Ok("Gagal Insert Data");
        }

        [HttpPut]
        public string Edit(CreateUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CategoryProvider.GetProvider().SaveData(model);
                    return "Berhasil Edit Data";
                }
                catch (Exception)
                {
                    return "Gagal Edit Data";
                }

            }
            return "Gagal Edit Data";
        }

        [HttpPatch("{id}")]
        public string Patch(long id, [FromBody] string desc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if( CategoryProvider.GetProvider().UpdateDescription(id, desc))
                    {
                        return "Patch Berhasil";
                    }
                    return "Patch Gagal";
                }
                catch
                {
                    return "Patch Gagal";
                }
            }
            return "Patch Gagal";
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public string Delete(long id) 
        {
            try
            {
                CategoryProvider.GetProvider().Delete(id);
                return "Berhasil Delete Data";
            }
            catch (Exception)
            {
                return "Gagal Delete Data";
            }

        } 


    }
}
