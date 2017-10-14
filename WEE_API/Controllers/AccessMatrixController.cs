using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WEE_WEB_API.Models;
using WEE_WEB_API.ViewModel;

namespace WEE_WEB_API.Controllers
{
    public class AccessMatrixController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PopulateAccessRight(string userID)
        {
            var model = new List<AccessMatrixViewModel>();
            var ds = db.AD_User_Menu.Where(a => a.UserID == userID).Select(a => new AccessMatrixViewModel
            {
                MenuID = a.MenuID,
                UserID = a.UserID,
                View = a.Read ?? false,
                Edit = a.Edit ?? false,
                Add = a.Add ?? false,
                Delete = a.Delete ?? false,
                PDF = a.PDF ?? false,
                Excel = a.Excel_CSV ?? false,
                Print = a.Print ?? false,

            }).ToList();
            return PartialView("_Matrix", model);
        }

    }
}