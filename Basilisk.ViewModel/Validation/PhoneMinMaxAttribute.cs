using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Validation
{
    public class PhoneMinMaxAttribute : ValidationAttribute
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value.ToString().Length > MaxLength)
                {
                    return new ValidationResult(ErrorMessage = $"Maksimal {MaxLength} karakter");
                }else if (value.ToString().Length < MinLength)
                {
                    return new ValidationResult(ErrorMessage = $"Minimal {MinLength} karakter");
                }
            }
            return ValidationResult.Success;
        }
    }
}
