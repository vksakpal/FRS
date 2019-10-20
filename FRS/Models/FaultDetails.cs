using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Models
{
    public class FaultDetails
    {
        public int FaultID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int StatusID { get; set; }
        public string StatusDescription { get; set; }
        public string UserID { get; set; }
        public int AssignedUserID { get; set; }
        public string FaultReportingDate { get; set; }        
        public int FaultTypeID { get; set; }
        public string FaultTypeDescription { get; set; }
        public string FaultResolvedDate { get; set; }
        public string FaultDescription { get; set; }
        public Customer CustomerInfo { get; set; }        
        public List<SelectListItem> ProductList { get; set; }
        public List<SelectListItem> FaultTypeList { get; set; }
        public List<SelectListItem> FaultStatusList { get; set; }

    }
}