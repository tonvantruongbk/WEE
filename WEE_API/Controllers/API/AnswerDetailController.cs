
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class AnswerDetailController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<AnswerDetail> GetAnswerDetail()
        {
            return db.AnswerDetail;
        }
 
        [ResponseType(typeof(AnswerDetail))]
        public IHttpActionResult GetAnswerDetail(int id)
        {
            AnswerDetail AnswerDetail = db.AnswerDetail.Find(id);
            if (AnswerDetail == null)
            {
                return NotFound();
            }

            return Ok(AnswerDetail);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnswerDetail(int id, AnswerDetail AnswerDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != AnswerDetail.AnswerDetailID)
            {
                return BadRequest();
            }

            db.Entry(AnswerDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerDetailExists(id))
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
 
        [ResponseType(typeof(AnswerDetail))]
        public IHttpActionResult PostAnswerDetail(AnswerDetail AnswerDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnswerDetail.Add(AnswerDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = AnswerDetail.AnswerDetailID }, AnswerDetail);
        }
 
        [ResponseType(typeof(AnswerDetail))]
        public IHttpActionResult DeleteAnswerDetail(int id)
        {
            AnswerDetail AnswerDetail = db.AnswerDetail.Find(id);
            if (AnswerDetail == null)
            {
                return NotFound();
            }

            db.AnswerDetail.Remove(AnswerDetail);
            db.SaveChanges();

            return Ok(AnswerDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswerDetailExists(int id)
        {
            return db.AnswerDetail.Count(e => e.AnswerDetailID == id) > 0;
        }
    }
}