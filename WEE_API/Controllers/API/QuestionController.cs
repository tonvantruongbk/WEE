using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class QuestionController : ApiController
    {
        private DBContext db = new DBContext();
        /// <summary>
        /// Các câu hỏi khi bắt đầu bình chọn, VD: Bạn KHÓ có thể đàm phán NHẤT ở khía cạnh nào 
        /// </summary>
        public IQueryable<Question> GetQuestion()
        {
            return db.Question;
        }

        // GET: api/Question/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(int id)
        {
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Question/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.QuestionID)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

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

        // POST: api/Question
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Question.Add(question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = question.QuestionID }, question);
        }

        // DELETE: api/Question/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Question.Remove(question);
            db.SaveChanges();

            return Ok(question);
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