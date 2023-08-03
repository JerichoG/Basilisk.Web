﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Shipment
{
    public class DetailsShipmentViewModel
    {
        public long ShipId { get; set; }
        public string ShipName { get; set; }
        public string Phone { get; set; }

        public List<OrderShipmentViewModel> Products{ get; set; }
    }
}
