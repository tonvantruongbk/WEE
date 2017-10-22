
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class JobTypeController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<JobType> GetJobType()
        {
            return db.JobType;
        }
 
        [ResponseType(typeof(JobType))]
        public IHttpActionResult GetJobType(int id)
        {
            JobType JobType = db.JobType.Find(id);
            if (JobType == null)
            {
                return NotFound();
            }

            return Ok(JobType);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobType(int id, JobType JobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != JobType.JobTypeID)
            {
                return BadRequest();
            }

            db.Entry(JobType).State = EntityState.Modified;

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
 
        [ResponseType(typeof(JobType))]
        public IHttpActionResult PostJobType(JobType JobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobType.Add(JobType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = JobType.JobTypeID }, JobType);
        }
 
        [ResponseType(typeof(JobType))]
        public IHttpActionResult DeleteJobType(int id)
        {
            JobType JobType = db.JobType.Find(id);
            if (JobType == null)
            {
                return NotFound();
            }

            db.JobType.Remove(JobType);
            db.SaveChanges();

            return Ok(JobType);
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