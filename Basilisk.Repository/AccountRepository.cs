using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;


namespace Basilisk.Repository
{
    public class AccountRepository : BaseRepository
    {
        private static AccountRepository instance = new AccountRepository();
        public static AccountRepository GetRepository()
        {
            return instance;
        }

        public string GetRole(string username)
        {
            using(var context = new BasiliskTFContext())
            {
                return context.Accounts.SingleOrDefault(a => a.Username == username).Role;
            }
        }

        public bool GetIsAuthentication(string username, string password)
        {
            using(var context = new BasiliskTFContext())
            {


                var accountByUsername = context.Accounts.SingleOrDefault(a => a.Username == username && a.Password == password);
                if(accountByUsername != null)
                {
                    return true;
                }
            }
            return false;   
        }

        public IQueryable<Account> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.Accounts;
        }

        public Account GetSingle(object username)
        {
            var context = new BasiliskTFContext();

            return context.Accounts.SingleOrDefault(a=> a.Username == username);
        }

        public bool Insert(Account model)
        {
            try
            {
                using (var context = new BasiliskTFContext())
                {
                    context.Accounts.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
