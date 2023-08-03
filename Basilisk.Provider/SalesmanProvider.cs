using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.Region;
using Basilisk.ViewModel.Salesman;
using Basilisk.ViewModel.Shipment;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Provider
{
    public class SalesmanProvider : BaseProvider
    {
        private static SalesmanProvider _instance = new SalesmanProvider();
        public static SalesmanProvider GetProvider()
        {
            return _instance;
        }

        #region Index Salesman
        public IEnumerable<GridSalesmanViewModel> GetDataIndex(string searchID)
        {

            var salesmen = (from sales in SalesmanRepository.GetRepository().GetAll().AsEnumerable()
                            join s in SalesmanRepository.GetRepository().GetAll().AsEnumerable() 
                            on sales.SuperiorEmployeeNumber equals s.EmployeeNumber into newSales
                            from subsales in newSales.DefaultIfEmpty()
                            select new GridSalesmanViewModel
                            {
                                EmployeeNumber = sales.EmployeeNumber,
                                FirstName = sales.FirstName,
                                LastName = sales.LastName,
                                Level = sales.Level,
                                BirthDate = sales.BirthDate.ToString("dd MMMM yyyy"),
                                HireDate = sales.HiredDate.ToString("dd MMMM yyyy"),
                                Address = sales.Address,
                                City = sales.City,
                                Phone = string.IsNullOrEmpty(sales.Phone) ? "N/A" : sales.Phone,
                                SuperiorEmployee = string.IsNullOrEmpty(sales.SuperiorEmployeeNumber) ? "N/A" : subsales.FirstName + " " + subsales.LastName
                            });

            if (!string.IsNullOrEmpty(searchID))
            {
                salesmen = salesmen.Where(a => a.EmployeeNumber.Contains(searchID));
                
            }

            return salesmen.ToList();
        }

        public IndexSalesmanViewModel GetIndex(int page, string searchID)
        {
            var data = GetDataIndex(searchID);


            var model = new IndexSalesmanViewModel
            {
                SearchID = searchID,
                Salesmen = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion

        #region Add - Edit Salesman

        public List<SelectListItem> GetSales()
        {

            var result = SalesmanRepository.GetRepository().GetAll().Select(
                s => new SelectListItem
                {
                    Value = s.EmployeeNumber,
                    Text = s.FirstName + " " + s.LastName
                }).ToList();


            return result;
        }

        public AddEditSalesmanViewModel GetAddForm()
        {
            var model = new AddEditSalesmanViewModel();
            model.DropdownSuperiorEmp = GetSales();

            return model;
        }

        public AddEditSalesmanViewModel GetUpdateData(string empNum)
        {

            var model = new AddEditSalesmanViewModel();
            var oldEmp = SalesmanRepository.GetRepository().GetSingle(empNum);

            MappingModel<AddEditSalesmanViewModel, Salesman>(model, oldEmp);

            model.DropdownSuperiorEmp = GetSales();

            return model;

        }

        public void AddData(AddEditSalesmanViewModel model)
        {
            var data = new Salesman();

            MappingModel<Salesman, AddEditSalesmanViewModel>(data, model);

            SalesmanRepository.GetRepository().Insert(data);
        }

        public void EditData(AddEditSalesmanViewModel model)
        {
            var oldEmp = SalesmanRepository.GetRepository().GetSingle(model.EmployeeNumber);

            MappingModel<Salesman, AddEditSalesmanViewModel>(oldEmp, model);

            SalesmanRepository.GetRepository().Update(oldEmp);
        }

        //Add dan Edit harus dipisah
        
        #endregion

        #region Details Salesman

        public DetailsSalesmanViewModel GetIndexDetail(string empNum)
        {
            
            var salesmanRegion = (from sales in SalesmanRepository.GetRepository().GetAll().AsEnumerable()
                                  join s in SalesmanRepository.GetRepository().GetAll().AsEnumerable()
                                  on sales.SuperiorEmployeeNumber equals s.EmployeeNumber
                                  into newSales
                                  from subsales in newSales.DefaultIfEmpty()
                                  where sales.EmployeeNumber == empNum
                                  select new DetailsSalesmanViewModel
                                  {
                                      EmployeeNumber = sales.EmployeeNumber,
                                      FirstName = sales.FirstName,
                                      LastName = sales.LastName,
                                      Level = sales.Level,
                                      BirthDate = sales.BirthDate.ToString("dd MMMM yyyy"),
                                      HireDate = sales.HiredDate.ToString("dd MMMM yyyy"),
                                      Address = sales.Address,
                                      City = sales.City,
                                      Phone = string.IsNullOrEmpty(sales.Phone) ? "N/A" : sales.Phone,
                                      SuperiorEmployee = string.IsNullOrEmpty(sales.SuperiorEmployeeNumber) ? "N/A" : subsales.FirstName + " " + subsales.LastName,
                                      GridRegion = GetDataDetail(empNum).ToList()
                                  }).SingleOrDefault();
            return salesmanRegion;
        }

        public IEnumerable<RegionViewModel> GetDataDetail(string empNum)
        {
            var regionSales = (from s in SalesmanRepository.GetRepository().GetAll().AsEnumerable()
                               join sr in SalesmenRegionRepository.GetRepository().GetAll().AsEnumerable() 
                               on s.EmployeeNumber equals sr.SalesmanEmployeeNumber
                               join r in RegionRepository.GetRepository().GetAll().AsEnumerable()
                               on sr.RegionId equals r.Id
                               where s.EmployeeNumber == empNum
                               select new RegionViewModel
                               {
                                   Id = r.Id,
                                   City = r.City,
                                   Remark = r.Remark

                               });

            return regionSales;

        }

        #endregion

        #region Delete Salesman

        
        public bool DeleteData(string empNum)
        {

            return SalesmanRepository.GetRepository().Delete(empNum);


        }

        #endregion

    }
}
