
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class QuestionController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<Question> GetQuestion()
        {
            return db.Question;
        }
 
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(int id)
        {
            Question Question = db.Question.Find(id);
            if (Question == null)
            {
                return NotFound();
            }

            return Ok(Question);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question Question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Question.QuestionID)
            {
                return BadRequest();
            }

            db.Entry(Question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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
 
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question Question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Question.Add(Question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Question.QuestionID }, Question);
        }
 
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question Question = db.Question.Find(id);
            if (Question == null)
            {
                return NotFound();
            }

            db.Question.Remove(Question);
            db.SaveChanges();

            return Ok(Question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Question.Count(e => e.QuestionID == id) > 0;
        }
    }
}