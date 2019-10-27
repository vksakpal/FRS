using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FRS.Mapper;

namespace FRS.DAL
{
    public class CommonDAL
    {
        public List<SelectListItem> GetProductList()
        {
            List<SelectListItem> productList = new List<SelectListItem>();
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM TProduct";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    productList = dt.MapProductsDataTableToCollection();

                }
            }
            catch (Exception ex)
            {

            }
            return productList;
        }

        public List<SelectListItem> GetFaultTypesList()
        {
            List<SelectListItem> faultTypeList = new List<SelectListItem>();
            try
            {

                DataTable dt = new DataTable();
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM TFaultTypes";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    faultTypeList = dt.MapFaultTypesDataTableToCollection();

                }               

            }
            catch (Exception ex)
            {

            }
            return faultTypeList;
        }

        public List<SelectListItem> GetFaultStatusList()
        {
            List<SelectListItem> statusList = new List<SelectListItem>();
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM TStatus";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    statusList = dt.MapFaultStatusDataTableToCollection();

                }
            }
            catch (Exception ex)
            {

            }
            return statusList;
        }

        public List<SelectListItem> GetListOfDevelopersByManagerId(int managerId)
        {
            List<SelectListItem> developerList = new List<SelectListItem>();
            try
            {

                DataTable dt = new DataTable();
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM TUserDetails WHERE ManagerID = {managerId}";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    developerList = dt.MapDeveloperDataTableToCollection();

                }
            }
            catch (Exception ex)
            {

            }
            return developerList;
        }

    }
}