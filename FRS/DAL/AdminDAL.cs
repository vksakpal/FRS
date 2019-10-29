using FRS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace FRS.DAL
{
    public class AdminDAL
    {
        public bool AddUserDetails(UserDetails userDetails)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"INSERT INTO TUserDetails(UserID, UserPassword, RoleID,ManagerID) VALUES('{userDetails.UserID}','{userDetails.UserPassword}',{userDetails.RoleID},{userDetails.ManagerId})";
                    sqlite_conn.Open();
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0 )
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool AddCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    int userID = 0;
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    sqlite_conn.Open();
                    cmd.CommandText = $"INSERT INTO TUserDetails(UserID, UserPassword, RoleID,ManagerID) VALUES('{customer.Name}','{customer.Name}',2,NULL)";
                    cmd.ExecuteNonQuery();
                    userID = (int)sqlite_conn.LastInsertRowId;
                    cmd.CommandText = $"INSERT INTO TCustomer(Name, Phone, Email, UserDetailsId) VALUES('{customer.Name}',{customer.Phone},'{customer.Email}',{userID})";
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}