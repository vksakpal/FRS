using FRS.Mapper;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FRS.DAL
{
    public class FaultManagerDAL
    {
        public List<FaultDetails> GetFaultList(int status,int roleId, int userId)
        {
            List<FaultDetails> faultDetailsList = new List<FaultDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("GetFaultDetails", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@roleID", roleId);
                    cmd.Parameters.AddWithValue("@userID", userId);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    faultDetailsList = dt.MapFaultDetailsDataTableToCollection();
                }
               
            }
            catch (Exception ex)
            {

            }
            return faultDetailsList;
        }

        public int AddFaultDetails(FaultDetails faultDetails)
        {
            int faultId = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("AddFaultDetails", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@productId", faultDetails.ProductID);
                    cmd.Parameters.AddWithValue("@statusID", faultDetails.StatusID);
                    cmd.Parameters.AddWithValue("@faultReportingDate", DateTime.Now);                    
                    cmd.Parameters.AddWithValue("@faultTypeID", faultDetails.FaultTypeID);
                    cmd.Parameters.AddWithValue("@faultDescription", faultDetails.FaultDescription);                    
                    cmd.Parameters.AddWithValue("@CustomerName", faultDetails.CustomerInfo.Name);
                    cmd.Parameters.AddWithValue("@Phone", faultDetails.CustomerInfo.Phone);
                    cmd.Parameters.AddWithValue("@Email", faultDetails.CustomerInfo.Email);
                    cmd.Parameters.AddWithValue("@faultPriority", faultDetails.FaultPriorityID);
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    faultId = Convert.ToInt16(cmd.Parameters["@id"].Value);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return faultId;
        }

        public void UpdateFaultDetails(FaultDetails faultDetails)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("UpdateFaultDetails", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@faultID", faultDetails.ProductID);
                    cmd.Parameters.AddWithValue("@productId", faultDetails.ProductID);
                    cmd.Parameters.AddWithValue("@statusID", faultDetails.StatusID);
                    cmd.Parameters.AddWithValue("@faultResolvedDate", faultDetails.FaultResolvedDate == null ? (object)DBNull.Value : Convert.ToDateTime(faultDetails.FaultResolvedDate));
                    cmd.Parameters.AddWithValue("@faultTypeID", faultDetails.FaultTypeID);
                    cmd.Parameters.AddWithValue("@faultDescription", faultDetails.FaultDescription);
                    cmd.Parameters.AddWithValue("@faultPriority", faultDetails.FaultPriorityID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaultDetails GetFaultListByFaultID(int faultId)
        {
            List<FaultDetails> faultDetailsList = new List<FaultDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("FaultDetailsByFaultID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@faultId", faultId);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    faultDetailsList = dt.MapFaultDetailsDataTableToCollection();
                }

            }
            catch (Exception ex)
            {

            }
            return faultDetailsList.FirstOrDefault();
        }
    }
}