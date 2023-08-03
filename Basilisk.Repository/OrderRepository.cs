using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class OrderRepository : BaseRepository, IRepository<Order>
    {

        private static OrderRepository _instance = new OrderRepository();
        public static OrderRepository GetRepository()
        {
            return _instance;
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Orders;
        }

        public Order GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var order = context.Orders.SingleOrDefault(a => a.InvoiceNumber == (string)id);
            return order;
        }

        public bool Insert(Order model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order model)
        {
            throw new NotImplementedException();
        }
    }
}
