using FRS.Business_Layer;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
                
                return View("LoginView");
            }
                               
            if (!ValidateUser(loginDetails, HttpContext.Response))
            {
                ViewBag.ErrorMessage = "Please enter valid Username and password";
                return View("LoginView");
                
            }
           
            return RedirectToAction("Index", "Home");
        }

        private  bool ValidateUser(LoginDetails loginDetails, HttpResponseBase response)
        {
            bool result = false;
            LoginBAL bal = new LoginBAL();
            UserDetails user = bal.Login(loginDetails);
            if (user != null)
            {
                
                var serializer = new JavaScriptSerializer();
                
                string userData = serializer.Serialize(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        loginDetails.UserID,
                        DateTime.Now,
                        DateTime.Now.AddDays(30),
                        true,
                        userData,
                        FormsAuthentication.FormsCookiePath);
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);
                // Create the cookie.
                response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                result = true;
            }
            return result;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("LoginView", "Auth");
        }
    }
}