using FRS.DAL;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FRS.Mapper
{
    public static class MapperExtension
    {
        public static List<FaultDetails> MapFaultDetailsDataTableToCollection(this DataTable dt)
        {
            List<FaultDetails> faultDetailsList = new List<FaultDetails>();            
            if(dt!= null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    faultDetailsList.Add(new FaultDetails
                    {
                        FaultID = Convert.IsDBNull(dr["FaultID"]) ? 0: Convert.ToInt32(dr["FaultID"]),
                        ProductID = Convert.IsDBNull(dr["ProductID"]) ? 0 : Convert.ToInt32(dr["ProductID"]),
                        ProductName = Convert.IsDBNull(dr["ProductName"]) ? string.Empty : Convert.ToString(dr["ProductName"]),
                        StatusID = Convert.IsDBNull(dr["StatusID"]) ? 0 : Convert.ToInt32(dr["StatusID"]),
                        StatusDescription = Convert.IsDBNull(dr["StatusDescription"]) ? string.Empty : Convert.ToString(dr["StatusDescription"]),
                        UserID = Convert.IsDBNull(dr["UserID"]) ? string.Empty : Convert.ToString(dr["UserID"]),
                        AssignedUserID = Convert.IsDBNull(dr["AssignedUserID"]) ? 0 : Convert.ToInt32(dr["AssignedUserID"]),
                        FaultReportingDate = Convert.IsDBNull(dr["FaultReportingDate"]) ? string.Empty : string.Format("{0: dd/MM/yyyy}", Convert.ToDateTime(dr["FaultReportingDate"])),
                        CustomerInfo = new Customer
                        {
                            ID = Convert.IsDBNull(dr["CustomerID"]) ? 0 : Convert.ToInt32(dr["CustomerID"]),
                            Name = Convert.IsDBNull(dr["CustomerName"]) ? string.Empty : Convert.ToString(dr["CustomerName"]),
                            Email = Convert.IsDBNull(dr["Email"]) ? string.Empty : Convert.ToString(dr["Email"]),
                            Phone = Convert.IsDBNull(dr["Phone"]) ? string.Empty : Convert.ToString(dr["Phone"]),
                        },
                        FaultResolvedDate = Convert.IsDBNull(dr["FaultResolvedDate"]) ? string.Empty : string.Format("{0: dd/MM/yyyy}", Convert.ToDateTime(dr["FaultResolvedDate"])),
                        FaultTypeID = Convert.IsDBNull(dr["FaultTypeID"]) ? 0 : Convert.ToInt32(dr["FaultTypeID"]),
                        FaultDescription = Convert.IsDBNull(dr["FaultDescription"]) ? string.Empty : Convert.ToString(dr["FaultDescription"]),
                        FaultTypeDescription = Convert.IsDBNull(dr["FaultTypeDescription"]) ? string.Empty : Convert.ToString(dr["FaultTypeDescription"]),
                        FaultPriorityID = Convert.IsDBNull(dr["FaultPriority"]) ? 0 : Convert.ToInt32(dr["FaultPriority"])                    });
                }
            }

            return faultDetailsList;
        }
    }
}