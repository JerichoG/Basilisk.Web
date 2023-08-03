using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Account
{
    public class UpsertAccountViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<DropdownListViewModel> Roles { get; set; }
    }
}
