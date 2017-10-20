using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{

    public class SearchCompanyByNameLocationController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Tìm kiếm công ty theo 3 tiêu chí (tên gần đúng, chính xác mã địa điểm và mã lĩnh vực)
        /// </summary>
        public IQueryable<Company> GetSearchCompanyByNameLocation(string name = null, int? locationId = null, int? ZoneId = null)
        {
            var company = db.Company.AsQueryable();
            if (name != null)
            {
                company = company.Where(a => a.CompanyName.Contains(name));
            }
            if (locationId != null)
            {
                company = company.Where(a => a.LocationID == locationId);
            }
            if (ZoneId != null)
            {
                company = company.Where(a => a.ListCompanyZone.Any(b => b.ZoneID == ZoneId));
            }

            return company;
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