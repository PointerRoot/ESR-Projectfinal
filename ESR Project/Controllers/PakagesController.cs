﻿using ESR_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESR_Project.Controllers
{
    public class PakagesController : ApiController
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public List<ESR> GetAllClassesNames()
        {
            List<ESR> li = new List<ESR>();

            var All = obj.Pakages.Where(x => x.Status != 0).ToList();

            foreach (var details in All)
            {
                ESR temp = new ESR();
                temp.Name = details.Name;
                li.Add(temp);
            }
            return li;
        }
        public List<PakagesClass> GetAllPakages(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Pakages.Count();
            int max = (skip + ShowRecords);
            if (max > AllRecords)
            {
                max = AllRecords;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + AllRecords.ToString();

            List<PakagesClass> li = new List<PakagesClass>();

            var AllPakages = obj.Pakages.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllPakages.Count();
            foreach (var details in AllPakages)
            {
                PakagesClass temp = new PakagesClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.PakageType = details.PakageType;
                try
                {
                    temp.CostPerMonth = details.CostPerMonth.Value;
                }
                catch (Exception e) { temp.CostPerMonth = 0; }
                temp.ServicesIncluded = details.ServicesIncluded;
                temp.Count = count;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
            }
            return li;
        }
        public List<PakagesClass> GetAllPakagesByName(string id)
        {
            List<PakagesClass> li = new List<PakagesClass>();

            var AllPakages = obj.Pakages.Where(x => x.Status != 0 && x.Name.Equals(id)).ToList();
            var count = AllPakages.Count();
            foreach (var details in AllPakages)
            {
                PakagesClass temp = new PakagesClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.PakageType = details.PakageType;
                try
                {
                    temp.CostPerMonth = details.CostPerMonth.Value;
                }
                catch (Exception e) { temp.CostPerMonth = 0; }
                temp.ServicesIncluded = details.ServicesIncluded;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<PakagesClass> GetAllPakagesById(int id)
        {
            List<PakagesClass> li = new List<PakagesClass>();

            var AllPakages = obj.Pakages.Where(x => x.Status != 0 && x.Id.Equals(id)).ToList();
            var count = AllPakages.Count();
            foreach (var details in AllPakages)
            {
                PakagesClass temp = new PakagesClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                temp.PakageType = details.PakageType;
                try
                {
                    temp.CostPerMonth = details.CostPerMonth.Value;
                }
                catch (Exception e) { temp.CostPerMonth = 0; }
                temp.ServicesIncluded = details.ServicesIncluded;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }
        public List<PakagesClass> GetAllPakageServicesById(int id)
        {
            List<PakagesClass> li = new List<PakagesClass>();

            var Ps = obj.PakageServices.Where(x => x.PakagesId.Equals(id)).ToList();
            var count = Ps.Count();
            foreach (var details in Ps)
            {
                PakagesClass temp = new PakagesClass();
                temp.ServiceId = details.ServicesId;

                temp.Count = count;

                li.Add(temp);
            }
            return li;
        }

        public int PostPakage(PakagesClass PC)
        {
            int check = 0;
            try
            {
                Pakage pakage = new Pakage();
                pakage.Name = PC.Name;
                pakage.PakageType = PC.PakageType;
                pakage.CostPerMonth = PC.CostPerMonth;
                pakage.ServicesIncluded = PC.ServicesIncluded;
                pakage.Status = 1;
                pakage.AddDate = DateTime.Now.ToShortDateString();
                obj.Pakages.InsertOnSubmit(pakage);
                obj.SubmitChanges();
                check = pakage.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int PostPakageServices(string id)
        {
            var str = id.Split(',');
            int check = 0;
            try
            {
                PakageService pakage = new PakageService();
                pakage.PakagesId = Int32.Parse( str[0]);
                pakage.ServicesId = Int32.Parse(str[1]);
                obj.PakageServices.InsertOnSubmit(pakage);               
                obj.SubmitChanges();
                check = pakage.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int UpdatePakage(PakagesClass PC)
        {
            int check = 0;
            try
            {
                Pakage pakage = obj.Pakages.First(x=>x.Id.Equals(PC.Id));
                pakage.Name = PC.Name;
                pakage.PakageType = PC.PakageType;
                pakage.CostPerMonth = PC.CostPerMonth;
                pakage.ServicesIncluded = PC.ServicesIncluded;
                pakage.Status = 1;
                pakage.AddDate = DateTime.Now.ToShortDateString();
                
                obj.SubmitChanges();
                check = pakage.Id;
                List<PakageService> ps = obj.PakageServices.Where(x => x.PakagesId.Equals(pakage.Id)).ToList();
                obj.PakageServices.DeleteAllOnSubmit(ps);
                obj.SubmitChanges();

            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int DeletePakage(int id)
        {
            int check = 0;
            try
            {
                Pakage pakage = obj.Pakages.First(x => x.Id.Equals(id));
                pakage.Status = 0;
                obj.SubmitChanges();
                check = 1;
                List<PakageService> ps = obj.PakageServices.Where(x => x.PakagesId.Equals(id)).ToList();
                obj.PakageServices.DeleteAllOnSubmit(ps);
                obj.SubmitChanges();
            }
            catch (Exception e) { }

            return check;
        }
    }
}
