using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class SalesmanRepository : BaseRepository, IRepository<Salesman>
    {

        private static SalesmanRepository _instance = new SalesmanRepository();
        public static SalesmanRepository GetRepository()
        {
            return _instance;
        }

        public bool Delete(object empNum)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var emp = context.Salesmen.SingleOrDefault(a => a.EmployeeNumber == (string)empNum);
                    if (emp == null)
                    {
                        return false;
                    }

                    var superiors = context.Salesmen.Any(a => a.SuperiorEmployeeNumber == (string)empNum);
                    if (superiors)
                    {
                        return false;
                    }

                    var orders = context.Orders.Any(o=> o.SalesEmployeeNumber == (string)empNum);
                    if (orders)
                    {
                        return false;
                    }

                    var salesmenRegions = context.SalesmenRegions.Any(s=> s.SalesmanEmployeeNumber == (string)empNum);  
                    if(salesmenRegions)
                    {
                        return false;
                    }
                    else
                    {
                        context.Remove(emp);
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

        public IQueryable<Salesman> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Salesmen;
        }

        public Salesman GetSingle(object empNum)
        {
            var context = new BasiliskTFContext();

            var salesman = context.Salesmen.SingleOrDefault(a => a.EmployeeNumber == (string)empNum);
            return salesman;
        }

        public bool Insert(Salesman model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Salesmen.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Salesman model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    var oldEmp = context.Salesmen.SingleOrDefault(a => a.EmployeeNumber == model.EmployeeNumber);
                    MappingModel(oldEmp, model);

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
