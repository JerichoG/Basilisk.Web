using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.Provider
{
    public class CategoryProvider : BaseProvider
    {
        private static CategoryProvider _instance = new CategoryProvider();
        public static CategoryProvider GetProvider()
        {
            return _instance;
        }

        #region Index Category
        public IEnumerable<GridCategoryViewModel> GetDataIndex(string searchName)
        {
           
            var categories = CategoryRepository.GetRepository().GetAll();
            var grid = (from cat in categories
                        select new GridCategoryViewModel
                        {
                            Id = cat.Id,
                            Name = cat.Name,
                            Description = cat.Description
                        });

            

            if (!string.IsNullOrEmpty(searchName))
            {
                grid = grid.Where(a => a.Name.Contains(searchName));
            }

            return grid.ToList();
        }
        public IndexCategoryViewModel GetIndex(string searchName, int page)
        {

            var data = GetDataIndex(searchName);


            return (new IndexCategoryViewModel()
            {
                Grid = data.Skip(GetSkip(page)).Take(_totalDataPerPage).ToList(),
                SearchName = searchName,
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())

            });


        }

        #endregion

        #region Detail Category

        public DetailCategoryViewModel GetDetailIndex(long id)
        {


            var newIndex = new DetailCategoryViewModel()
            {
                CategoryName = CategoryRepository.GetRepository().GetSingle(id).Name,
                Description = CategoryRepository.GetRepository().GetSingle(id).Description,
                Products = GetDetail(id).ToList()
            };

            return newIndex;

        }


        public IEnumerable<GridProductViewModel> GetDetail(long id)
        {
            var products = (from a in ProductRepository.GetRepository().GetAll()
                            where a.CategoryId == id
                            select new GridProductViewModel
                            {
                                Id = a.Id,
                                ProductName = a.Name,
                                Description = a.Description,
                                Stock = a.Stock,
                                OnOrder = a.OnOrder,
                                Price = GetIndoFormat(a.Price),
                                Discontinue = a.Discontinue ? "Discontinue" : "Continue",
                                CategoryName = a.Category.Name,
                                SupplierName = a.Supplier.CompanyName
                            }).AsEnumerable();

            

            return products;

        }
        #endregion


        #region Save Category

        public CreateUpdateViewModel GetAddForm()
        {
            var model = new CreateUpdateViewModel();
            return model;
        }

        public CreateUpdateViewModel GetUpdateData(long id)
        {
            

            var model = new CreateUpdateViewModel();
            var oldCategory = CategoryRepository.GetRepository().GetSingle(id);
            MappingModel<CreateUpdateViewModel, Category>(model, oldCategory);

            return model;
        }

        public void SaveData(CreateUpdateViewModel model)
        {
            
            if (model.Id == 0)
            {
                    var data = new Category();
                    
                    MappingModel<Category, CreateUpdateViewModel>(data, model);
                    

                    CategoryRepository.GetRepository().Insert(data);

            }
            else
            {
                    var oldCat = CategoryRepository.GetRepository().GetSingle(model.Id);
                    
                    MappingModel<Category, CreateUpdateViewModel>(oldCat, model);

                    CategoryRepository.GetRepository().Update(oldCat);
                
                   

            }
            
            
        }

        public bool UpdateDescription(long id, string desc)
        {
            try
            {
                return CategoryRepository.GetRepository().UpdateDescription(id, desc);
            }
            catch
            {
                return false;
            }
            
        }

        #endregion

        #region Delete Category

        
        public bool Delete(long idCat)
        {
          
            return CategoryRepository.GetRepository().Delete(idCat);

        }

        #endregion

        #region API
        
        public JSONResultViewModel GetById(long id)
        {
            var check = CategoryRepository.GetRepository().GetSingle(id);
            if (check != null)
            {
                var oldCategory = CategoryRepository.GetRepository().GetSingle(id);
                var model = new CreateUpdateViewModel();
                MappingModel<CreateUpdateViewModel, Category>(model, oldCategory);
               
                var result = new JSONResultViewModel
                {
                    Success = true,
                    Message = "Data found",
                    ReturnObject = model
                };
                return result;
            }
            else
            {
                var result = new JSONResultViewModel
                {
                    Success = false,
                    Message = "Data not Found",
                    ReturnObject = check
                };
                return result;
            }
        }


        #endregion
    }
}
