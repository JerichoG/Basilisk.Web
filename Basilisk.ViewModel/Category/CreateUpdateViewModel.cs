using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Category
{
    public class CreateUpdateViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        [StringLength(50, ErrorMessage = "Maks. 50 karakter")]
        [MinLength(5, ErrorMessage = "Min. 5 karakter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        [StringLength(500, ErrorMessage = "Maks. 500 karakter")]
        [MinLength(10, ErrorMessage = "Min. 10 karakter")]
        public string Description { get; set; }
    }
}
