using FRS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FRS.Mapper
{
    public static class UserMapperExtension
    {
        public static UserDetails MapUserDetailsDataTable(this DataTable dt)
        {
            UserDetails UsertDetails = null ;
            if(dt!= null && dt.Rows.Count > 0)
            {
                return new UserDetails()
                {
                    UserID = Convert.ToString(dt.Rows[0]["UserID"]),
                    ID = Convert.ToInt16(dt.Rows[0]["ID"]),
                    RoleID = Convert.ToInt16(dt.Rows[0]["RoleID"]),
                };
            }

            return UsertDetails;
        }
    }
}