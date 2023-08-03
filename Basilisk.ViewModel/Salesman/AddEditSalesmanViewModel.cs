using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Salesman
{
    public class AddEditSalesmanViewModel
    {
        //Jika di .Net lama maka harus ditambahkan attribute [Required]
        //Namun jika di .Net > 6.0 attribute [Required] sudah nempel pada property
        //Maka untuk di versi terbaru, buat menjadi nullable walaupun tipe datanya string
        [Required(ErrorMessage ="Wajib diisi")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Wajib diisi")]
        [BirthdateHiredate()]
        public DateTime? HiredDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        [PhoneMinMax(MaxLength = 13, MinLength = 11)]
        public string? Phone { get; set; }

        [SuperiorEmployee(ErrorMessage = "Tidak bisa memilih diri sendiri sebagai Superior")]
        public string? SuperiorEmployeeNumber { get; set; }

        public List<SelectListItem>? DropdownSuperiorEmp { get; set; }


    }
}
