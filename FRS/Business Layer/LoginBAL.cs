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
            DataTable dt = dal.Login(loginDetails);
            if (dt.Rows.Count > 0)
            {
                return new UserDetails()
                {
                    UserID = Convert.ToString(dt.Rows[0]["UserID"]),
                    ID = Convert.ToInt16(dt.Rows[0]["ID"]),
                    RoleID = Convert.ToInt16(dt.Rows[0]["RoleID"]),
                };

            }

            return null;
        }
    }
}