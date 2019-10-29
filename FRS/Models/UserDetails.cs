using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Models
{
    public class UserDetails
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int RoleID { get; set; }
        public string UserPassword { get; set; }
        public int ManagerId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> ManagerList { get; set; }
        public string ManagerName { get; set; }
        public string RoleDescription { get; set; }
    }
}