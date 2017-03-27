using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace CMS.Controllers
{
    public class LoginController : Controller, IAuthorizationRequirement
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.LoginModel login)
        {
            //TODO: add login funktion
            var identity = new ClaimsIdentity("SmartSleeper");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Username));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync("SmartSleeper", principal);
            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("SmartSleeper");
            return RedirectToAction("Index", "Login");
        }
    }
}