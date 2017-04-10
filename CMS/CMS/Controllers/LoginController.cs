using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            ViewData["title"] = "Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.LoginModel login)
        {
            bool isLoginValided = Login(login.Username, login.Password).Result;

            if (!isLoginValided)
            {
                var identity = new ClaimsIdentity("SmartSleeper");
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Username));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.Authentication.SignInAsync("SmartSleeper", principal);
                return RedirectToAction("Index", "Board");
            }
            return RedirectToAction("Index");
        }

        public async Task<bool> Login(string username, string password)
        {
            var retValue = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://smart-sleeper.herokuapp.com/users/login/" + username +"/"+password);
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("SmartSleeper");
            return RedirectToAction("Index", "Login");
        }
    }
}