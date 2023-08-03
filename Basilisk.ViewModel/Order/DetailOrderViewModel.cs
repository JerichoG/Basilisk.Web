using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Order
{
    public class DetailOrderViewModel
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string SalesName { get; set; }
        public string OrderDate { get; set;}

        public DeliveryInfoViewModel DeliveryInfo { get; set; }
        public IEnumerable<ProductInfoOrderViewModel> ProductOrders { get; set; }


    }
}
