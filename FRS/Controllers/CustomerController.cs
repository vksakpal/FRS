using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomerListView()
        {
            return View();
        }
    }
}