
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class SalaryLevelController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<SalaryLevel> GetSalaryLevel()
        {
            return db.SalaryLevel;
        }
 
        [ResponseType(typeof(SalaryLevel))]
        public IHttpActionResult GetSalaryLevel(int id)
        {
            SalaryLevel SalaryLevel = db.SalaryLevel.Find(id);
            if (SalaryLevel == null)
            {
                return NotFound();
            }

            return Ok(SalaryLevel);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalaryLevel(int id, SalaryLevel SalaryLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != SalaryLevel.SalaryLevelID)
            {
                return BadRequest();
            }

            db.Entry(SalaryLevel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryLevelExists(id))
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
 
        [ResponseType(typeof(SalaryLevel))]
        public IHttpActionResult PostSalaryLevel(SalaryLevel SalaryLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalaryLevel.Add(SalaryLevel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = SalaryLevel.SalaryLevelID }, SalaryLevel);
        }
 
        [ResponseType(typeof(SalaryLevel))]
        public IHttpActionResult DeleteSalaryLevel(int id)
        {
            SalaryLevel SalaryLevel = db.SalaryLevel.Find(id);
            if (SalaryLevel == null)
            {
                return NotFound();
            }

            db.SalaryLevel.Remove(SalaryLevel);
            db.SaveChanges();

            return Ok(SalaryLevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalaryLevelExists(int id)
        {
            return db.SalaryLevel.Count(e => e.SalaryLevelID == id) > 0;
        }
    }
}