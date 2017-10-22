
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class WorkingStatusController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<WorkingStatus> GetWorkingStatus()
        {
            return db.WorkingStatus;
        }
 
        [ResponseType(typeof(WorkingStatus))]
        public IHttpActionResult GetWorkingStatus(int id)
        {
            WorkingStatus WorkingStatus = db.WorkingStatus.Find(id);
            if (WorkingStatus == null)
            {
                return NotFound();
            }

            return Ok(WorkingStatus);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkingStatus(int id, WorkingStatus WorkingStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != WorkingStatus.WorkingStatusID)
            {
                return BadRequest();
            }

            db.Entry(WorkingStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkingStatusExists(id))
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
 
        [ResponseType(typeof(WorkingStatus))]
        public IHttpActionResult PostWorkingStatus(WorkingStatus WorkingStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkingStatus.Add(WorkingStatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = WorkingStatus.WorkingStatusID }, WorkingStatus);
        }
 
        [ResponseType(typeof(WorkingStatus))]
        public IHttpActionResult DeleteWorkingStatus(int id)
        {
            WorkingStatus WorkingStatus = db.WorkingStatus.Find(id);
            if (WorkingStatus == null)
            {
                return NotFound();
            }

            db.WorkingStatus.Remove(WorkingStatus);
            db.SaveChanges();

            return Ok(WorkingStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkingStatusExists(int id)
        {
            return db.WorkingStatus.Count(e => e.WorkingStatusID == id) > 0;
        }
    }
}