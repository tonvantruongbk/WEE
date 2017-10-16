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
    public class UserTypeController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Phân loại người dùng, VD: Người đi làm, Doanh nghiệp, Tổ chức khác
        /// </summary>

        public IQueryable<UserType> GetUserType()
        {
            return db.UserType;
        }

        // GET: api/UserType/5
        [ResponseType(typeof(UserType))]
        public IHttpActionResult GetUserType(int id)
        {
            UserType userType = db.UserType.Find(id);
            if (userType == null)
            {
                return NotFound();
            }

            return Ok(userType);
        }

        // PUT: api/UserType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserType(int id, UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userType.UserTypeID)
            {
                return BadRequest();
            }

            db.Entry(userType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/UserType
        [ResponseType(typeof(UserType))]
        public IHttpActionResult PostUserType(UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserType.Add(userType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userType.UserTypeID }, userType);
        }

        // DELETE: api/UserType/5
        [ResponseType(typeof(UserType))]
        public IHttpActionResult DeleteUserType(int id)
        {
            UserType userType = db.UserType.Find(id);
            if (userType == null)
            {
                return NotFound();
            }

            db.UserType.Remove(userType);
            db.SaveChanges();

            return Ok(userType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTypeExists(int id)
        {
            return db.UserType.Count(e => e.UserTypeID == id) > 0;
        }
    }
}