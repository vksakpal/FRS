using FRS.Business_Layer;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Controllers
{
    public class FaultManagementController : Controller
    {
        // GET: FaultManagement
        public ActionResult Index()
        {
            return View();
        }

        [Route("faultlist")]
        public PartialViewResult GetFaultDetailsView()
        {
            return PartialView("FaultListView");
        }

        [HttpGet]
        public PartialViewResult FaultDetailsView(int faultId)
        {
            if(faultId > 0)
            {
                ViewBag.ModalTitle = "Update Fault";
            }
            else
            {
                ViewBag.ModalTitle = "Add Fault";
            }
            return PartialView("FaultDetailsView");
        }

        [HttpGet]
        public JsonResult GetFaultList(int status)
        {
            FaultManagerBAL bal = new FaultManagerBAL();
            List<FaultDetails> faultList = bal.GetFaultList(status);
            return Json(new { data = faultList }, JsonRequestBehavior.AllowGet);
        }
    }
}