using FRS.DAL;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FRS.Business_Layer
{
    public class LoginBAL
    {

        public UserDetails Login(LoginDetails loginDetails)
        {
            LoginDAL dal = new LoginDAL();
            return dal.Login(loginDetails);
        }
    }
}