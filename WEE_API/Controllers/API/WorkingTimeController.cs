
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class WorkingTimeController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<WorkingTime> GetWorkingTime()
        {
            return db.WorkingTime;
        }
 
        [ResponseType(typeof(WorkingTime))]
        public IHttpActionResult GetWorkingTime(int id)
        {
            WorkingTime WorkingTime = db.WorkingTime.Find(id);
            if (WorkingTime == null)
            {
                return NotFound();
            }

            return Ok(WorkingTime);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkingTime(int id, WorkingTime WorkingTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != WorkingTime.WorkingTimeID)
            {
                return BadRequest();
            }

            db.Entry(WorkingTime).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkingTimeExists(id))
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
 
        [ResponseType(typeof(WorkingTime))]
        public IHttpActionResult PostWorkingTime(WorkingTime WorkingTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkingTime.Add(WorkingTime);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = WorkingTime.WorkingTimeID }, WorkingTime);
        }
 
        [ResponseType(typeof(WorkingTime))]
        public IHttpActionResult DeleteWorkingTime(int id)
        {
            WorkingTime WorkingTime = db.WorkingTime.Find(id);
            if (WorkingTime == null)
            {
                return NotFound();
            }

            db.WorkingTime.Remove(WorkingTime);
            db.SaveChanges();

            return Ok(WorkingTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkingTimeExists(int id)
        {
            return db.WorkingTime.Count(e => e.WorkingTimeID == id) > 0;
        }
    }
}