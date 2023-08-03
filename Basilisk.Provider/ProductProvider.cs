using Basilisk.DataAccess.Models;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Product;
using Basilisk.Repository;
using Basilisk.ViewModel.Shipment;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basilisk.ViewModel.Category;

namespace Basilisk.Provider
{
    public class ProductProvider : BaseProvider
    {
        private static ProductProvider _instance = new ProductProvider();
        public static ProductProvider GetProvider()
        {
            return _instance;
        }

        #region Index Product
        public IEnumerable<GridProductViewModel> GetDataIndex(string prodName, string supName, string catName)
        {

            var prods = ProductRepository.GetRepository().GetAll().AsEnumerable();
            var sups = SupplierRepository.GetRepository().GetAll().AsEnumerable();
            var cats = CategoryRepository.GetRepository().GetAll().AsEnumerable();
            
            var products = prods.Join(sups,
                prods => prods.SupplierId,
                sups => sups.Id,
                (prods, sups) => new
                {
                    prods, sups
                }).ToList().Join(cats,
                prodSups => prodSups.prods.CategoryId,
                cats => cats.Id,
                (prodSups, cats) => new GridProductViewModel
                {
                    Id = prodSups.prods.Id,
                    ProductName = prodSups.prods.Name,
                    SupplierName = prodSups.sups.CompanyName,
                    CategoryName = cats.Name,
                    Description = prodSups.prods.Description,
                    Price = GetIndoFormat(prodSups.prods.Price),
                    Stock = prodSups.prods.Stock,
                    OnOrder = prodSups.prods.OnOrder,
                    Discontinue = prodSups.prods.Discontinue ? "Discontinue" : "Continue"
                });

            if (!string.IsNullOrEmpty(prodName))
            {
                products = products.Where(p => p.ProductName.Contains(prodName));
            }
            if (!string.IsNullOrEmpty(supName))
            {
                products = products.Where(p => p.SupplierName.Contains(supName));
            }
            if (!string.IsNullOrEmpty(catName))
            {
                products = products.Where(p => p.CategoryName.Contains(catName));
            }

            return products.ToList();

        }

        public IndexProductViewModel GetIndex(int page, string prodName, string supName, string catName)
        {
            var data = GetDataIndex(prodName, supName, catName);


            var model = new IndexProductViewModel
            {
                Grid = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                ProdName = prodName,
                SupName = supName,
                CatName = catName,
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion


        #region Dropdown Supplier, Category


        private List<SelectListItem> GetCategory()
        {

            var result = CategoryRepository.GetRepository().GetAll().Select(
                c=> new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();


            return result;

        }

        public List<DropdownListViewModel> GetSupplierCustom()
        {
            
            var result = SupplierRepository.GetRepository().GetAll().Select(
                a => new DropdownListViewModel
                {
                    LongValue = a.Id,
                    StringValue = a.CompanyName
                }).ToList();

            return result;
            
        }

        #endregion

        #region Add - Edit Product

        public UpsertViewModel GetAddForm()
        {
            var model = new UpsertViewModel()
            {
                DropdownCategory = GetCategory(),
                DropdownSupplierCustom = GetSupplierCustom()
            };

            return model;
        }

        public UpsertViewModel GetUpdateData(long id) 
        {
            
            var model = new UpsertViewModel();
            var oldProd = ProductRepository.GetRepository().GetSingle(id);
            MappingModel(model, oldProd);

            model.DropdownSupplierCustom = GetSupplierCustom();
            model.DropdownCategory = GetCategory();

            return model;
        }

        public void SaveData(UpsertViewModel model)
        {
            
            if(model.Id == 0)
            {
                var data = new Product();

                MappingModel<Product, UpsertViewModel>(data, model);


                ProductRepository.GetRepository().Insert(data);
            }
            else
            {
                var oldProd = ProductRepository.GetRepository().GetSingle(model.Id);

                MappingModel<Product, UpsertViewModel>(oldProd, model);

                ProductRepository.GetRepository().Update(oldProd);

            }

        }

        #endregion


        #region Delete Product

       

        public bool DeleteData(long id)
        {
            return ProductRepository.GetRepository().Delete(id);


        }

        #endregion
    }
}
