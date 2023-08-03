using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Shipment
{
    public class OrderShipmentViewModel
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string ShippedDate { get; set; }
        public string DueDate { get; set; }
        public string OrderDate { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationPostalCode { get; set; }
        public string Cost { get; set; }

    }
}
