using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WEE_API.Controllers.API;
using WEE_API.Models;
using WEE_API.RBAC;

namespace WEE_API.Tests.Common
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            //mock.Setup(x => x.Set<ApplicationUser>())
            //    .Returns(new FakeDbSet<ApplicationUser>
            //    {
            //        new ApplicationUser { Id = 2, UserName = "user", Email = "defaultuser@somedomain.com", FullName = "Default User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true }
            //    });
            var db = new DBContext();

            var a = db.Company.ToList();

            var c=   new UserRatingCompany {CompanyID = 1, UserID = 3, AnswerID = 1,  QuestionID = 1, Score = 3m};
            var d = new UserRatingCompany { CompanyID = 1, UserID =2, AnswerID = 1,   QuestionID = 1, Score = 4m };
            // Arrange
            var controller = new UserRatingCompanyController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.PostUserRatingCompany(d);
            // Act
            var response = controller.PostUserRatingCompany(c);
             
            


        }
    }
}
