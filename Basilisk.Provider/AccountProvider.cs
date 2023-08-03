using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Account;
using Basilisk.ViewModel.Salesman;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Basilisk.Utility;

namespace Basilisk.Provider
{
    public class AccountProvider : BaseProvider
    {
        private static AccountProvider instance = new AccountProvider();
        public static AccountProvider GetProvider()
        {
            return instance;
        }

        public bool IsAuthentication(LoginViewModel model)
        {
            var hashPassword = AccountRepository.GetRepository().GetSingle(model.Username).Password;

            var check = BCrypt.Net.BCrypt.Verify(model.Password, hashPassword);
            if (!check)
            {
                return false;
            }

            return AccountRepository.GetRepository().GetIsAuthentication(model.Username, hashPassword);

        }

        public string GetRoleName(string username)
        {
            return AccountRepository.GetRepository().GetRole(username);
        }


        #region Index Customer
        public IEnumerable<GridAccountViewModel> GetDataIndex()
        {

            var accounts = (from acc in AccountRepository.GetRepository().GetAll().AsEnumerable()
                             select new GridAccountViewModel
                             {
                                Username = acc.Username,
                                Role = acc.Role,
                             }).AsEnumerable();

           
            return accounts.ToList();

        }

        public IndexAccountViewModel GetIndex(int page)
        {
            var data = GetDataIndex();


            var model = new IndexAccountViewModel
            {

                Accounts = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        public UpsertAccountViewModel GetAddForm()
        {
            var model = new UpsertAccountViewModel
            {
                Roles = GetRole()
            };
            return model;
        }

        public bool AddData(UpsertAccountViewModel model)
        {
            try
            {
                var data = new Account();
                if(string.IsNullOrEmpty( model.Password))
                {
                    model.Password = "indocyber";
                }

                MappingModel<Account, UpsertAccountViewModel>(data, model);

                data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
                AccountRepository.GetRepository().Insert(data);

                return true;
            }
            catch
            {
                return false;
            }

        }

        private List<DropdownListViewModel> GetRole()
        {

            
            var result = new List<DropdownListViewModel>();
            foreach(int value in Enum.GetValues(typeof(EnumRoles)))
            {
                result.Add(new DropdownListViewModel
                {
                    StringValue = Enum.GetName(typeof(EnumRoles), value)
                });
            }

            return result;

        }

        #endregion
    }
}
