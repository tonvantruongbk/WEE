using System;
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
            var db = new DBContext("Data Source=(local);Initial Catalog=WEE_DB2017;Integrated Security=True");
            try
            {
                db.Database.Delete();
            }
            catch (Exception e)
            {
                // ignored
            }

            var a = db.Company.ToList();

            var db1 = new DBContext("Data Source=118.70.117.56;Initial Catalog=WEE_DB2017;User Id=sa; password=cosmic1234$;");
            try
            {
                db1.Database.Delete();
            }
            catch (Exception e)
            {
                // ignored
            }

            var b = db1.Company.ToList();
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
                var result = UserManager.Create(user, "adminn");
                Assert.IsTrue(result.Succeeded);
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
