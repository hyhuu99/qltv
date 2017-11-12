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
using QLTV.Models;

namespace QLTV.Controllers
{
    public class CTPNsController : ApiController
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: api/CTPNs
        public IQueryable<CTPN> GetCTPNS()
        {
            return db.CTPNS;
        }

        // GET: api/CTPNs/5
        [ResponseType(typeof(CTPN))]
        public IHttpActionResult GetCTPN(int id)
        {
            CTPN cTPN = db.CTPNS.Find(id);
            if (cTPN == null)
            {
                return NotFound();
            }

            return Ok(cTPN);
        }

        // PUT: api/CTPNs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCTPN(int id, CTPN cTPN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cTPN.MAPNS)
            {
                return BadRequest();
            }

            db.Entry(cTPN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTPNExists(id))
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

        // POST: api/CTPNs
        [ResponseType(typeof(CTPN))]
        public IHttpActionResult PostCTPN(CTPN cTPN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CTPNS.Add(cTPN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CTPNExists(cTPN.MAPNS))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cTPN.MAPNS }, cTPN);
        }

        // DELETE: api/CTPNs/5
        [ResponseType(typeof(CTPN))]
        public IHttpActionResult DeleteCTPN(int id)
        {
            CTPN cTPN = db.CTPNS.Find(id);
            if (cTPN == null)
            {
                return NotFound();
            }

            db.CTPNS.Remove(cTPN);
            db.SaveChanges();

            return Ok(cTPN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CTPNExists(int id)
        {
            return db.CTPNS.Count(e => e.MAPNS == id) > 0;
        }
    }
}