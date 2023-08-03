using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Customer
{
    public class CartViewModel
    {
        public List<CartDetailViewModel> CartDetails { get; set; }
        public decimal TotalPrice { get; set; }
        public string FormatTotalPrice { get; set; }
        public long CustomerId { get; set; }
    }
}
