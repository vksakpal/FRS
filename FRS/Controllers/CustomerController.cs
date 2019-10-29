using FRS.Models;
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

        [HttpGet]
        public PartialViewResult AddCustomerView(int? customerId)
        {
            Customer objCustomer = new Customer();
            if(customerId == null)
            {
                ViewBag.ModalTitle = "Add Customer";

            }
            else
            {
                ViewBag.ModalTitle = "Update Customer";
            }
            
            return PartialView(objCustomer);
        }

        [HttpPost]
        public JsonResult AddUpdateCustomer(Customer customer)
        {
            if (customer.ID == 0)
            {
                //Todo: Call Add customer Bal
            }
            else
            {
                //Todo: Call Update customer Bal
            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}