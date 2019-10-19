using FRS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FRS.DAL
{
    public class LoginDAL
    {
        public DataTable Login(LoginDetails logindetails)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Administrator\Desktop\FRS\src\FRS\App_Data\FRS.mdf;Integrated Security=True;User Instance=True");
                SqlCommand cmd = new SqlCommand("Login", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@userId", logindetails.UserID);
                cmd.Parameters.AddWithValue("@password", logindetails.Password);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {

            }
            return dt;
        }
    }
}