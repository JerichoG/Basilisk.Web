using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Basilisk.DataAccess.Models;
using Newtonsoft.Json.Linq;

namespace Basilisk.ViewModel.Validation
{

    public class BirthdateHiredateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (var context = new BasiliskTFContext())
                {
                    var birthDate = (DateTime)validationContext.ObjectInstance.GetType().GetProperty("BirthDate").GetValue(validationContext.ObjectInstance);  //naik ke level object
                    var cek = birthDate > DateTime.Parse(value.ToString());
                    if (cek)
                    {
                        return new ValidationResult(ErrorMessage = "HireDate tidak boleh kurang dari BirthDate!");
                    }

                    int age = DateTime.Parse(value.ToString()).Year - birthDate.Year;
                    if (age <= 18)
                    {
                        return new ValidationResult(ErrorMessage = "Umur minimal 18 Tahun saat dipekerjakan!");
                    }
                }
            }
            return ValidationResult.Success;
        }

        
    }
}
