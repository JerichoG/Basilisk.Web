using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.ViewModel.Product
{
    public class UpsertViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Wajib diisi")]
        public string Name { get; set; }
        public long? SupplierId { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        public int Stock { get; set; }
        
        [Required(ErrorMessage = "Wajib diisi")] 
        public int OnOrder { get; set; }
        public bool Discontinue { get; set; }


        public List<SelectListItem> DropdownCategory { get; set; }

        public List<DropdownListViewModel> DropdownSupplierCustom { get; set; }

    }
}
