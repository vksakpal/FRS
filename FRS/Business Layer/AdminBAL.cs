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

        public List<Customer> GetCustomerList()
        {
            AdminDAL dal = new AdminDAL();
            return dal.GetCustomerList();
        }

        public List<UserDetails> GetUserList()
        {
            AdminDAL dal = new AdminDAL();
            return dal.GetUserList();
        }

    }
}