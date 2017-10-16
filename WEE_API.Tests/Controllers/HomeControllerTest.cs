using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEE_API;
using WEE_API.Controllers;
using WEE_API.Controllers.ADM;

namespace WEE_API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
