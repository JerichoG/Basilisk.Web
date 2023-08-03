using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Category
{
    public class IndexCategoryViewModel
    {
        public List<GridCategoryViewModel> Grid { get; set; }
    
        public string SearchName { get; set; }
        public int TotalData { get; set; }
        public double TotalHalaman { get; set; }
    }
}
