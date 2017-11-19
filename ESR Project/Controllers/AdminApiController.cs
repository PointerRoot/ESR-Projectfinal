using ESR_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESR_Project.Controllers
{
    public class AdminApiController : ApiController
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public List<ESR> GetAllClassesNames()
        {
            List<ESR> li = new List<ESR>();

            var All = obj.Admins.Where(x => x.Status != 0).ToList();

            foreach (var details in All)
            {
                ESR temp = new ESR();
                temp.Name = details.UserName;
                li.Add(temp);
            }
            return li;
        }
        public List<AdminClass> GetAllAdmins(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Admins.Count();
            int max = (skip + ShowRecords);
            if (max > AllRecords)
            {
                max = AllRecords;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + AllRecords.ToString();

            List<AdminClass> li = new List<AdminClass>();

            var AllAdmins = obj.Admins.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllAdmins.Count();
            foreach (var details in AllAdmins)
            {
                AdminClass temp = new AdminClass();
                temp.Id = details.Id;
                temp.UserName = details.UserName;
              
                temp.Count = count;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
            }
            return li;
        }
        public List<AdminClass> GetAllAdminsByName(string id)
        {
            List<AdminClass> li = new List<AdminClass>();

            var AllAdmins = obj.Admins.Where(x => x.Status != 0 && x.UserName.Equals(id)).ToList();
            var count = AllAdmins.Count();
            foreach (var details in AllAdmins)
            {
                AdminClass temp = new AdminClass();
                temp.Id = details.Id;
                temp.UserName = details.UserName;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<AdminClass> GetAllAdminsById(int id)
        {
            List<AdminClass> li = new List<AdminClass>();

            var AllAdmins = obj.Admins.Where(x => x.Status != 0 && x.Id.Equals(id)).ToList();
            var count = AllAdmins.Count();
            foreach (var details in AllAdmins)
            {
                AdminClass temp = new AdminClass();
                temp.Id = details.Id;
                temp.UserName = details.UserName;
                temp.Role = details.Status.Value;
      
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }
        public int PostAdmin(AdminClass AC)
        {
            int check = 0;
            try
            {
                Admin Admins = new Admin();
                Admins.UserName = AC.UserName;
                Admins.Password = AC.Password;
                Admins.Status = AC.Role;
                Admins.AddDate = DateTime.Now.ToShortDateString();
                obj.Admins.InsertOnSubmit(Admins);
                obj.SubmitChanges();
                check = Admins.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int UpdateAdmin(AdminClass AC)
        {
            int check = 0;
            try
            {
                Admin Admins = obj.Admins.First(x => x.Id.Equals(AC.Id));
                Admins.UserName = AC.UserName;
                if (AC.Password != "No")
                {
                    Admins.Password = AC.Password;
                }
                Admins.Status = AC.Role;
                obj.SubmitChanges();
                check = Admins.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int DeleteAdmin(int id)
        {
            int check = 0;
            try
            {
                Admin admin = obj.Admins.First(x => x.Id.Equals(id));
                admin.Status = 0;
                obj.SubmitChanges();
            }
            catch (Exception e) { }

            return check;
        }
  
    }
}
