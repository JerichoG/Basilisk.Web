using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class ShipmentRepository : BaseRepository, IRepository<Delivery>
    {
        private static ShipmentRepository _instance = new ShipmentRepository();
        public static ShipmentRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var ship = context.Deliveries.SingleOrDefault(a => a.Id == (long)id);
                    if (ship == null)
                    {
                        return false;
                    }

                    var orders = context.Orders.Any(o => o.DeliveryId == (long)id);
                    if (orders == null)
                    {
                        return false;

                    }
                    else
                    {
                        context.Remove(ship);
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

        public IQueryable<Delivery> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Deliveries;
        }

        public Delivery GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var ship = context.Deliveries.SingleOrDefault(a => a.Id == (long)id);
            return ship;
        }

        public bool Insert(Delivery model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Deliveries.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Delivery model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var oldShip = context.Deliveries.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(oldShip, model);

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
