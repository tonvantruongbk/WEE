
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class JobPositionController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<JobPosition> GetJobPosition()
        {
            return db.JobPosition;
        }
 
        [ResponseType(typeof(JobPosition))]
        public IHttpActionResult GetJobPosition(int id)
        {
            JobPosition JobPosition = db.JobPosition.Find(id);
            if (JobPosition == null)
            {
                return NotFound();
            }

            return Ok(JobPosition);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobPosition(int id, JobPosition JobPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != JobPosition.JobPositionID)
            {
                return BadRequest();
            }

            db.Entry(JobPosition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPositionExists(id))
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
 
        [ResponseType(typeof(JobPosition))]
        public IHttpActionResult PostJobPosition(JobPosition JobPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobPosition.Add(JobPosition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = JobPosition.JobPositionID }, JobPosition);
        }
 
        [ResponseType(typeof(JobPosition))]
        public IHttpActionResult DeleteJobPosition(int id)
        {
            JobPosition JobPosition = db.JobPosition.Find(id);
            if (JobPosition == null)
            {
                return NotFound();
            }

            db.JobPosition.Remove(JobPosition);
            db.SaveChanges();

            return Ok(JobPosition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobPositionExists(int id)
        {
            return db.JobPosition.Count(e => e.JobPositionID == id) > 0;
        }
    }
}