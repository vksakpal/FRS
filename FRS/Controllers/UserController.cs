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
        public PartialViewResult AddUpdateUserView(int? Id)
        {
            UserDetails userDetails = new UserDetails();
            CommonBAL bal = new CommonBAL();
            userDetails.RoleList = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Value = "1",
                     Text = "Help Desk"
                },
                new SelectListItem
                {
                     Value = "2",
                     Text = "Manager"
                }
            };

            userDetails.ManagerList = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Value = "1",
                     Text = "Tina"
                },
                new SelectListItem
                {
                     Value = "2",
                     Text = "Brenda"
                }
            };

            if (Id== null)
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
            if (userDetails.ID == 0)
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