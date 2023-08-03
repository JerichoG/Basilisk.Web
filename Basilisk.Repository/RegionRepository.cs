using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;

namespace Basilisk.Repository
{
    public class RegionRepository : BaseRepository, IRepository<Region>
    {
        private static RegionRepository _instance = new RegionRepository();
        public static RegionRepository GetRepository()
        {
            return _instance;
        }

        public bool Delete(object id)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var region = context.Regions.SingleOrDefault(a => a.Id == (long)id);
                    if (region == null)
                    {
                        return false;
                    }

                    var salesmenRegions = context.SalesmenRegions.Any(o => o.RegionId == (long)id);
                    if (salesmenRegions == null)
                    {
                        return false;

                    }
                    else
                    {
                        context.Remove(region);
                        context.SaveChanges();
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public IQueryable<Region> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Regions;
        }

        public Region GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var region = context.Regions.SingleOrDefault(a => a.Id == (long)id);
            return region;
        }

        public bool Insert(Region model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Regions.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Region model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var oldReg = context.Regions.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(oldReg, model);

                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
