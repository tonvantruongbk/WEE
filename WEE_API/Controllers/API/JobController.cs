
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class JobController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<Job> GetJob()
        {
            return db.Job;
        }
 
        [ResponseType(typeof(Job))]
        public IHttpActionResult GetJob(int id)
        {
            Job Job = db.Job.Find(id);
            if (Job == null)
            {
                return NotFound();
            }

            return Ok(Job);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJob(int id, Job Job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Job.JobID)
            {
                return BadRequest();
            }

            db.Entry(Job).State = EntityState.Modified;

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
 
        [ResponseType(typeof(Job))]
        public IHttpActionResult PostJob(Job Job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Job.Add(Job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Job.JobID }, Job);
        }
 
        [ResponseType(typeof(Job))]
        public IHttpActionResult DeleteJob(int id)
        {
            Job Job = db.Job.Find(id);
            if (Job == null)
            {
                return NotFound();
            }

            db.Job.Remove(Job);
            db.SaveChanges();

            return Ok(Job);
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