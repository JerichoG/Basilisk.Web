using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;

namespace Basilisk.Repository
{
    public class ProductRepository : BaseRepository, IRepository<Product>
    {
        private static IRepository<Product> _instance = new ProductRepository();
        public static IRepository<Product> GetRepository()
        {
            return _instance;
        }

        public bool Delete(object id)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var product = context.Products.SingleOrDefault(a => a.Id == (long)id);
                    if (product == null)
                    {
                        return false;
                    }

                    var ordets = context.OrderDetails.Any(o=> o.ProductId == (long)id);
                    if (ordets == null)
                    {
                        return false;

                    }

                    var carts = context.Carts.Any(a => a.ProductId == (long)id);
                    if (carts)
                    {
                        return false;
                    }
                    else
                    {
                        context.Remove(product);
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

        public IQueryable<Product> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Products;
        }

        public Product GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var product = context.Products.SingleOrDefault(a => a.Id == (long)id);
            return product;
        }

        public bool Insert(Product model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Products.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Product model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var oldProd = context.Products.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(oldProd, model);

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
