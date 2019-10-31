using FRS.DAL;
using FRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.Business_Layer
{
    public class FaultManagerBAL
    {
        public List<FaultDetails> GetFaultList(string status, int roleId, int userID)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.GetFaultList(status, roleId, userID);
        }

        public FaultDetails GetFaultListByFaultId(int faultId)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.GetFaultListByFaultID(faultId);
        }

        public int AddFaultDetails(FaultDetails faultDetails,UserDetails userDetails)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.AddFaultDetails(faultDetails, userDetails);
        }

        public void UpdateFaultDetails(FaultDetails faultDetails, int roleId)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            dal.UpdateFaultDetails(faultDetails, roleId);
        }

        public bool AssignFault(int faultId, int userId)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.AssignFault(faultId, userId);
        }

        public bool AddDeveloperComment(int faultId, int userId, string comment, string faultResolvedDate, int statusId)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.AddDeveloperComment(faultId, userId, comment, faultResolvedDate, statusId );
        }

        public List<DeveloperComments> GetDeveloperComments(int faultId)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.GetDeveloperComments(faultId);
        }
    }
}