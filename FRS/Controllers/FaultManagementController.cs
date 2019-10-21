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
            FaultDetails faultDetails = new FaultDetails();
            FaultManagerBAL fmBal = new FaultManagerBAL();
            CommonBAL bal = new CommonBAL();
            if(faultId > 0)
            {
                ViewBag.ModalTitle = "Update Fault";
                faultDetails = fmBal.GetFaultListByFaultId(faultId);
            }
            else
            {
                ViewBag.ModalTitle = "Add Fault";
            }

            faultDetails.ProductList = bal.GetProductList();
            faultDetails.FaultTypeList = bal.GetFaultTypeList();
            faultDetails.FaultStatusList = bal.GetFaultStatusList();
            faultDetails.FaultPriorityList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "1",
                    Text = "Minor"
                },
                new SelectListItem
                {
                    Value = "2",
                    Text = "Major"
                }
            };
            if(faultId ==0 )
            {
                faultDetails.StatusID = Convert.ToInt32(faultDetails.FaultStatusList.Where(x => x.Text.ToLower() == "new").Select(x => x.Value).FirstOrDefault());
            }
            
            return PartialView("FaultDetailsView", faultDetails);
        }

        [HttpPost]
        public JsonResult AddFaultDetails(FaultDetails faultDetails)
        {
            int result = 0;
            int faultId = 0;

            if(faultDetails == null)
            {
                result = 1;
            }

            try
            {
                FaultManagerBAL bal = new FaultManagerBAL();
                faultId = bal.AddFaultDetails(faultDetails);
                result = 2;
            }
            catch(Exception ex)
            {
                result = 3;
            }

            return Json(new { result ,faultId}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdateFaultDetails(FaultDetails faultDetails)
        {
            int result = 0;

            if (faultDetails == null)
            {
                result = 1;
            }

            try
            {
                FaultManagerBAL bal = new FaultManagerBAL();
                bal.UpdateFaultDetails(faultDetails);
                result = 2;
            }
            catch (Exception ex)
            {
                result = 3;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetFaultList(int status)
        {
            List<FaultDetails> faultList = new List<FaultDetails>();

            if (HttpContext.User != null)
            {
                UserDetails userDetails = ((MyPrincipal)HttpContext.User).User;
                FaultManagerBAL bal = new FaultManagerBAL();
                 faultList = bal.GetFaultList(status, userDetails.RoleID, userDetails.ID);
                
            }
            return Json(new { data = faultList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFaultListByFaultID(int faultId)
        {
            FaultDetails faultDetail = new FaultDetails();

            if (HttpContext.User != null)
            {
                FaultManagerBAL bal = new FaultManagerBAL();
                faultDetail = bal.GetFaultListByFaultId(faultId);

            }
            return Json(new { data = faultDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AssignFault(int faultId, int userId)
        {
            bool success = false;
            try
            {
                FaultManagerBAL bal = new FaultManagerBAL();
                success = bal.AssignFault(faultId,userId);
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }


    }
}