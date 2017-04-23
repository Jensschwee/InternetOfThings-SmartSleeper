using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.DAL;
using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    public class UserController : Controller
    {
        UserDal userDal = new UserDal();

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(LoginModel login)
        {
            bool isSaved = userDal.RegisterUser(login.Username, login.Password).Result;
            if (isSaved)
            {
                var identity = new ClaimsIdentity("SmartSleeper");
                identity.AddClaim(new Claim(ClaimTypes.Name, login.Username));
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.Authentication.SignInAsync("SmartSleeper", principal);

                return RedirectToAction("Index", "Board");
            }
            else
                return RedirectToAction("UserRegister");
        }
    }
}