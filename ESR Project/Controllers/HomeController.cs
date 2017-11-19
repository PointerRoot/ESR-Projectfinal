using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ESR_Project.Models;

using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Mail;
using Microsoft.AspNet.Identity;

namespace ESR_Project.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        private ApplicationUserManager _userManager;
        public List<string> profile = new List<string>();
        public List<string> comment_profile = new List<string>();


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<ActionResult> Index()
        {

            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;

           

            try
            {
                ViewBag.ErrorMessage = (string)TempData["ErrorMessage"];
            }
            catch (Exception ee) { }
            try
            {
                HttpCookie authCookie = Request.Cookies["TraineeHellodjfdshfsdanfkjdsfsfwgbhwvifbwsdjwiufdn"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;

                if (!expired)
                    ViewBag.UserId = CookieId;
            }
            catch (Exception e) { }

            var value = obj.AccessTokens.First();
            var accessToken = value.AccessToken1;



            if (accessToken == "")
            {
                return View("Index2");
            }
            //curly braces
            string s = accessToken;
            string url = String.Format("https://graph.facebook.com/me?fields=id,name,groups{{feed.limit(1000){{from,message,story,created_time,attachments{{description,type,url,title,media,target}},likes{{pic}},comments}}}}&access_token={0}", accessToken);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            //curly braces

            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string result = await reader.ReadToEndAsync();
                    dynamic jsonObj = System.Web.Helpers.Json.Decode(result);

                    Models.SocialMedia.SocialMedia.posts posts = new Models.SocialMedia.SocialMedia.posts(jsonObj);

                    List<string> pictures = new List<string>();
                    List<string> comments_pictures = new List<string>();


                    foreach (var post in posts.feed.data)
                    {
                        if (post.comments != null)
                        {
                            foreach (var id in post.comments.data)
                            {
                                comments_pictures.Add(id.from.id);
                            }
                        }
                    }

                    foreach (var post in posts.feed.data)
                    {
                        pictures.Add(post.From.id);
                    }

                    foreach (var id in comments_pictures)
                    {
                        string url2 = String.Format("https://graph.facebook.com/" + id + "?fields=picture&access_token={0}", accessToken);
                        HttpWebRequest request2 = WebRequest.Create(url2) as HttpWebRequest;
                        request2.Method = "GET";

                        using (HttpWebResponse response2 = await request2.GetResponseAsync() as HttpWebResponse)
                        {
                            StreamReader reader2 = new StreamReader(response2.GetResponseStream());
                            string result2 = await reader2.ReadToEndAsync();
                            dynamic jsonObj2 = System.Web.Helpers.Json.Decode(result2);
                            Models.SocialMedia.SocialMedia.Profile from2 = new Models.SocialMedia.SocialMedia.Profile(jsonObj2);
                            comment_profile.Add(from2.profile);
                        }
                    }

                    foreach (var from in pictures)
                    {
                        string url1 = String.Format("https://graph.facebook.com/" + from + "?fields=picture&access_token={0}", accessToken);
                        HttpWebRequest request1 = WebRequest.Create(url1) as HttpWebRequest;
                        request1.Method = "GET";
                        using (HttpWebResponse response1 = await request1.GetResponseAsync() as HttpWebResponse)
                        {
                            StreamReader reader1 = new StreamReader(response1.GetResponseStream());
                            string result1 = await reader1.ReadToEndAsync();
                            dynamic jsonObj1 = System.Web.Helpers.Json.Decode(result1);
                            Models.SocialMedia.SocialMedia.Profile from1 = new Models.SocialMedia.SocialMedia.Profile(jsonObj1);
                            profile.Add(from1.profile);

                        }
                    }
                    Models.SocialMedia.SocialMedia.Pictures pics = new Models.SocialMedia.SocialMedia.Pictures(profile, comment_profile);

                    return View(Tuple.Create(pics, posts));
                }
            }
            catch (Exception exs)
            { return RedirectToAction("Index2"); }
        }

        public ActionResult Careers()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;

            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            return View();
        }
        public ActionResult Index2()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;

            Restaurant obj1 = new Restaurant();
            var data = obj.Restaurants.Where(x => x.Status != 0).ToList();
            ViewBag.details = data;


            return View();
        }

        public string PostSendMessage()
        {
            var check = "true";

            string name = System.Web.HttpContext.Current.Request["Name"];
            string phone = System.Web.HttpContext.Current.Request["Email"];
            string message = System.Web.HttpContext.Current.Request["Message"];
            string CNIC = System.Web.HttpContext.Current.Request["Password"];


            try
            {
                if (ModelState.IsValid)
                {
                    var sender = new MailAddress("eatsleeprepeatesr1122@gmail.com", "By Eatsleeprepeat");
                    var rec = new MailAddress("eatsleeprepeatesr1122@gmail.com");

                    var pass = "eatsleeprepeat";
                    var body = "Name: " + name + "\nPhone: " + phone + "\nCNIC: " + CNIC + "\nMessage: " + message;


                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sender.Address, pass)
                    };

                    MailMessage msgMail = new MailMessage(sender, rec);
                    msgMail.IsBodyHtml = false;
                    msgMail.Subject = "Contact";
                    msgMail.Body = body;
                    smtp.Timeout = 200000;
                    smtp.Send(msgMail);

                }

            }
            catch (Exception e)
            {
                check = "false";
            }
            return check;
        }

        public ActionResult Contests()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            return View();
        }

        public ActionResult Events()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            return View();
        }

        public ActionResult Gallery()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            return View();
        }

        public ActionResult Restaurants()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            try
            {
                HttpCookie authCookie = Request.Cookies["userteripengfkansdHello1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;

                if (!expired)
                {
                    ViewBag.UserId = CookieId;
                }
                else
                {
                    ViewBag.UserId = 0;
                }
                return View();
            }
            catch (Exception ex) { return View(); }
        }

        public ActionResult AboutUs()
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;

            return View();
        }
        public ActionResult AddTag(int id)
        {
            ViewBag.Id = id;
            Restaurant obj1 = new Restaurant();
            var data = obj.Restaurants.Where(x => x.Status != 0).ToList();
            ViewBag.details = data;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;
            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            return View();
        }
        public ActionResult SignInTable(UserLogin u)
        {
            List<string> upperCarousel = new List<string>();
            List<string> lowerCarousel = new List<string>();
            var clients = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();

            foreach (var details in clients)
            {
                var clientPackages = obj.ClientPakages.Where(x => x.ClientId.Equals(details.Id)).ToList();
                foreach (var details1 in clientPackages)
                {
                    if (details1.Pakage.PakageType == "gold")
                    {
                        upperCarousel.Add(details.Image);
                    }
                    else if (details1.Pakage.PakageType == "silver")
                    {
                        lowerCarousel.Add(details.Image);
                    }
                }

            }

            ViewBag.upperCarousel = upperCarousel;
            ViewBag.lowerCarousel = lowerCarousel;

            Client obj2 = new Client();
            var data2 = obj.Clients.Where(x => x.Status != 0 && x.Image != null).ToList();
            ViewBag.details1 = data2;
            string SessionValue = "";
            UserLogin login = new UserLogin();
            try
            {
                login = obj.UserLogins.First(x => x.UserName.Equals(u.UserName) && x.Password.Equals(u.Password));
            }
            catch (Exception e) { }

            var check = "true";
            User temp = new User();
            try
            {
                temp = obj.Users.First(x => x.Id.Equals(login.UserId));
                SessionValue = temp.Name;
                SessionValue = SessionValue + "$";
                SessionValue = SessionValue + temp.Email;
            }
            catch (Exception e1) { check = "false"; }
            if (check == "true")
            {
                FormsAuthentication.SignOut();
                DateTime cookieIssuedDate = DateTime.Now;

                var ticket = new FormsAuthenticationTicket(0,
                    temp.Id.ToString(),
                    cookieIssuedDate,
                    cookieIssuedDate.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    false,
                    SessionValue,
                    FormsAuthentication.FormsCookiePath);

                string encryptedCookieContent = FormsAuthentication.Encrypt(ticket);

                var formsAuthenticationTicketCookie = new HttpCookie("userteripengfkansdHello1234hytusksdbsdfasdjasdidasdijnasd", encryptedCookieContent)
                {
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath,
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL
                };

                System.Web.HttpContext.Current.Response.Cookies.Add(formsAuthenticationTicketCookie);
                FormsAuthentication.SetAuthCookie(temp.Name, false);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Userlogout()
        {
            HttpCookie oldCookie = new HttpCookie("TrainerHello1234hytusksdbsdfasdjasdidasdijnasd");
            oldCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(oldCookie);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public string TraineeSessionCheck()
        {
            try
            {
                HttpCookie authCookie = Request.Cookies["TraineeHellodjfdshfsdanfkjdsfsfwgbhwvifbwsdjwiufdn"];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string CookieId = ticket.Name;
                string userData = ticket.UserData;
                int version = ticket.Version;

                if (!expired)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception e) { return "False"; }
        }
    }
}