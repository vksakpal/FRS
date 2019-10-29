using FRS.Mapper;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
                    cmd.CommandText = $"INSERT INTO TUserDetails(UserID, UserPassword, RoleID,ManagerID) VALUES('{userDetails.UserID}','{userDetails.UserID}',{userDetails.RoleID},{userDetails.ManagerId})";
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
                    cmd.CommandText = $"INSERT INTO TUserDetails(UserID, UserPassword, RoleID,ManagerID) VALUES('{customer.FirstName}','{customer.FirstName}',2,NULL)";
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

        public List<Customer> GetCustomerList()
        {
            List<Customer> CustomerList = new List<Customer>();
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM TCustomer";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    CustomerList = dt.MapCustomerDataTableToCollection();

                }
            }
            catch (Exception ex)
            {

            }
            return CustomerList;
        }

        public List<UserDetails> GetUserList()
        {
            List<UserDetails> UserList = new List<UserDetails>();
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT TUD.ID,TUD.UserID, TR.RoleDescription, TUDM.UserID AS ManagerName FROM TUserDetails TUD");
                    sb.Append(" LEFT JOIN TRoles TR ON TUD.RoleID = TR.RoleID");
                    sb.Append(" LEFT JOIN TUserDetails TUDM ON  TUD.ManagerID = TUDM.ID AND TR.RoleDescription ='Developer'");

                    cmd.CommandText = sb.ToString();
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    UserList = dt.MapUserDataTableToCollection();

                }
            }
            catch (Exception ex)
            {

            }
            return UserList;
        }
    }
}