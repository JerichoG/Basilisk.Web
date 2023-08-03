using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Customer
{
    public class CatalogProductViewModel
    {
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string FormatedPrice { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
