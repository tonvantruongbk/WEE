using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEE_API.Models;

namespace WEE_API.Tests.Common
{
    [TestClass]
    public class TestAutoUpdateMany2Many
    {
        [TestMethod]
        public void AutoJoinAndUpdateOne2Many()
        {
            var cpn = new Company () { CompanyName = "Test 1", Location = new Location() {LocationName = "Đà Nẵng"}, LocationID = 3, ZoneID = 1};

            var db  = new DBContext();

            db.Company.AddOrUpdate(a=>a.CompanyID,cpn);
            db.SaveChanges();
        }


        [TestMethod]
        public void AutoJoinAndUpdateMany2Many()
        {
            var db = new DBContext();
            var cpn = new Company() { CompanyName = "Test 2" , ListCompanyJob = new List<CompanyJob>() {new CompanyJob() {Job= new Job() {JobName = "vvvvv"} } } };

            db.Company.AddOrUpdate(a => a.CompanyID, cpn);
            db.SaveChanges();
        }
    }
}
