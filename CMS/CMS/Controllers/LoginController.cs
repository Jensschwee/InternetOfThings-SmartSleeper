using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using CMS.DAL;


namespace CMS.Controllers
{
    public class LoginController : Controller, IAuthorizationRequirement
    {
        private UserDal _userBackend = new UserDal();

        public IActionResult Index(bool loginFail = false)
        {
            if(loginFail)
                ViewData["ErrorMsg"] = "Username and password does not match";
            ViewData["title"] = "Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.LoginModel login)
        {
            bool isLoginValided = _userBackend.Login(login.Username, login.Password).Result;

            if (isLoginValided)
            {
                var identity = new ClaimsIdentity("SmartSleeper");
                identity.AddClaim(new Claim(ClaimTypes.Name, login.Username));
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.Authentication.SignInAsync("SmartSleeper", principal);
                return RedirectToAction("Index", "Board");
            }
            return RedirectToAction("Index", new { loginFail = true});
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("SmartSleeper");
            return RedirectToAction("Index", "Login");
        }
    }
}