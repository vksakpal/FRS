﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LoginView", "Auth");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("faultlist")]
        public PartialViewResult GetFaultDetailsView()
        {
            return PartialView("FaultListView");
        }
    }
}