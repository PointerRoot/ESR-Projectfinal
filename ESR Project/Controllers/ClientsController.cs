using ESR_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace ESR_Project.Controllers
{
    public class ClientsController : ApiController
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public List<ESR> GetAllClassesNames()
        {
            List<ESR> li = new List<ESR>();

            var All = obj.Clients.Where(x => x.Status != 0).ToList();

            foreach (var details in All)
            {
                ESR temp = new ESR();
                temp.Name = details.Name;
                li.Add(temp);
            }
            return li;
        }
        public List<ClientsClass> GetAllClient(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Clients.Where(x => x.Status != 0);
            var Records = AllRecords.Count();
            int max = (skip + ShowRecords);
            if (max > Records)
            {
                max = Records;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + Records.ToString();

            List<ClientsClass> li = new List<ClientsClass>();

            var AllClients = obj.Clients.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllClients.Count();
            foreach (var details in AllClients)
            {
                ClientsClass temp = new ClientsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.Testimonial = details.Testimonials;
                temp.Count = count;
                if (max >= Records)
                    temp.Next = id;
                else
                    temp.Next = id + 1;
                if (id == 0)
                    temp.Prev = id;
                else
                    temp.Prev = id - 1;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
            }
            return li;
        }
        public List<ClientsClass> GetAllClientsByName(string id)
        {
            List<ClientsClass> li = new List<ClientsClass>();

            var AllClients = obj.Clients.Where(x => x.Status != 0 && x.Name.Equals(id)).ToList();
            var count = AllClients.Count();
            foreach (var details in AllClients)
            {
                ClientsClass temp = new ClientsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.Testimonial = details.Testimonials;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<ClientsClass> GetAllClientsById(int id)
        {
            List<ClientsClass> li = new List<ClientsClass>();

            var AllClients = obj.Clients.Where(x => x.Status != 0 && x.Id.Equals(id)).ToList();
            var count = AllClients.Count();
            foreach (var details in AllClients)
            {
                ClientsClass temp = new ClientsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.Testimonial = details.Testimonials;
                temp.Count = count;
               
                li.Add(temp);
            }
            return li;
        }
        public List<ClientsClass> GetAllClientsPakageById(int id)
        {
            List<ClientsClass> li = new List<ClientsClass>();

            var AllClients = obj.ClientPakages.Where(x =>  x.ClientId.Equals(id)).ToList();
            var count = AllClients.Count();
            foreach (var details in AllClients)
            {
                ClientsClass temp = new ClientsClass();
                temp.PakageId = details.PakagesId;
                
                temp.Count = count;

                li.Add(temp);
            }
            return li;
        }

        public int PostClient(ClientsClass CC)
        {
            int check = 0;
            int Id = 0;
            var typeOfImage = "";
           

            try
            {
                Client client = new Client();
                client.Name = CC.Name;
                client.Testimonials = CC.Testimonial;
                client.Status = 1;
                
                client.AddDate = DateTime.Now.ToShortDateString();
                obj.Clients.InsertOnSubmit(client);
                obj.SubmitChanges();
                check = client.Id;
                Id = client.Id;

                 

               

            }
            catch (Exception e) { check = 0; }
            return check;
        }

        public int postImage()
        {

            int check=1;
            //posting image
            var typeOfImage = "";
            int Id;

            try
            {
                Id = int.Parse(System.Web.HttpContext.Current.Request["Id"]);
                // Get the uploaded image from the Files collection
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["Image"];
                string httpPostedFile1 = httpPostedFile.FileName;
                string[] httpPostedFile2 = httpPostedFile1.Split('.');
                typeOfImage = "." + httpPostedFile2[httpPostedFile2.Length - 1];

                if (httpPostedFile != null)
                {
                    WebImage img = new WebImage(httpPostedFile.InputStream);
                    if (img.Width > 1000)
                        img.Resize(1000, 1000);
                    img.Save(@"~\Content\Images\Ads\" + (Id + typeOfImage));
                }



                string image = Id.ToString() + typeOfImage.ToString();
                Client Cl = obj.Clients.First(x => x.Id.Equals(Id));
                Cl.Image = image;
                obj.SubmitChanges();

            }
            catch (Exception e)
            {
                check = 0;
            }
            
            return check;
        }
        public int PostClientPakage(string id)
        {
            var str = id.Split(',');
            int check = 0;
            try
            {
                ClientPakage pakage = new ClientPakage();
                pakage.ClientId = Int32.Parse(str[0]);
                pakage.PakagesId = Int32.Parse(str[1]);
                obj.ClientPakages.InsertOnSubmit(pakage);
                obj.SubmitChanges();
                check = pakage.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int UpdateClient(ClientsClass CC)
        {
            int check = 0;
            try
            {
                Client client = obj.Clients.First(x => x.Id.Equals(CC.Id));
                client.Name = CC.Name;
                client.Testimonials = CC.Testimonial;
                obj.SubmitChanges();
                check = client.Id;
                List<ClientPakage> cp=obj.ClientPakages.Where(x => x.ClientId.Equals(client.Id)).ToList();
                obj.ClientPakages.DeleteAllOnSubmit(cp);
                obj.SubmitChanges();

            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int DeleteClient(int id)
        {
            int check = 0;
            try
            {
                Client client = obj.Clients.First(x => x.Id.Equals(id));
                client.Status = 0;
                obj.SubmitChanges();
                check = 1;
                List<ClientPakage> cp = obj.ClientPakages.Where(x => x.ClientId.Equals(id)).ToList();
                obj.ClientPakages.DeleteAllOnSubmit(cp);
                obj.SubmitChanges();
            }
            catch (Exception e) { }

            return check;
        }
    }
}
