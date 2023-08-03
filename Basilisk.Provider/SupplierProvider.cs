using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.Product;
using Basilisk.ViewModel.Shipment;
using Basilisk.ViewModel.Supplier;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Provider
{
    public class SupplierProvider : BaseProvider
    {
        private static SupplierProvider _instance = new SupplierProvider();
        public static SupplierProvider GetProvider()
        {
            return _instance;
        }

        #region Index Supplier
        public IEnumerable<GridSupplierViewModel> GetDataIndex(string searchName)
        {

            var suppliers = (from sup in SupplierRepository.GetRepository().GetAll()
                             where sup.DeleteDate == null
                             select new GridSupplierViewModel
                             {
                                 Id = sup.Id,
                                 CompanyName = sup.CompanyName,
                                 ContactPerson = sup.ContactPerson,
                                 JobTitle = sup.JobTitle,
                                 Address = sup.Address,
                                 City = sup.City,
                                 Phone = sup.Phone,
                                 Email = sup.Email

                             });

            if (!string.IsNullOrEmpty(searchName))
            {
                
                suppliers = suppliers.Where(c => c.CompanyName.Contains(searchName));
                
            }


            return suppliers.ToList();
        }

        public IndexSupplierViewModel GetIndex(int page, string searchName)
        {
            var data = GetDataIndex(searchName);


            var model = new IndexSupplierViewModel
            {
                SearchName = searchName,
                Suppliers = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion

        #region Detail Supplier

        public DetailSupplierViewModel GetDetail(long id)
        {
            
            var data = SupplierRepository.GetRepository().GetAll().Where(s=> s.Id == id).SingleOrDefault();
            var supplier = new DetailSupplierViewModel();

            MappingModel(supplier, data);

            supplier.GridProd = GetDataDetail(id).ToList();

            return supplier;

        }

        public IEnumerable<GridProductViewModel> GetDataDetail(long id)
        {
            
            var suppliers = (from prod in ProductRepository.GetRepository().GetAll()
                             where prod.SupplierId == id
                             select new GridProductViewModel
                             {
                                 ProductName = prod.Name,
                                 CategoryName = prod.Category.Name,
                                 Description = prod.Description,
                                 Price = GetIndoFormat(prod.Price),
                                 Stock = prod.Stock,
                                 OnOrder = prod.OnOrder,
                                 Discontinue = prod.Discontinue ? "Discontinue" : "Continue"
                             }).AsEnumerable();

            return suppliers.ToList();
        }

        #endregion

        #region Add - Edit Supplier

        public AddEditSupplierViewModel GetAddForm()
        {
            var model = new AddEditSupplierViewModel();
            return model;
        }

        public AddEditSupplierViewModel GetUpdateData(long id)
        {
            
            var model = new AddEditSupplierViewModel();
            var oldSupplier = SupplierRepository.GetRepository().GetSingle(id);
            MappingModel(model, oldSupplier);

            return model;
        }

        public void SaveData(AddEditSupplierViewModel model)
        {
            //using (var context = new BasiliskTFContext())
            //{
            //    if (model.Id == 0)
            //    {
            //        var data = new Supplier();
            //        data.CompanyName = model.CompanyName;
            //        data.ContactPerson = model.ContactPerson;
            //        data.JobTitle = model.JobTitle;
            //        data.Address = model.Address;
            //        data.City = model.City;
            //        data.Phone = model.Phone;
            //        data.Email = string.IsNullOrEmpty(model.Email) ? "N/A" : model.Email;

            //        context.SaveChanges();
            //        context.Suppliers.Add(data);
            //        context.SaveChanges();
            //    }
            //    else
            //    {
            //        var oldSupplier = context.Suppliers.SingleOrDefault(s => s.Id == model.Id);
            //        oldSupplier.CompanyName = model.CompanyName;
            //        oldSupplier.ContactPerson = model.ContactPerson;
            //        oldSupplier.JobTitle = model.JobTitle;
            //        oldSupplier.Address = model.Address;
            //        oldSupplier.City = model.City;
            //        oldSupplier.Phone = model.Phone;
            //        oldSupplier.Email = string.IsNullOrEmpty(model.Email) ? "N/A" : model.Email;

            //        context.SaveChanges();

            //    }
            //}

            if (model.Id == 0)
            {
                var data = new Supplier();

                MappingModel<Supplier, AddEditSupplierViewModel>(data, model);

                SupplierRepository.GetRepository().Insert(data);
            }
            else
            {
                var oldSup = SupplierRepository.GetRepository().GetSingle(model.Id);

                MappingModel<Supplier, AddEditSupplierViewModel>(oldSup, model);

                SupplierRepository.GetRepository().Update(oldSup);
            }
        }

        #endregion

        #region Delete Supplier


        public bool SoftDeleteData(long id)
        {
            
            return SupplierRepository.GetRepository().Delete(id);

        }

        #endregion
    }
}
