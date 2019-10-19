using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.Models
{
    public class UserDetails
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int RoleID { get; set; }
    }
}