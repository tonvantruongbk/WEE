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
 
    public class QuestionTypeController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Quản lý loại câu hỏi, VD Công việc & sự nghiệp, Lương & Phúc lợi
        /// </summary>
        public IQueryable<QuestionType> GetQuestionType()
        {
            return db.QuestionType;
        }

        // GET: api/QuestionType/5
        [ResponseType(typeof(QuestionType))]
        public IHttpActionResult GetQuestionType(int id)
        {
            QuestionType questionType = db.QuestionType.Find(id);
            if (questionType == null)
            {
                return NotFound();
            }

            return Ok(questionType);
        }

        // PUT: api/QuestionType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestionType(int id, QuestionType questionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionType.QuestionTypeID)
            {
                return BadRequest();
            }

            db.Entry(questionType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionTypeExists(id))
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

        // POST: api/QuestionType
        [ResponseType(typeof(QuestionType))]
        public IHttpActionResult PostQuestionType(QuestionType questionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QuestionType.Add(questionType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = questionType.QuestionTypeID }, questionType);
        }

        // DELETE: api/QuestionType/5
        [ResponseType(typeof(QuestionType))]
        public IHttpActionResult DeleteQuestionType(int id)
        {
            QuestionType questionType = db.QuestionType.Find(id);
            if (questionType == null)
            {
                return NotFound();
            }

            db.QuestionType.Remove(questionType);
            db.SaveChanges();

            return Ok(questionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionTypeExists(int id)
        {
            return db.QuestionType.Count(e => e.QuestionTypeID == id) > 0;
        }
    }
}