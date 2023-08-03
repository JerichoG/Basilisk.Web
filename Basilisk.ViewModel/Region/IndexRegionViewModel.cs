using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Region
{
    public class IndexRegionViewModel
    {
        public string SearchCity { get; set; }
        public IEnumerable<RegionViewModel> ListRegion { get; set;}
        public int TotalData { get; set; }
        public double TotalHalaman { get; set; }
    }
}
