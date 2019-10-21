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
        public List<FaultDetails> GetFaultList(int status, int roleId, int userID)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.GetFaultList(status,roleId,userID);
        }

        public FaultDetails GetFaultListByFaultId(int faultId)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.GetFaultListByFaultID(faultId);
        }

        public int AddFaultDetails(FaultDetails faultDetails)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            return dal.AddFaultDetails(faultDetails);
        }

        public void UpdateFaultDetails(FaultDetails faultDetails)
        {
            FaultManagerDAL dal = new FaultManagerDAL();
            dal.UpdateFaultDetails(faultDetails);
        }
    }
}