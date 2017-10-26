
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WEE_API.Common;
using WEE_API.Common.Datatables;
using WEE_API.Models;
using System.Linq.Dynamic;
using System.Data.Entity;
using WEE_API.RBAC;


namespace WEE_API.Controllers
{
    public class UsersController : Controller
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
                var all = db.Users
                    .Include(a => a.Roles.Select(b=>b.Role))
                    .AsNoTracking();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<ApplicationUser>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);

                var lstFinal = new List<ApplicationUser>();
                foreach (var user in finalquery.ToList())
                {
                    if (user.Roles != null)
                    {
                        user.UserRole = user.Roles.Select(a => new MultipleCheckboxClass() { id = a.RoleId, name = a.Role.Name }).ToList();
                    }
                    lstFinal.Add(user);
                }
                var UserRoleOptions = new Dictionary<string, List<CommonModel>>
                {
                    {"UserRole[].id", db.Roles.Select(a => new CommonModel() {value = a.Id, label = a.Name + ""}).ToList()}
                };
                ReponseToDatatables<ApplicationUser> result = new ReponseToDatatables<ApplicationUser>
                {
                    draw = request.Draw,
                    data = lstFinal,
                    recordsFiltered = queryFiltered.Count(),
                    recordsTotal = all.Count(),
                    options = UserRoleOptions
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
                                var bbb = db.Users.Select("new (" + dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
                                field.SetValue(result, bbb.Distinct().ToListAsync().Result);
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
        public JsonResult Create(ApplicationUser data)
        {
            db.Users.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(int Id, ApplicationUser data)
        {
            if (data != null)
            {
                data.Id = Id;
                db.Entry(data).State = EntityState.Modified;
                db.UserRoles.RemoveRange(db.UserRoles.Where(b => b.UserId == data.Id).AsEnumerable());
                db.UserRoles.AddRange(data.UserRole.Select(b => new ApplicationUserRole() { UserId = data.Id, RoleId = b.id }).ToArray());
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ApplicationUser entity = db.Users.FirstOrDefault(a => a.Id == id);
            if (entity != null)
            {
                db.Users.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList2Select()
        {
            var result = db.Users.Select(a => new CommonModel { label = a.UserName, value = a.Id }).ToList();

            return Json(new { result }, JsonRequestBehavior.AllowGet);
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