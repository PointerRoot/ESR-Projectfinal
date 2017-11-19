using ESR_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Mail;
using System.Web.Mail;



namespace ESR_Project.Controllers
{
    public class EventsController : ApiController
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public List<ESR> GetAllClassesNames()
        {
            List<ESR> li = new List<ESR>();

            var All = obj.Events.Where(x => x.Status != 0).ToList();

            foreach (var details in All)
            {
                ESR temp = new ESR();
                temp.Name = details.Name;
                li.Add(temp);
            }
            return li;
        }
        public List<Emails> GetAllMails( int id)
        {
            List<Emails> li = new List<Emails>();
           
            
                var emails = obj.Emails.Where(x => x.Id == id).ToList();

                foreach (var details in emails)
                {
                    Emails temp = new Emails();
                    temp.Body = details.body;
                    temp.Subject = details.subject;
                    temp.Id = details.Id;
                    li.Add(temp);
                }

            
            return li;
        }

        public List<EventsClass> GetAllEvents(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Events.Count();
            int max = (skip + ShowRecords);
            if (max > AllRecords)
            {
                max = AllRecords;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + AllRecords.ToString();

            List<EventsClass> li = new List<EventsClass>();

            var AllEvents = obj.Events.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllEvents.Count();
            foreach (var details in AllEvents)
            {
                EventsClass temp = new EventsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.Place = details.Plcae;
                temp.Date = details.Date;
                temp.Time = details.Time;
                try
                {
                    temp.TicketPrice = details.TicketPrice.Value;
                }
                catch (Exception e) { temp.TicketPrice = 0; }
                try
                {
                    temp.Discount = details.Discount.Value;
                }
                catch (Exception e) { temp.Discount = 0; }
                temp.Count = count;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
            }
            return li;
        }
        public List<TicketBook> GetAllTickets()
        {
            List<TicketBook> li = new List<TicketBook>();

            var AllEvents = obj.TicketBookings.Where(x => x.status != 0).ToList();
            var count = AllEvents.Count();
            foreach (var details in AllEvents)
            {
                TicketBook temp = new TicketBook();
                temp.id = details.Id;
                temp.username = details.name;
                temp.tickets = int.Parse(details.tickets.ToString());
                temp.cnic= int.Parse(details.cnic.ToString());
                temp.eventId = int.Parse(details.eventId.ToString());
                temp.email = details.email;
                temp.status = int.Parse(details.status.ToString());
                
                temp.count = count;
             
                li.Add(temp);
            }
            return li;
        }

        public List<EventsClass> GetAllEventsImageById(int id)
        {
            List<EventsClass> li = new List<EventsClass>();

            var AllEvents = obj.EventImages.Where(x => x.Status != 0 && x.EventId.Equals(id)).ToList();
            var count = AllEvents.Count();
            foreach (var details in AllEvents)
            {
                EventsClass temp = new EventsClass();
                temp.Id = details.Id;
                temp.Image = details.Images;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<EventsClass> GetAllEventsByName(string id)
        {
            List<EventsClass> li = new List<EventsClass>();

            var AllEvents = obj.Events.Where(x => x.Status != 0 && x.Name.Equals(id)).ToList();
            var count = AllEvents.Count();
            foreach (var details in AllEvents)
            {
                EventsClass temp = new EventsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.Place = details.Plcae;
                temp.Date = details.Date;
                temp.Time = details.Time;
                
                try
                {
                    temp.TicketPrice = details.TicketPrice.Value;
                }
                catch (Exception e) { temp.TicketPrice = 0; }
                try
                {
                    temp.Discount = details.Discount.Value;
                }
                catch (Exception e) { temp.Discount = 0; }
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<EventsClass> GetAllEventsById(int id)
        {
            List<EventsClass> li = new List<EventsClass>();

            var AllEvents = obj.Events.Where(x => x.Status != 0 && x.Id.Equals(id)).ToList();
            var count = AllEvents.Count();
            foreach (var details in AllEvents)
            {
                EventsClass temp = new EventsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.Place = details.Plcae;
                temp.Date = details.Date;
                temp.Time = details.Time;
                try
                {
                    temp.TicketPrice = details.TicketPrice.Value;
                }
                catch (Exception e) { temp.TicketPrice = 0; }
                try
                {
                    temp.Discount = details.Discount.Value;
                }
                catch (Exception e) { temp.Discount = 0; }
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }
        public List<TicketBook> GetBookingsById(int id)
        {
            List<TicketBook> li = new List<TicketBook>();

            var AllEvents = obj.TicketBookings.Where(x => x.status != 0 && x.Id.Equals(id)).ToList();
            var count = AllEvents.Count();
            foreach (var details in AllEvents)
            {
                TicketBook temp = new TicketBook();
                temp.id= details.Id;
                temp.username= details.name;
                temp.cnic = int.Parse(details.cnic.ToString());
                temp.email = details.email;
                temp.tickets = int.Parse(details.tickets.ToString());
               
               
                
                temp.count = count;
                li.Add(temp);
            }
            return li;
        }

        public string PostSendMessage()
        {
            var check = "true";
            string emails = HttpContext.Current.Request["Emails"];
            string message = HttpContext.Current.Request["Message"];
            string subject = HttpContext.Current.Request["Subject"];
            string id = HttpContext.Current.Request["Id"];

           
            if (!emails.Contains(','))
            {
                emails += ',';
            }
            if (!id.Contains(','))
            {
                id += ',';
            }
            string[] email = emails.Split(',');
            string[] ids = id.Split(',');
            for (int i = 0; i < email.Length - 1; i++)
            {
                
                try
                {
                    var sender = new MailAddress("");
                    var rec = new MailAddress(email[i]);


                    var pass = "";
                    var body = message;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sender.Address, pass)
                    };
                    System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage(sender, rec);
                    msgMail.IsBodyHtml = false;
                    msgMail.Subject = subject;
                    msgMail.Body = body;
                    smtp.Send(msgMail);

                    TicketBooking events = obj.TicketBookings.First(x => x.Id.Equals(int.Parse(id[i].ToString())));
                    events.Id = int.Parse(id[i].ToString());
                    events.status = 2;
                    obj.SubmitChanges();




                }
                catch (Exception)
                {
                    check = "false";
                   
                }
            }
            return check;
        }

        public string PostMail(int id)
        {
            string check = "";

          

            string emails = HttpContext.Current.Request["Emails"];
            string message = HttpContext.Current.Request["Message"];
            string subject = HttpContext.Current.Request["Subject"];
            int Id= int.Parse(HttpContext.Current.Request["Id"]);

            try
            {
                Email e = obj.Emails.First(x=>Id.Equals(x.Id));
                e.subject = subject;
                e.body = message;

                
                obj.SubmitChanges();
                check = "true";


            }
            catch (Exception e)
            {
                check = "false";

            }


            return check;
        }
        public int PostEvent(EventsClass EC)
        {
            int check = 0;
            try
            {
                Event events = new Event();
                events.Name = EC.Name;
                events.Plcae = EC.Place;
                events.Date = EC.Date;
                events.Time = EC.Time;
                events.AddDate = DateTime.Now.ToShortDateString();
                events.TicketPrice = EC.TicketPrice;
                events.Discount = EC.Discount;
                events.Status = 1;
                obj.Events.InsertOnSubmit(events);
                obj.SubmitChanges();
                check = events.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public string BookTicket()
        {
            string check = "true";

            string emails = HttpContext.Current.Request["Email"];
            string username = HttpContext.Current.Request["Username"];
            int cnic = int.Parse(HttpContext.Current.Request["Cnic"]);
            int tickets = int.Parse(HttpContext.Current.Request["Tickets"]);
            int eventId = int.Parse(HttpContext.Current.Request["EventId"]);
            try
            {
                TicketBooking events = new TicketBooking();
                events.name = username;
                events.eventId = (eventId);
                events.cnic = cnic;
                events.tickets = tickets;
                events.status = 1;
                events.email = emails;
                
                obj.TicketBookings.InsertOnSubmit(events);
                obj.SubmitChanges();
                
            }
            catch (Exception e) { check = "false"; }
            return check;

        }
       
        public int UpdateEvent(EventsClass EC)
        {
            int check = 0;
            try
            {
                Event events = obj.Events.First(x=>x.Id.Equals(EC.Id));
                events.Name = EC.Name;
                events.Plcae = EC.Place;
                if (EC.Date != "")
                {
                    events.Date = EC.Date;
                }
                events.Time = EC.Time;
                events.TicketPrice = EC.TicketPrice;
                events.Discount = EC.Discount;
                events.Status = 1;
                
                obj.SubmitChanges();
                check = events.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int DeleteEvent(int id)
        {
            int check = 0;
            try
            {
                Event events = obj.Events.First(x => x.Id.Equals(id));
                events.Status = 0;
                obj.SubmitChanges();
                check = 1;
            }
            catch (Exception e) { }

            return check;
        }
    }
}
