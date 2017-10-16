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
  
    public class JobController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Công việc, VD: Lập trình viên, Designer
        /// </summary>
        public IQueryable<Job> GetJob()
        {
            return db.Job;
        }

        // GET: api/Job/5
        [ResponseType(typeof(Job))]
        public IHttpActionResult GetJob(int id)
        {
            Job job = db.Job.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/Job/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.JobID)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Job
        [ResponseType(typeof(Job))]
        public IHttpActionResult PostJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Job.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.JobID }, job);
        }

        // DELETE: api/Job/5
        [ResponseType(typeof(Job))]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Job.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.Job.Remove(job);
            db.SaveChanges();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Job.Count(e => e.JobID == id) > 0;
        }
    }
}