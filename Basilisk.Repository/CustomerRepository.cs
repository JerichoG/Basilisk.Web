using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class CustomerRepository : BaseRepository, IRepository<Customer>
    {
        private static CustomerRepository _instance = new CustomerRepository();
        public static CustomerRepository GetRepository()
        {
            return _instance;
        }
        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Customers;
        }

        public Customer GetSingle(object id)
        {
            var context = new BasiliskTFContext();

            var customer = context.Customers.SingleOrDefault(a => a.Id == (long)id);
            return customer;
        }

        public bool Insert(Customer model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
