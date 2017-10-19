using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WEE_API.Common;
using WEE_API.Common.Datatables;
using WEE_API.Models;

namespace WEE_API.Controllers
{
    public class BatchEditController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult getData()
        {
            var model = db.Job.ToList();
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDataJSON()
        {
            var model = db.Job.Select(a => a.JobName).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveData(int abc, Job data)
        {


            var model = db.Job.Single(a => a.JobID == abc);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DataHandler(DatatablesRequest request)
        {
            try
            {
                var all = db.Job.AsQueryable();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<Job>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<Job> result = new ReponseToDatatables<Job>
                {
                    draw = request.Draw,
                    data = finalquery.ToList(),
                    recordsFiltered = queryFiltered.Count(),
                    recordsTotal = all.Count(),
                    //yadcf_data_0 = db.TB_Pick.Select(a=>new Yadcf_SourceServer { value = a.PickID+"", label = a.JobRef}).ToList(),
                    //yadcf_data_1 = db.TB_Pick.Select(a => new Yadcf_SourceServer { value = a.PickID + "", label = a.JobRef }).ToList(),
                    //yadcf_data_2 = db.TB_Pick.Select(a => new Yadcf_SourceServer { value = a.PickID + "", label = a.JobRef }).ToList(),
                };
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
        public JsonResult Edit(Job data)
        {
            if (data != null)
            {
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
    }

    public class Customer
    {
        public int CustomersID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Account { get; set; }
        public string CreditCard { get; set; }
    }
}
