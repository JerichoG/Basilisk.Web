using Basilisk.Provider;
using Basilisk.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Basilisk.ViewModel.Account;

namespace Basilisk.Web.Controllers
{
    public class AccountController : BaseController
    {


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginViewModel();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (AccountProvider.GetProvider().IsAuthentication(model))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("username", model.Username),
                        new Claim(ClaimTypes.NameIdentifier, model.Username),
                        new Claim(ClaimTypes.Role, AccountProvider.GetProvider().GetRoleName(model.Username))
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(principal);

                    if(returnUrl  == null)
                    {
                        return RedirectToAction("Index","Home");
                    }

                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("LoginFailed");
                }

            }
            return View(model);

            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginFailed()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var accounts = AccountProvider.GetProvider().GetIndex(page);

            return View(accounts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            SetUsernameRole(User.Claims);
            var model = AccountProvider.GetProvider().GetAddForm();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(UpsertAccountViewModel model)
        {

            try
            {
                AccountProvider.GetProvider().AddData(model);
                return RedirectToAction("Index");
            }
            catch
            {

                throw;
            }

            return View("Add", model);
        }
    }
}
