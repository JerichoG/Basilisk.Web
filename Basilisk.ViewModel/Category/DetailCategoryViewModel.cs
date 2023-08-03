using Basilisk.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Category
{
    public class DetailCategoryViewModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<GridProductViewModel> Products { get; set; }
    }
}
