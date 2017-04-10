using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        public List<BoardModel> generateModel()
        {
            List<BoardModel> lbm = new List<BoardModel>();
            lbm.Add(new BoardModel("test", "test"));
            lbm.Add(new BoardModel("test1", "test1"));
            lbm.Add(new BoardModel("test2", "test2"));
            return lbm;
        }

        public IActionResult Index()
        {
            ViewData["title"] = "Board";
            return View(generateModel());
        }


        public IActionResult BoardDetails(string deviceId)
        {
            ViewData["title"] = "Board Details";
            return View(generateModel().FindLast(t => t.DeviceId == deviceId));
        }

    }
}