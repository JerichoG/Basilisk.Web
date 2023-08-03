using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;

namespace Basilisk.Repository
{
    public class SupplierRepository : BaseRepository, IRepository<Supplier>
    {
        private static IRepository<Supplier> _instance = new SupplierRepository();
        public static IRepository<Supplier> GetRepository()
        {
            return _instance;
        }

        public bool Delete(object id)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var supplier = context.Suppliers.SingleOrDefault(a => a.Id == (long)id);
                    if (supplier == null)
                    {
                        return false;
                    }

                    var products = context.Products.Any(a => a.SupplierId == (long)id);
                    if (products)
                    {
                        return false;
                    }
                    else
                    {
                        supplier.DeleteDate = DateTime.Now;
                       
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

        public IQueryable<Supplier> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Suppliers;
        }

        public Supplier GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var supplier = context.Suppliers.SingleOrDefault(a => a.Id == (long)id);
            return supplier;
        }

        public bool Insert(Supplier model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Suppliers.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Supplier model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var oldSup = context.Suppliers.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(oldSup, model);

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
