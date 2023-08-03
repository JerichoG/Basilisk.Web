using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Basilisk.Utility;
using Basilisk.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Basilisk.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetUsernameRole(IEnumerable<Claim> claims)
        {
            ViewBag.Username = GetUsername(claims);
            ViewBag.Role = GetRole(claims);
            
            SetMenuByUsername(claims);

        }

        protected string GetUsername(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
        }

        protected string GetRole(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(a => a.Type == ClaimTypes.Role).Value;
        }

        protected void SetMenuByUsername(IEnumerable<Claim> claims)
        {
            var role = GetRole(claims);
            ViewBag.Menus = GlobalConfiguration.Menus().Where(a=> a.Role == role);
        }

        protected IEnumerable<ValidationViewModel> GetValidationMessage(ModelStateDictionary modelState)
        {
            var result = new List<ValidationViewModel>();
            foreach (KeyValuePair<string, ModelStateEntry> item in modelState)
            {
                if(item.Value.Errors.Count > 0)
                {
                    result.Add(new ValidationViewModel
                    {
                        PropertyName = item.Key,
                        MessageError = item.Value.Errors.FirstOrDefault().ErrorMessage,
                    }) ;
                }
            }
            return result;
        }

    }
}
