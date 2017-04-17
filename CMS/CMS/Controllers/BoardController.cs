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
        private SensorReadingDal sensorReadingDal = new SensorReadingDal();


        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            ViewData["title"] = "Board";
            return View(boardDal.GetAllBoards(identity.Name).Result);
        }

        public IActionResult BoardDetails(string deviceId)
        {
            ViewData["title"] = "Board Details";
            return View(sensorReadingDal.GetAllSensorReadings(deviceId).Result);
        }

        public IActionResult BoardDelete(string deviceId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            ViewData["title"] = "Board Delete";
            return View(boardDal.GetBoards(identity.Name, deviceId).Result);
        }

        [HttpPost]
        public IActionResult BoardDelete(BoardModel board)
        {
            var identity = (ClaimsIdentity)User.Identity;

            bool isSaved = boardDal.DeleteBoard(board, identity.Name).Result;

            if (isSaved)
                return RedirectToAction("Index");

            return RedirectToActionPermanent("BoardDelete", new {deviceId = board.deviceID});
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