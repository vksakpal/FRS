using FRS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FRS.Mapper
{
    public static class AdminMapper
    {
        public static List<Customer> MapCustomerDataTableToCollection(this DataTable dt)
        {
            List<Customer> customerList = new List<Customer>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var customerName = Convert.IsDBNull(dr["Name"]) ? string.Empty : Convert.ToString(dr["Name"]);
                    string firstName = string.Empty;
                    string lastName = string.Empty;

                    if (!string.IsNullOrEmpty(customerName))
                    {
                        var customer = customerName.Split(' ');
                        if (customer != null && customer.Length > 0)
                        {
                            firstName = customer[0];
                            if (customer.Length > 1)
                            {
                                lastName = customer[1];
                            }
                        }
                    }

                    customerList.Add(new Customer
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Phone = Convert.IsDBNull(dr["Phone"]) ? string.Empty : Convert.ToString(dr["Phone"]),
                        Email = Convert.IsDBNull(dr["Email"]) ? string.Empty : Convert.ToString(dr["Email"])
                    });
                }
            }

            return customerList;
        }

        public static List<UserDetails> MapUserDataTableToCollection(this DataTable dt)
        {
            List<UserDetails> userList = new List<UserDetails>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    

                    userList.Add(new UserDetails
                    {
                         ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                         UserID = Convert.IsDBNull(dr["UserID"]) ? string.Empty : Convert.ToString(dr["UserID"]),
                        RoleDescription = Convert.IsDBNull(dr["RoleDescription"]) ? string.Empty : Convert.ToString(dr["RoleDescription"]),
                        ManagerName = Convert.IsDBNull(dr["ManagerName"]) ? string.Empty : Convert.ToString(dr["ManagerName"])
                    });
                }
            }

            return userList;
        }
    }
}