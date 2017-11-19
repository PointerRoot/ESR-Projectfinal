﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESR_Project.Models;

namespace ESR_Project.Controllers
{
    public class ContestsController : ApiController
    {
        DataClasses1DataContext obj = new DataClasses1DataContext();
        public List<ESR> GetAllClassesNames()
        {
            List<ESR> li = new List<ESR>();

            var All = obj.Contests.Where(x => x.Status != 0).ToList();

            foreach (var details in All)
            {
                ESR temp = new ESR();
                temp.Name = details.Name;
                li.Add(temp);
            }
            return li;
        }
        public List<ContestsClass> GetAllContests(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Contests.Count();
            int max = (skip + ShowRecords);
            if (max > AllRecords)
            {
                max = AllRecords;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + AllRecords.ToString();

            List<ContestsClass> li = new List<ContestsClass>();

            var AllContests = obj.Contests.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllContests.Count();
            foreach (var details in AllContests)
            {
                ContestsClass temp = new ContestsClass();
                temp.Id   = details.Id;
                temp.Name = details.Name;
                try
                {
                    temp.EntriesCount = details.EntriesCount.Value;
                }
                catch (Exception e) { temp.EntriesCount = 0; }
                temp.WinnerName      = details.WinnerName;
                temp.Reward          = details.Reward;
                temp.Date            = details.Date;
                temp.Count           = count;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
            }
            return li;
        }
        public List<ContestsClass> GetLatestContests(int id)
        {
            int ShowRecords = 20;
            int skip = id * ShowRecords;
            var DesiMasala = "";

            var AllRecords = obj.Contests.Count();
            int max = (skip + ShowRecords);
            if (max > AllRecords)
            {
                max = AllRecords;
            }
            DesiMasala = (skip + 1).ToString() + "-" + max.ToString() + "/" + AllRecords.ToString();

            List<ContestsClass> li = new List<ContestsClass>();

            var AllContests = obj.Contests.Skip(skip).Take(ShowRecords).Where(x => x.Status != 0).ToList();
            var count = AllContests.Count();
            int i = 0;
            foreach (var details in AllContests)
            {
                if (i == 3)
                    break;
                
                ContestsClass temp = new ContestsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                try
                {
                    temp.EntriesCount = details.EntriesCount.Value;
                }
                catch (Exception e) { temp.EntriesCount = 0; }
                temp.WinnerName = details.WinnerName;
                temp.Reward = details.Reward;
                temp.Date = details.Date;
                temp.Count = count;
                temp.NumberOfShowing = DesiMasala;
                li.Add(temp);
                i++;
            }
            List<ContestsClass> SortedList = li.OrderByDescending(x => x.AddDate).ToList();
            return SortedList;
        }
        public List<ContestsClass> GetAllContestsByName(string id)
        {
            List<ContestsClass> li = new List<ContestsClass>();

            var AllContests = obj.Contests.Where(x => x.Status != 0 && x.Name.Equals(id)).ToList();
            var count = AllContests.Count();
            foreach (var details in AllContests)
            {
                ContestsClass temp = new ContestsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                try
                {
                    temp.EntriesCount = details.EntriesCount.Value;
                }
                catch (Exception e) { temp.EntriesCount = 0; }
                temp.Date = details.Date;
                temp.WinnerName = details.WinnerName;
                temp.Reward = details.Reward;
                temp.Count = count;
                li.Add(temp);
            }
            return li;
        }

        public List<ContestsClass> GetAllContestsById(int id)
        {
            List<ContestsClass> li = new List<ContestsClass>();

            var AllContests = obj.Contests.Where(x => x.Status != 0 && x.Id.Equals(id)).ToList();
            var count = AllContests.Count();
            foreach (var details in AllContests)
            {
                ContestsClass temp = new ContestsClass();
                temp.Id = details.Id;
                temp.Name = details.Name;
                try
                {
                    temp.EntriesCount = details.EntriesCount.Value;
                }
                catch (Exception e) { temp.EntriesCount = 0; }
                temp.Date = details.Date;
                temp.WinnerName = details.WinnerName;
                temp.Reward = details.Reward;
                temp.Count  = count;
                li.Add(temp);
            }
            return li;
        }
        public int PostContest(ContestsClass CC)
        {
            int check = 0;
            try
            {
                Contest contest         = new Contest();
                contest.Name            = CC.Name;
                contest.EntriesCount    = CC.EntriesCount;
                contest.WinnerName      = CC.WinnerName;
                contest.Reward          = CC.Reward;
                contest.Date            = DateTime.Now.ToShortDateString();
                contest.AddDate = DateTime.Now.ToShortDateString();
                contest.Status          = 1;
                obj.Contests.InsertOnSubmit(contest);
                obj.SubmitChanges();
                check = contest.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int UpdateContest(ContestsClass CC)
        {
            int check = 0;
            try
            {
                Contest contest = obj.Contests.First(x => x.Id.Equals(CC.Id));
                contest.Name = CC.Name;
                contest.EntriesCount = CC.EntriesCount;
                contest.WinnerName = CC.WinnerName;
                contest.Reward = CC.Reward;
                if (CC.Date != "")
                {
                    contest.Date = CC.Date;
                }
                contest.AddDate = DateTime.Now.ToString();
                contest.Status = 1;
                obj.SubmitChanges();
                check = contest.Id;
            }
            catch (Exception e) { check = 0; }
            return check;
        }
        public int DeleteContest(int id)
        {
            int check = 0;
            try
            {
                Contest contest = obj.Contests.First(x => x.Id.Equals(id));
                contest.Status = 0;
                obj.SubmitChanges();
            }
            catch (Exception e) { }

            return check;
        }
    }
}
