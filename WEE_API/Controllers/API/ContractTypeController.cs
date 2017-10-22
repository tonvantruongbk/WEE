
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{
  
    public class ContractTypeController : ApiController
    {
        private DBContext db = new DBContext();

        public IQueryable<ContractType> GetContractType()
        {
            return db.ContractType;
        }
 
        [ResponseType(typeof(ContractType))]
        public IHttpActionResult GetContractType(int id)
        {
            ContractType ContractType = db.ContractType.Find(id);
            if (ContractType == null)
            {
                return NotFound();
            }

            return Ok(ContractType);
        }
 
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContractType(int id, ContractType ContractType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ContractType.ContractTypeID)
            {
                return BadRequest();
            }

            db.Entry(ContractType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractTypeExists(id))
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
 
        [ResponseType(typeof(ContractType))]
        public IHttpActionResult PostContractType(ContractType ContractType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContractType.Add(ContractType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ContractType.ContractTypeID }, ContractType);
        }
 
        [ResponseType(typeof(ContractType))]
        public IHttpActionResult DeleteContractType(int id)
        {
            ContractType ContractType = db.ContractType.Find(id);
            if (ContractType == null)
            {
                return NotFound();
            }

            db.ContractType.Remove(ContractType);
            db.SaveChanges();

            return Ok(ContractType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContractTypeExists(int id)
        {
            return db.ContractType.Count(e => e.ContractTypeID == id) > 0;
        }
    }
}