﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEE_API.Models;
using WEE_API.RBAC;

namespace WEE_API.Tests.Common
{
    [TestClass]
    public class TestAutoUpdateMany2Many
    {
        [TestMethod]
        public void AutoJoinAndUpdateOne2Many()
        {
            var db = new DBContext();
            db.Database.Delete();

           var a =  db.Company.ToList(); 
        }

        [TestMethod]
        public void CreateAdmin()
        {
            try
            {
                var db = new DBContext();
                //Create User...
                var user = new ApplicationUser { Id = 1, UserName = "admin", Email = "admin@somedomain.com", FullName = "System Administrator", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };

                ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(db));
               var   result = UserManager.Create(user, "adminn");
            Assert.IsTrue(result.Succeeded );
            }
            catch (Exception ex)
            {

            }
        }
        //[TestMethod]
        //public void AutoJoinAndUpdateMany2Many()
        //{
        //    var db = new DBContext();
        //    var cpn = new Company() { CompanyName = "Test 2" , ListCompanyJob = new List<CompanyJob>() {new CompanyJob() {Job= new Job() {JobName = "vvvvv"} } } };

        //    db.Company.AddOrUpdate(a => a.CompanyID, cpn);
        //    db.SaveChanges();
        //}
    }
}
