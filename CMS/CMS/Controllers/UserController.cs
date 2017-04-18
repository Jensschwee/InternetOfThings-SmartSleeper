using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult UserRegister(LoginModel login)
        {
            bool isSaved = userDal.RegisterUser(login.Username, login.Password).Result;
            if(isSaved)
                return RedirectToAction("Login", "Login", login);
            else
                return RedirectToAction("UserRegister");
        }
    }
}