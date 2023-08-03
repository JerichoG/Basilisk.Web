using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Validation
{
    public class SuperiorEmployeeAttribute : ValidationAttribute
    {
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value != null)
            {
                using (var context = new BasiliskTFContext())
                {
                    var empNumber = (string)validationContext.ObjectInstance.GetType().GetProperty("EmployeeNumber").GetValue(validationContext.ObjectInstance);  //naik ke level object
                    var cek = empNumber == value.ToString() ;
                    if (cek)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
