using FRS.Mapper;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace FRS.DAL
{
    public class FaultManagerDAL
    {
        public List<FaultDetails> GetFaultList(string status, int roleId, int userId)
        {
            List<FaultDetails> faultDetailsList = new List<FaultDetails>();
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SQLiteCommand cmd = con.CreateCommand();

                    StringBuilder sb = new StringBuilder();
                    string cmdText = string.Empty;
                    if (roleId == 4)
                    {
                        sb.Append("SELECT");
                        sb.Append(" FD.FaultID, FD.ProductID, P.ProductName, FD.StatusID, S.StatusDescription, UD.UserID, FD.AssignedUserID, FD.FaultReportingDate,");
                        sb.Append(" FD.CustomerID, FD.FaultResolvedDate, FD.FaultTypeID, FT.FaultTypeDescription, FD.FaultDescription, C.Name as CustomerName,");
                        sb.Append(" FD.FaultPriority, C.Phone, C.Email");
                        sb.Append(" FROM TFaultDetails FD");
                        sb.Append(" LEFT JOIN TPRODUCT P ON FD.ProductID = P.ProductID");
                        sb.Append(" LEFT JOIN TStatus S on FD.StatusID = S.StatusID");
                        sb.Append(" LEFT JOIN TUserDetails UD on FD.AssignedUserID = UD.ID");
                        sb.Append(" LEFT JOIN TFaultTypes FT on FD.FaultTypeID = FT.FaultTypeId");
                        sb.Append(" LEFT JOIN TCustomer C on FD.CustomerID = C.CustomerID");

                        sb.Append($" WHERE FD.AssignedUserID = {userId} AND FD.StatusID IN ({status})");


                    }
                    else if (roleId == 3)
                    {
                        sb.Append("SELECT");
                        sb.Append(" FD.FaultID, FD.ProductID, P.ProductName, FD.StatusID, S.StatusDescription, UD.UserID, FD.AssignedUserID, FD.FaultReportingDate,");
                        sb.Append(" FD.CustomerID, FD.FaultResolvedDate, FD.FaultTypeID, FT.FaultTypeDescription, FD.FaultDescription, C.Name as CustomerName,");
                        sb.Append(" FD.FaultPriority, C.Phone, C.Email");
                        sb.Append(" FROM TFaultDetails FD");
                        sb.Append(" LEFT JOIN TPRODUCT P ON FD.ProductID = P.ProductID");
                        sb.Append(" LEFT JOIN TStatus S on FD.StatusID = S.StatusID");
                        sb.Append(" LEFT JOIN TUserDetails UD on FD.AssignedUserID = UD.ID");
                        sb.Append(" LEFT JOIN TFaultTypes FT on FD.FaultTypeID = FT.FaultTypeId");
                        sb.Append(" LEFT JOIN TCustomer C on FD.CustomerID = C.CustomerID");
                        
                            sb.Append($" WHERE FD.StatusID IN({status}) AND");
                      

                        sb.Append($" (FD.AssignedUserID IS NULL OR FD.AssignedUserID IN(Select ID from TUserDetails where ManagerID = {userId}))");
                    }
                    else
                    {
                        sb.Append("SELECT");
                        sb.Append(" FD.FaultID, FD.ProductID, P.ProductName, FD.StatusID, S.StatusDescription, UD.UserID, FD.AssignedUserID, FD.FaultReportingDate,");
                        sb.Append(" FD.CustomerID, FD.FaultResolvedDate, FD.FaultTypeID, FT.FaultTypeDescription, FD.FaultDescription, C.Name as CustomerName,");
                        sb.Append(" FD.FaultPriority, C.Phone, C.Email");
                        sb.Append(" FROM TFaultDetails FD");
                        sb.Append(" LEFT JOIN TPRODUCT P ON FD.ProductID = P.ProductID");
                        sb.Append(" LEFT JOIN TStatus S on FD.StatusID = S.StatusID");
                        sb.Append(" LEFT JOIN TUserDetails UD on FD.AssignedUserID = UD.ID");
                        sb.Append(" LEFT JOIN TFaultTypes FT on FD.FaultTypeID = FT.FaultTypeId");
                        sb.Append(" LEFT JOIN TCustomer C on FD.CustomerID = C.CustomerID");
                        
                            sb.Append($" WHERE FD.StatusID IN({status})");
                      
                    }
                    cmd.CommandText = sb.ToString();
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
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
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    int userDetailsId = 0;
                    int customerId = 0;
                    DataTable dt = new DataTable();
                    StringBuilder sb = new StringBuilder();
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();

                        sb.Append($" SELECT CustomerID FROM TCustomer WHERE Phone = {faultDetails.CustomerInfo.Phone} AND Email = '{faultDetails.CustomerInfo.Email}' ");
                        cmd.CommandText = sb.ToString();
                        sqlite_conn.Open();
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                        ad.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            userDetailsId = Convert.ToInt32(dt.Rows[0]["UserDetailsId"]);
                        }
                        else
                        {
                            sb.Clear();
                            cmd.CommandText = $"INSERT INTO TUserDetails(UserID, UserPassword, RoleID,ManagerID) VALUES('{faultDetails.CustomerInfo.Name}','{faultDetails.CustomerInfo.Name}',2,NULL)";
                            cmd.ExecuteNonQuery();
                            userDetailsId = (int)sqlite_conn.LastInsertRowId;
                            cmd.CommandText = $"INSERT INTO TCustomer(Name, Phone, Email, UserDetailsId) VALUES('{faultDetails.CustomerInfo.Name}',{faultDetails.CustomerInfo.Phone},'{faultDetails.CustomerInfo.Email}',{userDetailsId})";
                            cmd.ExecuteNonQuery();
                            customerId = (int)sqlite_conn.LastInsertRowId;
                            
                        }
                    sb.Clear();
                    sb.Append(" INSERT INTO TFaultDetails (ProductId, StatusID, AssignedUserID, FaultReportingDate,CustomerID,FaultResolvedDate,FaultTypeID,FaultDescription,FaultPriority)  ");
                    sb.Append($" VALUES ({faultDetails.ProductID}, {faultDetails.StatusID}, NULL, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', {customerId},NULL, {faultDetails.FaultTypeID}, '{faultDetails.FaultDescription}',{faultDetails.FaultPriorityID}); ");
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                    faultId = (int)sqlite_conn.LastInsertRowId;

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
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    if (String.IsNullOrEmpty(faultDetails.FaultResolvedDate))
                    {
                        cmd.CommandText = $"UPDATE TFaultDetails SET ProductId = {faultDetails.ProductID}, StatusID = {faultDetails.StatusID}, FaultResolvedDate = NULL, FaultTypeID = {faultDetails.FaultTypeID}, FaultDescription = '{faultDetails.FaultDescription}', FaultPriority = {faultDetails.FaultPriorityID} where FaultID = {faultDetails.FaultID}";
                    }
                    else
                    {
                        cmd.CommandText = $"UPDATE TFaultDetails SET ProductId = {faultDetails.ProductID}, StatusID = {faultDetails.StatusID}, FaultResolvedDate = '{Convert.ToDateTime(faultDetails.FaultResolvedDate):yyyy-MM-dd HH:mm:ss}', FaultTypeID = {faultDetails.FaultTypeID}, FaultDescription = '{faultDetails.FaultDescription}', FaultPriority = {faultDetails.FaultPriorityID} where FaultID = {faultDetails.FaultID}";
                    }
                    sqlite_conn.Open();
                    cmd.ExecuteNonQuery();
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


                using (SQLiteConnection con = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SQLiteCommand cmd = con.CreateCommand();

                    StringBuilder sb = new StringBuilder();
                    string cmdText = string.Empty;

                    sb.Append("SELECT");
                    sb.Append(" FD.FaultID,FD.ProductID, P.ProductName, FD.StatusID ,S.StatusDescription, UD.UserID, FD.AssignedUserID, FD.FaultReportingDate,");
                    sb.Append(" FD.CustomerID, FD.FaultResolvedDate, FD.FaultTypeID,FT.FaultTypeDescription,FD.FaultDescription , C.Name as CustomerName,");
                    sb.Append(" C.Email, C.Phone,FD.FaultPriority");
                    sb.Append(" FROM TFaultDetails FD");
                    sb.Append(" LEFT JOIN TPRODUCT P ON FD.ProductID = P.ProductID");
                    sb.Append(" LEFT JOIN TStatus S on FD.StatusID = S.StatusID");
                    sb.Append(" LEFT JOIN TUserDetails UD on FD.AssignedUserID = UD.ID");
                    sb.Append(" LEFT JOIN TFaultTypes FT on FD.FaultTypeID = FT.FaultTypeId");
                    sb.Append(" LEFT JOIN TCustomer C on FD.CustomerID = C.CustomerID");
                    sb.Append($" where FD.FaultId = {faultId}");

                    cmd.CommandText = sb.ToString();
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    faultDetailsList = dt.MapFaultDetailsDataTableToCollection();
                }
            }
            catch (Exception ex)
            {

            }
            return faultDetailsList.FirstOrDefault();
        }

        public bool AssignFault(int faultId, int userId)
        {
            bool success = false;
            try
            {
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"Update TFaultDetails SET AssignedUserID = {userId} where FaultID = {faultId}";
                    sqlite_conn.Open();
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public bool AddDeveloperComment(int faultId, int userId, string comment, string faultResolveDate, int statusId)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    SQLiteCommand cmd = sqlite_conn.CreateCommand();
                    cmd.CommandText = $"INSERT INTO TDeveloperComments(FaultID, UserID, Comments,CreatedDate) VALUES({faultId},{userId},'{comment}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                    sqlite_conn.Open();
                    int countDevComment = cmd.ExecuteNonQuery();

                    if (string.IsNullOrEmpty(faultResolveDate))
                    {
                        cmd.CommandText = $"UPDATE TFaultDetails SET StatusID = {statusId}, FaultResolvedDate=NULL WHERE FaultID={faultId}";
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(faultResolveDate);
                        cmd.CommandText = $"UPDATE TFaultDetails SET StatusID = {statusId}, FaultResolvedDate='{dt:yyyy-MM-dd HH:mm:ss}' WHERE FaultID={faultId}";
                    }

                    int countUpdate = cmd.ExecuteNonQuery();
                    if (countUpdate > 0 && countDevComment > 0)
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

        public List<DeveloperComments> GetDeveloperComments(int faultId)
        {
            List<DeveloperComments> developerCommentList = new List<DeveloperComments>();
            try
            {


                using (SQLiteConnection con = new SQLiteConnection(ConfigurationManager.ConnectionStrings["FRSConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    SQLiteCommand cmd = con.CreateCommand();

                    StringBuilder sb = new StringBuilder();
                    string cmdText = string.Empty;

                    sb.Append("SELECT");
                    sb.Append(" TDC.FaultID,TDC.UserID, TDC.Comments, TDC.CreatedDate ,UD.UserID AS UserName");
                    sb.Append(" FROM TDeveloperComments TDC");
                    sb.Append(" INNER JOIN TUserDetails UD on TDC.UserID = UD.ID");
                    sb.Append($" where TDC.FaultID = {faultId}");
                    sb.Append(" ORDER BY TDC.CreatedDate DESC");

                    cmd.CommandText = sb.ToString();
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                    developerCommentList = dt.MapDeveloperCommentsDataTableToCollection();
                }
            }
            catch (Exception ex)
            {

            }
            return developerCommentList;
        }


    }
}