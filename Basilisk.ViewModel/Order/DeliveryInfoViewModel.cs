using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Order
{
    public class DeliveryInfoViewModel
    {
        public string ShippedDate { get; set; }
        public string DueDate { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationPostalCode { get; set; }
        public string DeliveryCost { get; set; }

    }
}
