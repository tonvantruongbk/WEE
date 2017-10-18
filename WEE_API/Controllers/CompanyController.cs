
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WEE_API.Common;
using WEE_API.Common.Datatables;
using WEE_API.Models;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.IO;


namespace WEE_API.Controllers
{ 
    public class CompanyController : Controller
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
                var all = db.Company.Include(a=>a.Location).Include(a=>a.Zone).AsQueryable();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<Company>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                ReponseToDatatables<Company> result = new ReponseToDatatables<Company>
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
                                var bbb = db.Company.Select("new ("+ dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
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
        public JsonResult Create(Company data)
        {
            db.Company.Add(data);
            db.SaveChanges();
            return Json(new { Message = "Đã thêm thành công!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(int Id, Company data)
        {
            if (data != null)
            {
                data.CompanyID = Id;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = "Đã sửa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Company entity = db.Company.FirstOrDefault(a => a.CompanyID == id);
            if (entity != null)
            {
                db.Company.Remove(entity);
                db.SaveChanges();
            }
            return Json(new { Message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult UploadImage()
        {
            var fileName = "";
            var path = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file != null)
                {
                    fileName = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    file.SaveAs(path);
                }
            }
            return Json(new { files = new { files = new { a = new { filename = fileName, web_path = path } } }, upload = new { id = "a" } }, JsonRequestBehavior.AllowGet);
        }


       

        [HttpPost]
        public ActionResult UploadImage1(string id)
        {

            return Json(new { Message = "Đã up thành công!" }, JsonRequestBehavior.AllowGet);
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