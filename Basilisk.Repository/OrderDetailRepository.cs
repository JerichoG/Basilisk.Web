using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class OrderDetailRepository : BaseRepository, IRepository<OrderDetail>
    {
        private static OrderDetailRepository _instance = new OrderDetailRepository();
        public static OrderDetailRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OrderDetail> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.OrderDetails;
        }

        public OrderDetail GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var ordet = context.OrderDetails.SingleOrDefault(a => a.InvoiceNumber == (string)id);
            return ordet;
        }

        public bool Insert(OrderDetail model)
        {
           throw new NotImplementedException ();
        }

        public bool Update(OrderDetail model)
        {
            throw new NotImplementedException();
        }
    }
}
