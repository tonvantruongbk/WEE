using System.Web.Mvc;

namespace WEE_API.Controllers
{
    public class UnauthorisedController : Controller
    {
        // GET: Unauthorised
        public ActionResult Index()
        {    
            return View();
        }

        public ActionResult Error(string _errorMsg)
        {   
            ViewBag.ErrorMsg = _errorMsg;            
            return View();
        }       
    }
}