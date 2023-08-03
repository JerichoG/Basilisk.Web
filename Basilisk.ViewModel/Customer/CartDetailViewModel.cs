using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Customer
{
    public class CartDetailViewModel
    {

        public string Seller { get; set; }
        public long SellerId { get; set; }
        public List<ProductDetailViewModel> Products { get; set; }
        public bool CheckedAll { get; set; }

        
    }
}
