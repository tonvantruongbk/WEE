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
 
    public class ZoneController : ApiController
    {
        private DBContext db = new DBContext();

        /// <summary>
        /// Quản lý Lĩnh vực của doanh nghiệp, VD: Cty Outsource, Cty Kinh doanh
        /// </summary>
        public IQueryable<Zone> GetZone()
        {
            return db.Zone;
        }

        // GET: api/Zone/5
        [ResponseType(typeof(Zone))]
        public IHttpActionResult GetZone(int id)
        {
            Zone zone = db.Zone.Find(id);
            if (zone == null)
            {
                return NotFound();
            }

            return Ok(zone);
        }

        // PUT: api/Zone/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutZone(int id, Zone zone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zone.ZoneID)
            {
                return BadRequest();
            }

            db.Entry(zone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(id))
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

        // POST: api/Zone
        [ResponseType(typeof(Zone))]
        public IHttpActionResult PostZone(Zone zone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Zone.Add(zone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = zone.ZoneID }, zone);
        }

        // DELETE: api/Zone/5
        [ResponseType(typeof(Zone))]
        public IHttpActionResult DeleteZone(int id)
        {
            Zone zone = db.Zone.Find(id);
            if (zone == null)
            {
                return NotFound();
            }

            db.Zone.Remove(zone);
            db.SaveChanges();

            return Ok(zone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZoneExists(int id)
        {
            return db.Zone.Count(e => e.ZoneID == id) > 0;
        }
    }
}