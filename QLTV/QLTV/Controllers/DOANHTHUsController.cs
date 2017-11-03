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
    public class DOANHTHUsController : ApiController
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: api/DOANHTHUs
        public IQueryable<DOANHTHU> GetDOANHTHUs()
        {
            return db.DOANHTHUs;
        }

        // GET: api/DOANHTHUs/5
        [ResponseType(typeof(DOANHTHU))]
        public IHttpActionResult GetDOANHTHU(int id)
        {
            DOANHTHU dOANHTHU = db.DOANHTHUs.Find(id);
            if (dOANHTHU == null)
            {
                return NotFound();
            }

            return Ok(dOANHTHU);
        }

        // PUT: api/DOANHTHUs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDOANHTHU(int id, DOANHTHU dOANHTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dOANHTHU.MADT)
            {
                return BadRequest();
            }

            db.Entry(dOANHTHU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DOANHTHUExists(id))
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

        // POST: api/DOANHTHUs
        [ResponseType(typeof(DOANHTHU))]
        public IHttpActionResult PostDOANHTHU(DOANHTHU dOANHTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DOANHTHUs.Add(dOANHTHU);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dOANHTHU.MADT }, dOANHTHU);
        }

        // DELETE: api/DOANHTHUs/5
        [ResponseType(typeof(DOANHTHU))]
        public IHttpActionResult DeleteDOANHTHU(int id)
        {
            DOANHTHU dOANHTHU = db.DOANHTHUs.Find(id);
            if (dOANHTHU == null)
            {
                return NotFound();
            }

            db.DOANHTHUs.Remove(dOANHTHU);
            db.SaveChanges();

            return Ok(dOANHTHU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DOANHTHUExists(int id)
        {
            return db.DOANHTHUs.Count(e => e.MADT == id) > 0;
        }
    }
}