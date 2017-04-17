using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.DAL;
using CMS.Helpers;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private BoardDal boardDal = new BoardDal();
        public List<BoardModel> generateModel()
        {
            List<BoardModel> lbm = new List<BoardModel>();
            lbm.Add(new BoardModel("test", "test"));
            lbm.Add(new BoardModel("test1", "test1"));
            lbm.Add(new BoardModel("test2", "test2"));
            return lbm;
        }

        public List<SensorReadingModel> generateSensorReadingModel()
        {
            List<SensorReadingModel> sem = new List<SensorReadingModel>();
            sem.Add(new SensorReadingModel(DateTime.Now, 2));
            sem.Add(new SensorReadingModel(DateTime.Now.AddDays(1), 50));
            return sem;
        }

        public IActionResult Index()
        {
            ViewData["title"] = "Board";
            return View(generateModel());
        }

        public IActionResult BoardDetails(string deviceId)
        {
            ViewData["title"] = "Board Details";
            return View(generateSensorReadingModel());
        }

        [HttpGet]
        public IActionResult BoardRegister()
        {

            ViewData["title"] = "Board Register";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BoardRegister(BoardModel board)
        {
            var identity = (ClaimsIdentity)User.Identity;
            bool isSaved = boardDal.SendtBoardRegister(board, identity.Name).Result;
            //if (isSaved)
            return RedirectToAction("Index", "Board");
        }

    }
}