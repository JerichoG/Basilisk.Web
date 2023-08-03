using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Order
{
    public class IndexOrderViewModel
    {
        public IEnumerable<GridOrderViewModel> Orders { get; set; }
        public string SearchCustomer { get; set; }
        public string SearchInvNumber { get; set; }
        public int TotalData { get; set; }
        public double TotalHalaman { get; set; }
    }
}
