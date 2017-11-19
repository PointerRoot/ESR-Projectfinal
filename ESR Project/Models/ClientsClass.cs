using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESR_Project.Models
{
    public class ClientsClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Testimonial { get; set; }
        public int PakageId { get; set; }
        public int Next { get; set; }
        public int Prev { get; set; }
        public string NumberOfShowing { get; set; }
        public int Count { get; set; }
        public string AddDate { get; set; }
        public string Image { get; set; }
    }
}