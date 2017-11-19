using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESR_Project.Models;

namespace ESR_Project.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public ActionResult loginPage()
        {
            Session["admin090ESRAdmin"] = null;
            Session.Clear();
            Session.Abandon();
            try
            {
                ViewBag.ErrorMessage = (string)TempData["ErrorMessage"];
            }
            catch (Exception ee) { }

            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();

        }
        public ActionResult TicketBookings()
        {
            return View();
        }

        public ActionResult adminPage(AdminClass a)//adminlogin a
        {
            //Admin loginAdmin = new Admin();
           // if (Session["admin090JYMAdmin"] == null)
           // {
               // try
               // {
              //      loginAdmin = obj.Admins.First(x => x.UserName.Equals(a.UserName) && x.Password.Equals(a.Password));
              //  }
              //  catch (Exception e) { /*TempData["ErrorMessage"] = "ErrorMessage"; */}
              //  if (loginAdmin.UserName!= null && loginAdmin.Password != null)
              //  {
             //       //Session["admin090JYMAdmin"] = "RandomValue";
            //        return View();
            //    }
            //    else
             //       return RedirectToAction("loginPage", "Admin");
           // }
           // else
            //{
               return View();
            //}
        }

        //Partial Views

        //FirstPage to Open 
        //FirstPage to Open 

        public ActionResult firstPageToOpen()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();

            return PartialView();
        }
        public ActionResult Analaytics()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();

            return PartialView();
        }
        public ActionResult ContestDetails()
        {
            return PartialView();
        }
        public ActionResult CareerDetails()
        {
            return PartialView();
        }
        public ActionResult DealDetails()
        {
            return PartialView();
        }
        public ActionResult ClientDetails()
        {
            return PartialView();
        }
        public ActionResult EventDetails()
        {
            return PartialView();
        }
        public ActionResult NewDetails()
        {
            return PartialView();
        }
        public ActionResult PakageDetails()
        {
            return PartialView();
        }
        public ActionResult RestaurantDetails()
        {
            return PartialView();
        }
        public ActionResult ServiceDetails()
        {
            return PartialView();
        }
        public ActionResult TagDetails()
        {
            return PartialView();
        }
        public ActionResult UserDetails()
        {
            return PartialView();
        }
        public ActionResult AdminDetails()
        {
            return PartialView();
        }
        public ActionResult Messages()
        {
            return PartialView();
        }

        public ActionResult CareerUpdates()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult ContestUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult AdminUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult ClientUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult EventUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult NewUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult PakageUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult RestaurantUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult ServiceUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult TagUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult DealUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }
        public ActionResult UserUpdate()
        {
            var count1 = obj.Users.Where(x => x.Status != 0).ToList();
            var count2 = obj.Clients.Where(x => x.Status != 0).ToList();
            var count3 = obj.Pakages.Where(x => x.Status != 0).ToList();
            var count4 = obj.Events.Where(x => x.Status != 0).ToList();

            ViewBag.UsersCount = count1.Count();
            ViewBag.ClientsCount = count2.Count();
            ViewBag.PakagesCount = count3.Count();
            ViewBag.EventsCount = count4.Count();
            return PartialView();
        }

    }
}