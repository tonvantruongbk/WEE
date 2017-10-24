
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
    public class JobController : Controller
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
                long id = 0;
                if (!string.IsNullOrEmpty(Session["CompanyID"] + ""))
                {
                    id = Convert.ToInt64(Session["CompanyID"] + "");
                }
                var all = db.Job.Where(a => a.CompanyID == id )
                            .Include(a => a.Company)
                            .Include(a => a.JobType)
                            .AsNoTracking();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<Job>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<Job> result = new ReponseToDatatables<Job>
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
                                var bbb = db.Job.Select("new (" + dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
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
        public JsonResult Create(Job data)
        {
            db.Job.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(int Id, Job data)
        {
            if (data != null)
            {
                data.JobID = Id;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Job entity = db.Job.FirstOrDefault(a => a.JobID == id);
            if (entity != null)
            {
                db.Job.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList2Select()
        {
            var result = db.Job.Select(a => new CommonModel { label = a.JobName, value = a.JobID }).ToList();

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SetParrent(int id)
        {
            Session["CompanyID"] = id;
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