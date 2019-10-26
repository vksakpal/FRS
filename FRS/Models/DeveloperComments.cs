using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.Models
{
    public class DeveloperComments
    {
        public int FaultId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
    }
}