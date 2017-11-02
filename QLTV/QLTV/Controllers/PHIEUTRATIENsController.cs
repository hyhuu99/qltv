using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTV.Models;

namespace QLTV.Controllers
{
    public class PHIEUTRATIENsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: PHIEUTRATIENs
        public ActionResult Index()
        {
            var pHIEUTRATIENs = db.PHIEUTRATIENs.Include(p => p.DAILY).Include(p => p.PHIEUXUATSACH);
            return View(pHIEUTRATIENs.ToList());
        }

        // GET: PHIEUTRATIENs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRATIEN pHIEUTRATIEN = db.PHIEUTRATIENs.Find(id);
            if (pHIEUTRATIEN == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTRATIEN);
        }

        // GET: PHIEUTRATIENs/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL");
            return View();
        }

        // POST: PHIEUTRATIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATK,MAPXS,MADL,NGAY,SOTIENTRA,SOTIENNO,TRANGTHAI")] PHIEUTRATIEN pHIEUTRATIEN)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUTRATIENs.Add(pHIEUTRATIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUTRATIEN.MADL);
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL", pHIEUTRATIEN.MAPXS);
            return View(pHIEUTRATIEN);
        }

        // GET: PHIEUTRATIENs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRATIEN pHIEUTRATIEN = db.PHIEUTRATIENs.Find(id);
            if (pHIEUTRATIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUTRATIEN.MADL);
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL", pHIEUTRATIEN.MAPXS);
            return View(pHIEUTRATIEN);
        }

        // POST: PHIEUTRATIENs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATK,MAPXS,MADL,NGAY,SOTIENTRA,SOTIENNO,TRANGTHAI")] PHIEUTRATIEN pHIEUTRATIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUTRATIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUTRATIEN.MADL);
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL", pHIEUTRATIEN.MAPXS);
            return View(pHIEUTRATIEN);
        }

        // GET: PHIEUTRATIENs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRATIEN pHIEUTRATIEN = db.PHIEUTRATIENs.Find(id);
            if (pHIEUTRATIEN == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTRATIEN);
        }

        // POST: PHIEUTRATIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUTRATIEN pHIEUTRATIEN = db.PHIEUTRATIENs.Find(id);
            db.PHIEUTRATIENs.Remove(pHIEUTRATIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
