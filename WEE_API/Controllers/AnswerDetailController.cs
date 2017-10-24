
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
    public class AnswerDetailController : Controller
    { 
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            Session["AnswerID"] = null;
            return View();
        }

        [HttpPost]
        public JsonResult DataHandler(DatatablesRequest request)
        {
            try
            {
                long id = 0;
                var all = db.AnswerDetail
                            .Include(a=>a.Answer)
                            .AsNoTracking();
                if (!string.IsNullOrEmpty(Session["AnswerID"] + ""))
                {
                    id = Convert.ToInt64(Session["AnswerID"] + "");
                    all = db.AnswerDetail.Where(a => a.AnswerID == id)
                            .Include(a=>a.Answer)
                        .AsNoTracking();
                }
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<AnswerDetail>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<AnswerDetail> result = new ReponseToDatatables<AnswerDetail>
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
                                var bbb = db.AnswerDetail.Select("new ("+ dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
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
        public JsonResult Create(AnswerDetail data)
        {
            db.AnswerDetail.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(int Id, AnswerDetail data)
        {
            if (data != null)
            {
                data.AnswerDetailID = Id;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            AnswerDetail entity = db.AnswerDetail.FirstOrDefault(a => a.AnswerID == id);
            if (entity != null)
            {
                db.AnswerDetail.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList2Select()
        {
          var result =  db.AnswerDetail.Select(a => new CommonModel {label = a.AnswerDetailName, value = a.AnswerDetailID}).ToList();
           
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void SetParrent(int id)
        {
            Session["AnswerID"] = id;
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