using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Product
{
    public class IndexProductViewModel
    {
        public IEnumerable<GridProductViewModel> Grid { get; set; }
        public string ProdName { get; set; }
        public string SupName { get; set; }
        public string CatName { get; set; }
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
    }
}
