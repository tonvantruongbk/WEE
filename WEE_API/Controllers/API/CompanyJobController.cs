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
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class CompanyJobController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Danh mục các Job của 1 cty: VD cty A đang tuyển dụng Designer
        /// </summary>
        public IQueryable<CompanyJob> GetCompanyJob()
        {
            return db.CompanyJob;
        }

        // GET: api/CompanyJob/5
        [ResponseType(typeof(CompanyJob))]
        public IHttpActionResult GetCompanyJob(int id)
        {
            CompanyJob companyJob = db.CompanyJob.Find(id);
            if (companyJob == null)
            {
                return NotFound();
            }

            return Ok(companyJob);
        }

        // PUT: api/CompanyJob/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyJob(int id, CompanyJob companyJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyJob.CompanyID)
            {
                return BadRequest();
            }

            db.Entry(companyJob).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyJobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CompanyJob
        [ResponseType(typeof(CompanyJob))]
        public IHttpActionResult PostCompanyJob(CompanyJob companyJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompanyJob.Add(companyJob);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CompanyJobExists(companyJob.CompanyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = companyJob.CompanyID }, companyJob);
        }

        // DELETE: api/CompanyJob/5
        [ResponseType(typeof(CompanyJob))]
        public IHttpActionResult DeleteCompanyJob(int id)
        {
            CompanyJob companyJob = db.CompanyJob.Find(id);
            if (companyJob == null)
            {
                return NotFound();
            }

            db.CompanyJob.Remove(companyJob);
            db.SaveChanges();

            return Ok(companyJob);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyJobExists(int id)
        {
            return db.CompanyJob.Count(e => e.CompanyID == id) > 0;
        }
    }
}