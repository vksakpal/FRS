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
            CommonBAL bal = new CommonBAL();
            var statusList = bal.GetFaultStatusList();
            ViewBag.FaultStatusList = statusList;
            ViewBag.SelectedFaultStatusList = statusList.Where(x => x.Text.ToLower() == "in progress" || x.Text.ToLower() == "new").Select(x => x.Value).ToList();
            return PartialView("FaultListView");
        }

        [HttpGet]
        public PartialViewResult FaultDetailsView(int faultId)
        {
            FaultDetails faultDetails = new FaultDetails();
            FaultManagerBAL fmBal = new FaultManagerBAL();
            CommonBAL bal = new CommonBAL();
            if (faultId > 0)
            {
                ViewBag.ModalTitle = "Update Fault";
                faultDetails = fmBal.GetFaultListByFaultId(faultId);
                faultDetails.DeveloperCommentList = fmBal.GetDeveloperComments(faultId);
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
            if (faultId == 0)
            {
                faultDetails.StatusID = Convert.ToInt32(faultDetails.FaultStatusList.Where(x => x.Text.ToLower() == "new").Select(x => x.Value).FirstOrDefault());
                faultDetails.StatusDescription = "New";
            }

            return PartialView("FaultDetailsView", faultDetails);
        }

        [HttpPost]
        public JsonResult AddFaultDetails(FaultDetails faultDetails)
        {
            int result = 0;
            int faultId = 0;
            UserDetails userDetails = ((MyPrincipal)HttpContext.User).User;
            if (faultDetails == null)
            {
                result = 1;
            }

            try
            {
                if(userDetails.RoleID == 2)
                {
                    faultDetails.FaultPriorityID = 1;
                }

                FaultManagerBAL bal = new FaultManagerBAL();                
                faultId = bal.AddFaultDetails(faultDetails,userDetails);
                result = 2;
            }
            catch (Exception ex)
            {
                result = 3;
            }

            return Json(new { result, faultId }, JsonRequestBehavior.AllowGet);

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
                UserDetails userDetails = ((MyPrincipal)HttpContext.User).User;
                FaultManagerBAL bal = new FaultManagerBAL();
                if(userDetails.RoleID == 4)
                {
                    bal.AddDeveloperComment(faultDetails.FaultID,userDetails.ID, faultDetails.Comment, faultDetails.FaultResolvedDate, faultDetails.StatusID);
                }
                else
                {
                    bal.UpdateFaultDetails(faultDetails,userDetails.RoleID);
                }
                
                result = 2;
            }
            catch (Exception ex)
            {
                result = 3;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetFaultList(int[] status)
        {
            List<FaultDetails> faultList = new List<FaultDetails>();
            CommonBAL commonBal = new CommonBAL();


            if (HttpContext.User != null)
            {

                UserDetails userDetails = ((MyPrincipal)HttpContext.User).User;


                FaultManagerBAL bal = new FaultManagerBAL();
                faultList = bal.GetFaultList(string.Join(",", status), userDetails.RoleID, userDetails.ID);
                if (userDetails.RoleID == 3)
                {
                    var developerList = commonBal.GetListOfDevelopersByManagerId(userDetails.ID);
                    //faultList = faultList.Select(x => x.DeveloperList = developerList).ToList();
                    faultList.All(c => { c.DeveloperList = developerList; return true; });
                }

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
                success = bal.AssignFault(faultId, userId);
            }
            catch
            {
                success = false;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult AddCommentView(int faultId, int statusId)
        {
            ViewBag.FaultId = faultId;
            ViewBag.StatusId = statusId;
            CommonBAL bal = new CommonBAL();
            ViewBag.FaultStatusList = bal.GetFaultStatusList();
            return PartialView("AddCommentView");
        }


        [HttpPost]
        public JsonResult AddComment(int faultId, string comment, int statusId, string faultResolvedDate)
        {
            var result = false;
            UserDetails userDetails = ((MyPrincipal)HttpContext.User).User;
            FaultManagerBAL bal = new FaultManagerBAL();
            result = bal.AddDeveloperComment(faultId, userDetails.ID, comment, faultResolvedDate, statusId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}