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
        public List<FaultDetails> GetFaultList(int status)
        {
            List<FaultDetails> faultDetailsList = new List<FaultDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("FaultDetailsByStatus", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@status", status);
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
    }
}