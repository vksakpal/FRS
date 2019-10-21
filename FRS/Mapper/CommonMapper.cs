using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRS.Mapper
{
    public static class CommonMapper
    {
        public static List<SelectListItem> MapProductsDataTableToCollection(this DataTable dt)
        {
            List<SelectListItem> productList = new List<SelectListItem>();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    productList.Add(new SelectListItem
                    {
                        
                        Value = Convert.IsDBNull(dr["ProductID"]) ? string.Empty : Convert.ToString(dr["ProductID"]),
                        Text = Convert.IsDBNull(dr["ProductName"]) ? string.Empty : Convert.ToString(dr["ProductName"]),
                        
                    });
                }
            }

            return productList;
        }


        public static List<SelectListItem> MapFaultTypesDataTableToCollection(this DataTable dt)
        {
            List<SelectListItem> faultTypeList = new List<SelectListItem>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    faultTypeList.Add(new SelectListItem
                    {

                        Value = Convert.IsDBNull(dr["FaultTypeId"]) ? string.Empty : Convert.ToString(dr["FaultTypeId"]),
                        Text = Convert.IsDBNull(dr["FaultTypeDescription"]) ? string.Empty : Convert.ToString(dr["FaultTypeDescription"]),

                    });
                }
            }

            return faultTypeList;
        }

        public static List<SelectListItem> MapFaultStatusDataTableToCollection(this DataTable dt)
        {
            List<SelectListItem> faultTypeList = new List<SelectListItem>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    faultTypeList.Add(new SelectListItem
                    {

                        Value = Convert.IsDBNull(dr["StatusID"]) ? string.Empty : Convert.ToString(dr["StatusID"]),
                        Text = Convert.IsDBNull(dr["StatusDescription"]) ? string.Empty : Convert.ToString(dr["StatusDescription"]),

                    });
                }
            }

            return faultTypeList;
        }

        public static List<SelectListItem> MapDeveloperDataTableToCollection(this DataTable dt)
        {
            List<SelectListItem> developerList = new List<SelectListItem>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    developerList.Add(new SelectListItem
                    {

                        Value = Convert.IsDBNull(dr["ID"]) ? string.Empty : Convert.ToString(dr["ID"]),
                        Text = Convert.IsDBNull(dr["UserID"]) ? string.Empty : Convert.ToString(dr["UserID"]),

                    });
                }
            }

            return developerList;
        }
    }
}