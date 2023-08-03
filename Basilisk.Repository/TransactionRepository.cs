using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Basilisk.Repository
{
    public class TransactionRepository : BaseRepository
    {
        private static TransactionRepository _instance = new TransactionRepository();
        public static TransactionRepository GetRepository()
        {
            return _instance;
        }

        
        public bool Checkout(Order order, List<OrderDetail> orderDetails, List<Product> products, List<Cart> carts)
        {
            using(var context = new BasiliskTFContext())
            {
                using(var dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Orders.Add(order);

                        foreach (var prod in products)
                        {
                            var oldProd = context.Products.SingleOrDefault(a => a.Id == prod.Id);
                            MappingModel(oldProd, prod);
                        }
                       
                        foreach (var ordet in orderDetails)
                        {
                            context.OrderDetails.Add(ordet);
                        }
                        

                        foreach (var cart in carts)
                        {
                            var oldCart = context.Carts.SingleOrDefault(c => c.Id == cart.Id);
                            if (oldCart == null)
                            {
                                return false;
                            }
                            else
                            {
                                context.Remove(oldCart);
                            }
                        }
                        context.SaveChanges();

                        dbTran.Commit();

                        return true;
                        

                    }
                    catch
                    {
                        context.Database.RollbackTransaction();
                        return false;
                    }
                }
            }
            

        }



    }
}
