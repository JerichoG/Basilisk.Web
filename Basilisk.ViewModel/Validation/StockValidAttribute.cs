using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Validation
{
    public class StockValidAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (var context = new BasiliskTFContext())
                {
                    var id = (long)validationContext.ObjectInstance.GetType().GetProperty("ProductId").GetValue(validationContext.ObjectInstance);  //naik ke level object
                    var stock = context.Products.SingleOrDefault(p => p.Id == id);
                    var cek = Convert.ToInt32(value.ToString()) > stock.Stock;
                    if (cek)
                    {
                        return new ValidationResult(ErrorMessage = "Jumlah pesanan melebihi Stock!");
                    }

                    
                }
            }
            return ValidationResult.Success;
        }

    }
}
