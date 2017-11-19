using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESR_Project.Models
{
    public class TicketBook
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public int cnic { get; set; }

        public int tickets { get; set; }
        public int eventId { get; set; }
        public int status { get; set; }
        public int count { get; set; }
    }
}