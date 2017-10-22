
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class AnswerCategoryController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<Answer> GetAnswerCategory()
        {
            return db.Answer;
        }
 
        [ResponseType(typeof(Answer))]
        public IHttpActionResult GetAnswerCategory(int id)
        {
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return NotFound();
            }

            return Ok(answer);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnswerCategory(int id, Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != answer.AnswerID)
            {
                return BadRequest();
            }

            db.Entry(answer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerCategoryExists(id))
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
        public IHttpActionResult PostAnswerCategory(Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Answer.Add(answer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = answer.AnswerID }, answer);
        }
 
        [ResponseType(typeof(Answer))]
        public IHttpActionResult DeleteAnswerCategory(int id)
        {
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return NotFound();
            }

            db.Answer.Remove(answer);
            db.SaveChanges();

            return Ok(answer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswerCategoryExists(int id)
        {
            return db.Answer.Count(e => e.AnswerID == id) > 0;
        }
    }
}