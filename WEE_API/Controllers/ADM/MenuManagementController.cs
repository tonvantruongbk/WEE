using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WEE_API.Models;
using WEE_API.ViewModel;

namespace WEE_API.Controllers
{
    public class MenuManagementController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            var model = db.AD_Menu.OrderBy(a => a.MenuSort).Select(a => new
            {
                id = a.MenuID,
                name = a.MenuText,
                _parentId = a.MenuParentID,
                iconCls = a.MenuIcon,
                action = a.URLAction,
                order = a.MenuSort,
            }).ToList();
            return Json(new { total = model.Count, rows = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Menu()
        {
            var model = db.AD_Menu.ToList();
            return PartialView("Menu", model);
        }

        [HttpPost]
        public JsonResult SaveData(string json)
        {
            try
            {
                var data = new JavaScriptSerializer().Deserialize<List<MenuViewModel>>(json);
                if (data.Count == 0)
                {
                    var menuViewModel = new JavaScriptSerializer().Deserialize<MenuViewModel>(json);
                    var entity = new AD_Menu();
                    entity.MenuID = menuViewModel.Id;
                    entity.MenuParentID = menuViewModel._parentId;
                    entity.MenuText = menuViewModel.name;
                    entity.MenuIcon = menuViewModel.iconCls;
                    entity.URLAction = menuViewModel.action;
                    entity.MenuSort = menuViewModel.order;
                    db.AD_Menu.Add(entity);
                }
                else
                {
                    foreach (var menuViewModel in data)
                    {
                        var entity = db.AD_Menu.FirstOrDefault(a => a.MenuID == menuViewModel.Id);
                        if (entity != null)
                        {
                            entity.MenuParentID = menuViewModel._parentId;
                            entity.MenuText = menuViewModel.name;
                            entity.MenuIcon = menuViewModel.iconCls;
                            entity.URLAction = menuViewModel.action;
                            entity.MenuSort = menuViewModel.order;
                            db.Entry(entity).State = EntityState.Modified;
                        }
                    }
                }
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
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
