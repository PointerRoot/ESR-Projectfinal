using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESR_Project.Models
{
    public class AdminClass
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string AddDate { get; set; }
        public int Count { get; set; }
        public string NumberOfShowing { get; set; }
    }
}