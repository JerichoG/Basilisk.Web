using Basilisk.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Region
{
    public class UpsertRegionViewModel
    {
        public long Id { get; set; }

        [DisplayName( "City Name")] //berguna untuk labelling, strukturnya wajib persis di atas property name
        [StringLength(maximumLength:50, ErrorMessage = "Maksimal 50 Karakter")]
        [UniqueCityName(ErrorMessage = "Nama City sudah ada!")]
        [Required(ErrorMessage = "String Wajib diisi")]
        public string City { get; set; }
        public string Remark { get; set; }
    }
}
