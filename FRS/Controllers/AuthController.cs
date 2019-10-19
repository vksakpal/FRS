using FRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDetails loginDetails)
        {

            if(string.IsNullOrWhiteSpace(loginDetails?.UserID) || string.IsNullOrWhiteSpace(loginDetails.Password))
            {
                ViewBag.ErrorMessage = "Please enter Username and password";
                ViewBag.IsAuthenticated = false;
                return View("LoginView");
            }
            FormsAuthentication.SetAuthCookie(loginDetails.UserID,false);
            ViewBag.IsAuthenticated = true;
            return RedirectToAction("Index", "Home");
        }
    }
}