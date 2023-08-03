using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Shipment
{
    public class UpsertShipmentViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        [StringLength(50, ErrorMessage = "Maks. 50 karakter")]
        [MinLength(10, ErrorMessage = "Min. 10 karakter")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        [RegularExpression(@"^[0-9()\s\S''-'\w\s]{1,20}$", ErrorMessage ="Isi dengan benar!")]
        [MinLength(11, ErrorMessage = "Min. 11 karakter")]
        [StringLength(20, ErrorMessage = "Maks. 20 karakter")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        public decimal Cost { get; set; }
    }
}
