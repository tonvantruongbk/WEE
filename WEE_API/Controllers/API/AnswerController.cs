
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class AnswerController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<Answer> GetAnswer()
        {
            return db.Answer;
        }
 
        [ResponseType(typeof(Answer))]
        public IHttpActionResult GetAnswer(int id)
        {
            Answer Answer = db.Answer.Find(id);
            if (Answer == null)
            {
                return NotFound();
            }

            return Ok(Answer);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnswer(int id, Answer Answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Answer.AnswerID)
            {
                return BadRequest();
            }

            db.Entry(Answer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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
 
        [ResponseType(typeof(Answer))]
        public IHttpActionResult PostAnswer(Answer Answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Answer.Add(Answer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Answer.AnswerID }, Answer);
        }
 
        [ResponseType(typeof(Answer))]
        public IHttpActionResult DeleteAnswer(int id)
        {
            Answer Answer = db.Answer.Find(id);
            if (Answer == null)
            {
                return NotFound();
            }

            db.Answer.Remove(Answer);
            db.SaveChanges();

            return Ok(Answer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswerExists(int id)
        {
            return db.Answer.Count(e => e.AnswerID == id) > 0;
        }
    }
}