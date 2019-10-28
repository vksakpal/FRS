using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserListView()
        {
            return View("UserListView");
        }
    }
}