
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using WEE_API.Common;
using WEE_API.Common.Datatables;
using WEE_API.Models;
using System.Linq.Dynamic;
using System.Text;
using WEE_API.ViewModel;

namespace WEE_API.Controllers
{
    public class PermissionController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DataHandler(DatatablesRequest request)
        {
            try
            {
                var all = db.AD_User.AsQueryable();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<AD_User>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<AD_User> result = new ReponseToDatatables<AD_User>
                {
                    draw = request.Draw,
                    data = finalquery.ToList(),
                    recordsFiltered = queryFiltered.Count(),
                    recordsTotal = all.Count()
                };
                if (request.FilterBase != null)
                {
                    foreach (var dtFilterBase in request.FilterBase)
                    {
                        Type itemType = result.GetType();
                        try
                        {
                            var field = itemType.GetProperty("yadcf_data_" + dtFilterBase.Ydacf_Number);
                            if (field != null)
                            {
                                //var bbb = db.TB_Pick.Select("new (" + dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
                                //field.SetValue(result, bbb.Distinct().ToListAsync().Result);
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
                return new JsonNetResult { Data = result };
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Create(AD_User data)
        {
            db.AD_User.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(string Id, AD_User data)
        {
            if (data != null)
            {
                data.UserID = Id;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            AD_User entity = db.AD_User.FirstOrDefault(a => a.UserID == id);
            if (entity != null)
            {
                db.AD_User.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult PopulateAccessRight(string userID)
        {
            var model = (from b in (from AD_Menu in db.AD_Menu
                                    where
                                    AD_Menu.URLAction != null &&
                                    AD_Menu.URLAction != "" &&
                                    AD_Menu.URLAction != "#"
                                    select new
                                    {
                                        AD_Menu
                                    })
                         join a in (from AD_User_Menu in db.AD_User_Menu
                                    where
                                    AD_User_Menu.UserID == userID
                                    select new
                                    {
                                        AD_User_Menu
                                    }) on b.AD_Menu.MenuID equals a.AD_User_Menu.MenuID into A_join
                         from a in A_join.DefaultIfEmpty()
                         select new AccessMatrixViewModel
                         {
                             MenuID = b.AD_Menu.MenuID,
                             UserID = userID,
                             View = a.AD_User_Menu.Read ?? false,
                             Edit = a.AD_User_Menu.Edit ?? false,
                             Add = a.AD_User_Menu.Add ?? false,
                             Delete = a.AD_User_Menu.Delete ?? false,
                             Excel = a.AD_User_Menu.Excel_CSV ?? false,
                             PDF = a.AD_User_Menu.PDF ?? false,
                             Print = a.AD_User_Menu.Print ?? false,
                             MenuText = b.AD_Menu.MenuText
                         }).ToList();
            return PartialView("_Matrix", model);
        }

        [HttpPost]
        public JsonResult SubmitFormAccessMatrix(List<AccessMatrixViewModel> frm)
        {
            try
            {
                foreach (var accessMatrixViewModel in frm)
                {
                    var model = new AD_User_Menu
                    {
                        MenuID = accessMatrixViewModel.MenuID,
                        UserID = accessMatrixViewModel.UserID,
                        Read = accessMatrixViewModel.View,
                        Edit = accessMatrixViewModel.Edit,
                        Add = accessMatrixViewModel.Add,
                        Delete = accessMatrixViewModel.Delete,
                        Excel_CSV = accessMatrixViewModel.Excel,
                        PDF = accessMatrixViewModel.PDF,
                    };

                    if (db.AD_User_Menu.Any(m=>m.MenuID==model.MenuID && m.UserID==model.UserID))
                    {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        db.AD_User_Menu.Add(model);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { Message = "Đã có lỗi. Chưa xét quyền thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Đã xét quyền thành công!" }, JsonRequestBehavior.AllowGet);
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