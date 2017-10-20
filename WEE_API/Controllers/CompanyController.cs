
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
using System.Data.Entity.Migrations;
using System.IO;
using Newtonsoft.Json;


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
                var all = db.Company
                    .Include(a => a.Location)
                    .Include(a => a.ListCompanyZone.Select(b => b.Zone))
                    .AsNoTracking();
                var queryFiltered = all.SearchForDataTables(request);
                queryFiltered = queryFiltered.Sort(request) as IQueryable<Company>;
                var finalquery = queryFiltered.Skip(request.Start).Take(request.Length);
                var lstFinal = new List<Company>();
                foreach (var company in finalquery.ToList())
                {
                    company.CompanyZone = company.ListCompanyZone.Select(a=>new MultipleCheckboxClass() {id = a.ZoneID,name = a.Zone.ZoneName}).ToList();
                    company.ListCompanyZone = null;
                    lstFinal.Add(company);
                }
                var ttt = new Dictionary<string, List<SelectizeClass>>
                {
                    {"CompanyZone[].id", db.Zone.Select(a => new SelectizeClass() {value = a.ZoneID, label = a.ZoneName + ""}).ToList()}
                };

                ReponseToDatatables<Company> result = new ReponseToDatatables<Company>
                {
                    draw = request.Draw,
                    data = lstFinal,
                    recordsFiltered = queryFiltered.Count(),
                    recordsTotal = all.Count(),
                    files = CommonFunction.GenListImageJSON(db.Company.Where(a=>!string.IsNullOrEmpty(a.Logo)).Select(a=>a.Logo).ToList()),
                    options =  ttt
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
                                var bbb = db.Company.Select("new (" + dtFilterBase.Ydacf_FieldName + " as label, " + dtFilterBase.Ydacf_FieldName + " as value)");
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

                db.CompanyZone.RemoveRange(db.CompanyZone.Where(b => b.CompanyID == data.CompanyID).AsEnumerable());
                db.CompanyZone.AddRange(data.CompanyZone.Select(b=>new CompanyZone() {CompanyID = data.CompanyID, ZoneID = b.id}).ToArray());

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
        public JsonResult UploadImage()
        {
            var fileName = "";
            var path = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file != null)
                {
                    fileName = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/Content/UPLOAD/"), fileName);
                    file.SaveAs(path);
                }
            }
            //var wpath = (new UrlHelper(this.ControllerContext.RequestContext)).Content(@"~/Content/UPLOAD/") + fileName;
            var wpath = "/Content/UPLOAD/" + fileName;

            return Json(CommonFunction.GenImageJSON(wpath), JsonRequestBehavior.AllowGet);
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