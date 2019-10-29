using FRS.Business_Layer;
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
        public JsonResult GetCustomerList()
        {
            AdminBAL bal = new AdminBAL();
            var customerList = bal.GetCustomerList();
            return Json(new { data = customerList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult AddCustomerView(int? customerId)
        {
            Customer objCustomer = new Customer();
            if (customerId == null)
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
            bool result = true;

            try
            {
                if (customer.ID == 0)
                {
                    AdminBAL bal = new AdminBAL();
                    result = bal.AddCustomer(customer);
                }
                else
                {
                    //Todo: Call Update customer Bal
                }
            }
            catch
            {
                result = false;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}