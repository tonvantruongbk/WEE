using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEE_WEB_API.Models;

namespace WEE_WEB_API.Controllers
{
    public class AD_UserController : Controller
    {
        DBContext db = new DBContext();
        // GET: AD_User
        public ActionResult Index()
        {
            var model = new AD_User();
            var id = "admin";
            if (id != null)
            {
                model = db.AD_User.Where(a => a.UserID == id).FirstOrDefault();
            }
            return View(model);        
        }       
        [HttpPost]
        public JsonResult AddUser(AD_User Aduser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.AD_User.Add(Aduser);
                    db.SaveChanges();
                    return Json(new { Result = "success", Message = "Thêm thành công" });
                }
                catch (Exception ex)
                {
                    return Json("thông báo lỗi" ,ex.Message);
                }
            }
            return Json(new { Result = "error", Message = "Thêm thất bại" });
        }
        [HttpPost]
        public JsonResult EditUser(AD_User Aduser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(Aduser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Result = "success", Message = "Chỉnh sửa thành công" });
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return Json(new { Result = "error", Message = "Chỉnh sửa thất bại" });
        }

    }
}