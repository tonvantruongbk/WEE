using System;
using System.Linq;
using System.Web.Mvc;
using WEE_API.Models;
using WEE_API.ViewModel;

namespace WEE_API.Controllers
{
    [RBAC]
    public class HomeController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult Index()
        {
            var DashBoardViewModel = new DashBoardViewModel
            {
                TotalNumberInfo = new TotalNumberInfo
                {
                    CompanyNum = db.Company.Count(),
                    CompanyPer =
                        Math.Round(db.Company.Count(a => !(a.TotalUserRate == null || a.TotalUserRate == 0))
                                   / (decimal)db.Company.Count() * 100, 0),
                    UserNum = db.Users.Count(),
                    JobNum = db.Job.Count(),
                    RateNum = db.UserRatingCompany.Count(),
                    //UserPer = Math.Round((decimal)(db.Company.Count(a => !(a.TotalUserRate == null || a.TotalUserRate == 0)) / db.Company.Count()), 1),
                }
            };

            return View(DashBoardViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                db = null;
            }
            base.Dispose(disposing);
        }
    }

}