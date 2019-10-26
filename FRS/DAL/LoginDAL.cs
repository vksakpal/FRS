using FRS.Models;
using FRS.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace FRS.DAL
{
    public class LoginDAL
    {
        public UserDetails Login(LoginDetails logindetails)
        {
            UserDetails ud = null;

            if(logindetails == null)
            {
                return ud;
            }
            
            try
            {                
                DataTable dt = new DataTable();                
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(@"Data Source=|DataDirectory|\FRS.db;Version=3;New=True;Compress=True;"))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM TUserDetails WHERE UserID = '{logindetails.UserID}' AND UserPassword = '{logindetails.Password}'";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    ud = dt.MapUserDetailsDataTable();

                }
            }
            catch (Exception ex)
            {

            }
            return ud;
        }
    }
}