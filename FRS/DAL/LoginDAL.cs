using FRS.Models;
using FRS.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FRS.DAL
{
    public class LoginDAL
    {
        public UserDetails Login(LoginDetails logindetails)
        {
            UserDetails ud = null;
            
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Login", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@userId", logindetails.UserID);
                cmd.Parameters.AddWithValue("@password", logindetails.Password);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                ud = dt.MapUserDetailsDataTable();
            }
            catch(Exception ex)
            {

            }
            return ud;
        }
    }
}