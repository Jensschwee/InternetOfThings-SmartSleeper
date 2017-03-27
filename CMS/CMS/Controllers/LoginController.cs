using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Logic;
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
            var identity = new ClaimsIdentity("SmartSleeper");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Username));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync("SmartSleeper", principal);
            return RedirectToAction("Index", "Board");
        }
    }
}