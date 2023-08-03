using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class CartRepository : BaseRepository, IRepository<Cart>
    {
        private static CartRepository _instance = new CartRepository();
        public static CartRepository GetRepository()
        {
            return _instance;
        }

        public bool Delete(object id)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var cart = context.Carts.SingleOrDefault(a => a.Id == (long)id);
                    if (cart == null)
                    {
                        return false;
                    }
                    else
                    {
                        context.Remove(cart);
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

        public IQueryable<Cart> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Carts;
        }

        public Cart GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var cart = context.Carts.SingleOrDefault(a => a.Id == (long)id);
            return cart;
        }

        public bool Insert(Cart model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Carts.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Cart model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var oldCart = context.Carts.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(oldCart, model);

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
