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
  
    public class UserRatingCompanyController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Người dùng bình chọn cho cty
        /// </summary>
        public IQueryable<UserRatingCompany> GetUserRatingCompany()
        {
            return db.UserRatingCompany;
        }

        [ResponseType(typeof(UserRatingCompany))]
        public IHttpActionResult GetUserRatingCompany(int id)
        {
            UserRatingCompany userRatingCompany = db.UserRatingCompany.Find(id);
            if (userRatingCompany == null)
            {
                return NotFound();
            }

            return Ok(userRatingCompany);
        }

        // PUT: api/UserRatingCompany/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserRatingCompany(int id, UserRatingCompany userRatingCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRatingCompany.QuestionTypeID)
            {
                return BadRequest();
            }

            db.Entry(userRatingCompany).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRatingCompanyExists(id))
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

        // POST: api/UserRatingCompany
        [ResponseType(typeof(UserRatingCompany))]
        public IHttpActionResult PostUserRatingCompany(UserRatingCompany userRatingCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserRatingCompany.Add(userRatingCompany);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserRatingCompanyExists(userRatingCompany.QuestionTypeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userRatingCompany.QuestionTypeID }, userRatingCompany);
        }

        // DELETE: api/UserRatingCompany/5
        [ResponseType(typeof(UserRatingCompany))]
        public IHttpActionResult DeleteUserRatingCompany(int id)
        {
            UserRatingCompany userRatingCompany = db.UserRatingCompany.Find(id);
            if (userRatingCompany == null)
            {
                return NotFound();
            }

            db.UserRatingCompany.Remove(userRatingCompany);
            db.SaveChanges();

            return Ok(userRatingCompany);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserRatingCompanyExists(int id)
        {
            return db.UserRatingCompany.Count(e => e.QuestionTypeID == id) > 0;
        }
    }
}