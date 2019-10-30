using FRS.Business_Layer;
using FRS.Models;
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

        [HttpGet]
        public JsonResult GetUserList()
        {
            AdminBAL bal = new AdminBAL();
            var userList = bal.GetUserList();
            return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public PartialViewResult AddUpdateUserView(int? Id)
        {
            UserDetails userDetails = new UserDetails();
            CommonBAL bal = new CommonBAL();
            userDetails.RoleList = bal.GetRolesList();
            userDetails.ManagerList = bal.GetListOfManagers();
            if (Id == null)
            {
                ViewBag.ModalTitle = "Add User";
            }
            else
            {
                ViewBag.ModalTitle = "Update User";
            }
            return PartialView(userDetails);
        }

        [HttpPost]
        public JsonResult AddUpdateUser(UserDetails userDetails)
        {
            bool result = true;
            try
            {
                if (userDetails.ID == 0)
                {
                    AdminBAL bal = new AdminBAL();
                    result = bal.AddUserDetails(userDetails);
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