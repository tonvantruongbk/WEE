
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WEE_API.Common;
using WEE_API.Common.Datatables;
using WEE_API.Models;
using System.Linq.Dynamic;
using System.Data.Entity;


namespace WEE_API.Controllers
{ 
    public class SalaryLevelController : Controller
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
                var all = db.SalaryLevel
                            .AsNoTracking();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<SalaryLevel>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<SalaryLevel> result = new ReponseToDatatables<SalaryLevel>
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
                            var field = itemType.GetProperty("yadcf_data_"+dtFilterBase.Ydacf_Number);
                            if (field != null)
                            {
                                var bbb = db.SalaryLevel.Select("new ("+ dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
                                field.SetValue(result,bbb.Distinct().ToListAsync().Result);
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
        public JsonResult Create(SalaryLevel data)
        {
            db.SalaryLevel.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(int Id, SalaryLevel data)
        {
            if (data != null)
            {
                data.SalaryLevelID = Id;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            SalaryLevel entity = db.SalaryLevel.FirstOrDefault(a => a.SalaryLevelID == id);
            if (entity != null)
            {
                db.SalaryLevel.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList2Select()
        {
          var result =  db.SalaryLevel.Select(a => new CommonModel {label = a.SalaryLevelName, value = a.SalaryLevelID}).ToList();
           
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