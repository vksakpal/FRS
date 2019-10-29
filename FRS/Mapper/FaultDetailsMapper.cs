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
                    var customerName = Convert.IsDBNull(dr["CustomerName"]) ? string.Empty : Convert.ToString(dr["CustomerName"]);
                    string firstName = string.Empty;
                    string lastName = string.Empty;

                    if(!string.IsNullOrEmpty(customerName))
                    {
                        var customer = customerName.Split(' ');
                        if(customer != null && customer.Length > 0)
                        {
                            firstName = customer[0];
                            if(customer.Length > 1)
                            {
                                lastName = customer[1];
                            }
                        }

                    }

                    faultDetailsList.Add(new FaultDetails
                    {
                        FaultID = Convert.IsDBNull(dr["FaultID"]) ? 0: Convert.ToInt32(dr["FaultID"]),
                        ProductID = Convert.IsDBNull(dr["ProductID"]) ? 0 : Convert.ToInt32(dr["ProductID"]),
                        ProductName = Convert.IsDBNull(dr["ProductName"]) ? string.Empty : Convert.ToString(dr["ProductName"]),
                        StatusID = Convert.IsDBNull(dr["StatusID"]) ? 0 : Convert.ToInt32(dr["StatusID"]),
                        StatusDescription = Convert.IsDBNull(dr["StatusDescription"]) ? string.Empty : Convert.ToString(dr["StatusDescription"]),
                        UserID = Convert.IsDBNull(dr["UserID"]) ? string.Empty : Convert.ToString(dr["UserID"]),
                        AssignedUserID = Convert.IsDBNull(dr["AssignedUserID"]) ? 0 : Convert.ToInt32(dr["AssignedUserID"]),
                        FaultReportingDate = Convert.IsDBNull(dr["FaultReportingDate"]) ? string.Empty : string.Format("{0: dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(dr["FaultReportingDate"])),
                        CustomerInfo = new Customer
                        {
                            ID = Convert.IsDBNull(dr["CustomerID"]) ? 0 : Convert.ToInt32(dr["CustomerID"]),
                             FirstName = firstName,
                             LastName = lastName,
                            Email = Convert.IsDBNull(dr["Email"]) ? string.Empty : Convert.ToString(dr["Email"]),
                            Phone = Convert.IsDBNull(dr["Phone"]) ? string.Empty : Convert.ToString(dr["Phone"]),
                        },
                        FaultResolvedDate = Convert.IsDBNull(dr["FaultResolvedDate"]) ? string.Empty : string.Format("{0: dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(dr["FaultResolvedDate"])),
                        FaultTypeID = Convert.IsDBNull(dr["FaultTypeID"]) ? 0 : Convert.ToInt32(dr["FaultTypeID"]),
                        FaultDescription = Convert.IsDBNull(dr["FaultDescription"]) ? string.Empty : Convert.ToString(dr["FaultDescription"]),
                        FaultTypeDescription = Convert.IsDBNull(dr["FaultTypeDescription"]) ? string.Empty : Convert.ToString(dr["FaultTypeDescription"]),
                        FaultPriorityID = Convert.IsDBNull(dr["FaultPriority"]) ? 0 : Convert.ToInt32(dr["FaultPriority"])                    });
                }
            }

            return faultDetailsList;
        }


        public static List<DeveloperComments> MapDeveloperCommentsDataTableToCollection(this DataTable dt)
        {
            List<DeveloperComments> developerCommentsList = new List<DeveloperComments>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    developerCommentsList.Add(new DeveloperComments
                    {
                        FaultId = Convert.IsDBNull(dr["FaultID"]) ? 0 : Convert.ToInt32(dr["FaultID"]),
                        UserId = Convert.IsDBNull(dr["UserID"]) ? 0 : Convert.ToInt32(dr["UserID"]),
                        Comment = Convert.IsDBNull(dr["Comments"]) ? string.Empty : Convert.ToString(dr["Comments"]),                        
                        UserName = Convert.IsDBNull(dr["UserName"]) ? string.Empty : Convert.ToString(dr["UserName"]),
                        Date = Convert.IsDBNull(dr["CreatedDate"]) ? string.Empty : string.Format("{0: dd/MM/yyyy HH:mm}", Convert.ToDateTime(dr["CreatedDate"])),
                        
                    });
                }
            }

            return developerCommentsList;
        }
    }
}