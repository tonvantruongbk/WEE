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
  
    public class JobTypeController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Loại công việc, VD: C#, Java
        /// </summary>
        public IQueryable<JobType> GetJobType()
        {
            return db.JobType;
        }

        // GET: api/JobType/5
        [ResponseType(typeof(JobType))]
        public IHttpActionResult GetJobType(int id)
        {
            JobType jobType = db.JobType.Find(id);
            if (jobType == null)
            {
                return NotFound();
            }

            return Ok(jobType);
        }

        // PUT: api/JobType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobType(int id, JobType jobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobType.JobTypeID)
            {
                return BadRequest();
            }

            db.Entry(jobType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTypeExists(id))
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

        // POST: api/JobType
        [ResponseType(typeof(JobType))]
        public IHttpActionResult PostJobType(JobType jobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobType.Add(jobType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobType.JobTypeID }, jobType);
        }

        // DELETE: api/JobType/5
        [ResponseType(typeof(JobType))]
        public IHttpActionResult DeleteJobType(int id)
        {
            JobType jobType = db.JobType.Find(id);
            if (jobType == null)
            {
                return NotFound();
            }

            db.JobType.Remove(jobType);
            db.SaveChanges();

            return Ok(jobType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobTypeExists(int id)
        {
            return db.JobType.Count(e => e.JobTypeID == id) > 0;
        }
    }
}