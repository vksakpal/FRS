using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRS.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}