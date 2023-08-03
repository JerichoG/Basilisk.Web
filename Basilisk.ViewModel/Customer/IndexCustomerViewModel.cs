using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Customer
{
    public class IndexCustomerViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public string SearchCustomer { get; set; }
        public IEnumerable<GridCustomerViewModel> Customers { get; set; }
    }
}
