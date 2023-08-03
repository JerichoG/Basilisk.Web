using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.Region;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Provider
{
    public class RegionProvider : BaseProvider
    {
        private static RegionProvider _instance = new RegionProvider();
        public static RegionProvider GetProvider()
        {
            return _instance;
        }

        #region Index Region
        public static IEnumerable<RegionViewModel> GetDataIndex(string searchCity)
        {

            var regions = (from a in RegionRepository.GetRepository().GetAll().AsEnumerable()
                           select new RegionViewModel
                           {
                               Id = a.Id,
                               City = a.City,
                               Remark = a.Remark
                           });

            if (!string.IsNullOrEmpty(searchCity))
                {
                    regions = regions.Where(c => c.City == searchCity);
                }

                return regions.ToList();
            
        }

        public IndexRegionViewModel GetIndex(int page, string searchCity)
        {
            var data = GetDataIndex(searchCity);

            
            var model = new IndexRegionViewModel
            {
                SearchCity = searchCity,
                ListRegion = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion

        #region Add - Edit

        public UpsertRegionViewModel GetAddForm()
        {
            var model = new UpsertRegionViewModel();
            return model;
        }

        public UpsertRegionViewModel GetEditData(long idReg)
        {

            var oldRegion = RegionRepository.GetRepository().GetSingle(idReg);
            var model = new UpsertRegionViewModel();
            MappingModel(model, oldRegion);

            return model;


        }

        public void SaveData(UpsertRegionViewModel model)
        {
            //using(var context = new BasiliskTFContext())
            //{
            //    if (model.Id == 0)
            //    {
            //        var entity = new Region
            //        {
            //            City = model.City,
            //            Remark = model.Remark
            //        };
            //        context.Regions.Add(entity);
            //        context.SaveChanges();
            //    }
            //    else
            //    {
            //        var oldRegion = context.Regions.SingleOrDefault(a => a.Id == model.Id);
            //        oldRegion.City = model.City;
            //        oldRegion.Remark = model.Remark;
            //        context.SaveChanges();
            //    }
            //}

            if(model.Id == 0)
            {
                var data = new Region();

                MappingModel<Region, UpsertRegionViewModel>(data, model);


                RegionRepository.GetRepository().Insert(data);
            }else
            {
                var oldReg = RegionRepository.GetRepository().GetSingle(model.Id);

                MappingModel<Region, UpsertRegionViewModel>(oldReg, model);

                RegionRepository.GetRepository().Update(oldReg);
            }
        }

        #endregion

        #region Delete

        //public bool CheckData(long idReg)
        //{
        //    using (var context = new BasiliskTFContext())
        //    {
        //        var checkReg = context.SalesmenRegions.Any(d => d.RegionId == idReg);
        //        return checkReg;
        //    }
        //}

        public bool DeleteData(long idReg)
        {
            
            return RegionRepository.GetRepository().Delete(idReg);

        }

        #endregion

    }

}
