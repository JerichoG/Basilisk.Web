using Basilisk.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Customer
{
    public class ProductDetailViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        
        [StockValid()]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string FormatPrice { get; set; }
        public bool Checked { get; set; }

    }
}
