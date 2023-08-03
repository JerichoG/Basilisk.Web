using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Account
{
    public class IndexAccountViewModel
    {
        public IEnumerable<GridAccountViewModel> Accounts { get; set; }
        public int TotalHalaman { get; set; }
        public int TotalData { get; set; }
    }
}
