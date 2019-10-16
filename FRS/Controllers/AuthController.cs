﻿using FRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Controllers
{
    [RoutePrefix("auth")]
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        [Route("login")]
        public ActionResult LoginView()
        {
            ViewBag.IsAuthenticated = false;
            return View("LoginView");
        }

        [HttpPost]
        public ActionResult Login(LoginDetails loginDetails)
        {
            ViewBag.IsAuthenticated = true;
            return RedirectToAction("Index", "Home");
        }
    }
}