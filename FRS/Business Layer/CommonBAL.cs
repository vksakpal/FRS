using FRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Business_Layer
{
    public class CommonBAL
    {
        public List<SelectListItem> GetProductList()
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetProductList();
        }

        public List<SelectListItem> GetFaultTypeList()
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetFaultTypesList();
        }

        public List<SelectListItem> GetFaultStatusList()
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetFaultStatusList();
        }
    }
}