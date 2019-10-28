using FRS.DAL;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.Business_Layer
{
    public class AdminBAL
    {
        public bool AddUserDetails(UserDetails userDetails)
        {
            AdminDAL dal = new AdminDAL();
            return dal.AddUserDetails(userDetails);
        }

        public bool AddCustomer(Customer customer)
        {
            AdminDAL dal = new AdminDAL();
            return dal.AddCustomer(customer);
        }

    }
}