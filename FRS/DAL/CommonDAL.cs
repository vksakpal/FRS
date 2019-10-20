using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("GetListOfProducts", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
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
            List<SelectListItem> productList = new List<SelectListItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("GetListOfFaultTypes", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    productList = dt.MapFaultTypesDataTableToCollection();
                }

            }
            catch (Exception ex)
            {

            }
            return productList;
        }

        public List<SelectListItem> GetFaultStatusList()
        {
            List<SelectListItem> statusList = new List<SelectListItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("GetListOfFaultStatus", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    statusList = dt.MapFaultStatusDataTableToCollection();
                }

            }
            catch (Exception ex)
            {

            }
            return statusList;
        }
    }
}