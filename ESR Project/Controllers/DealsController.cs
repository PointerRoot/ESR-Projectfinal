using ESR_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESR_Project.Controllers
{
    public class DealsController : ApiController
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public List<ESR> GetAllClassesNames()
        {
            List<ESR> li = new List<ESR>();

            var All = obj.Deals.Where(x => x.Status != 0).ToList();

            foreach (var details in All)
            {
                ESR temp = new ESR();
                temp.Name = details.Description;
                li.Add(temp);
            }
            return li;
        }
        public List<DealsClass> GetAllDeals(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Deals.Count();
            int max = (skip + ShowRecords);
            if (max > AllRecords)
            {
                max = AllRecords;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + AllRecords.ToString();

            List<DealsClass> li = new List<DealsClass>();

            var AllDeals = obj.Deals.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllDeals.Count();
            foreach (var details in AllDeals)
            {
                DealsClass temp = new DealsClass();
                temp.Id = details.Id;
                temp.Description = details.Description;
                temp.Count = count;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
            }
            return li;
        }
        public List<DealsClass> GetAllDealsByName(string id)
        {
            List<DealsClass> li = new List<DealsClass>();

            var AllDeals = obj.Deals.Where(x => x.Status != 0 && x.Description.Equals(id)).ToList();
            var count = AllDeals.Count();
            foreach (var details in AllDeals)
            {
                DealsClass temp = new DealsClass();
                temp.Id = details.Id;
                temp.Description = details.Description;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<DealsClass> GetAllDealsById(int id)
        {
            List<DealsClass> li = new List<DealsClass>();

            var AllDeals = obj.Deals.Where(x => x.Status != 0 && x.Id.Equals(id)).ToList();
            var count = AllDeals.Count();
            foreach (var details in AllDeals)
            {
                DealsClass temp = new DealsClass();
                temp.Id = details.Id;
                temp.Description = details.Description;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }
        public int PostDeals(DealsClass DC)
        {
            int check = 0;
            try
            {
                Deal Deals = new Deal();
                Deals.Description = DC.Description;
                Deals.Status = 1;
                obj.Deals.InsertOnSubmit(Deals);
                obj.SubmitChanges();
                check = Deals.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int UpdateDeals(DealsClass DC)
        {
            int check = 0;
            try
            {
                Deal Deals = obj.Deals.First(x=>x.Id.Equals(DC.Id));
                Deals.Description = DC.Description;
                Deals.Status = 1;
                
                obj.SubmitChanges();
                check = Deals.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int DeleteDeals(int id)
        {
            int check = 0;
            try
            {
                Deal Deals = obj.Deals.First(x => x.Id.Equals(id));
                Deals.Status = 0;
                obj.SubmitChanges();
                check = 1;
            }
            catch (Exception e) { }

            return check;
        }
    }
}
