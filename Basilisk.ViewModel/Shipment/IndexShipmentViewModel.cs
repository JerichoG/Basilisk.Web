using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Shipment
{
    public class IndexShipmentViewModel
    {
        public string SearchShipper { get; set; }
        public int TotalData { get; set; }
        public double TotalHalaman { get; set; }
        public IEnumerable<GridShipmentViewModel> Shipments { get; set; }
    }
}
