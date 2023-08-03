﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Supplier
{
    public class IndexSupplierViewModel
    {
        public string SearchName { get; set; }
        public int TotalHalaman { get; set; }
        public int TotalData { get; set; }
        public IEnumerable<GridSupplierViewModel> Suppliers { get; set; }
    }
}
