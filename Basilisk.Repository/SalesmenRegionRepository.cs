using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class SalesmenRegionRepository : BaseRepository, IRepository<SalesmenRegion>
    {
        private static SalesmenRegionRepository _instance = new SalesmenRegionRepository();
        public static SalesmenRegionRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SalesmenRegion> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.SalesmenRegions;
        }

        public SalesmenRegion GetSingle(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SalesmenRegion model)
        {
            throw new NotImplementedException();
        }

        public bool Update(SalesmenRegion model)
        {
            throw new NotImplementedException();
        }
    }
}
