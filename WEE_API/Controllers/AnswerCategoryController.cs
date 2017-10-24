
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
    public class AnswerCategoryController : Controller
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
                var all = db.Answer
                            .Include(a=>a.ListAnswerDetail)
                            .AsNoTracking();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<Answer>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<Answer> result = new ReponseToDatatables<Answer>
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
                                var bbb = db.Answer.Select("new ("+ dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
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
        public JsonResult Create(Answer data)
        {
            db.Answer.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(int Id, Answer data)
        {
            if (data != null)
            {
                data.AnswerID = Id;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Answer entity = db.Answer.FirstOrDefault(a => a.AnswerID == id);
            if (entity != null)
            {
                db.Answer.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList2Select()
        {
          var result =  db.Answer.Select(a => new CommonModel {label = a.AnswerName, value = a.AnswerID}).ToList();
           
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