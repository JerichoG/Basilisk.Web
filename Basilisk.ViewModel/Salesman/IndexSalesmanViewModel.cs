using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Salesman
{
    public class IndexSalesmanViewModel
    {
        public IEnumerable<GridSalesmanViewModel> Salesmen { get; set; }
        public string SearchID { get; set; }
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
    }
}
