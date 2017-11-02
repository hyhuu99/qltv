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
    public class SLDLsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: SLDLs
        public ActionResult Index()
        {
            var sLDLs = db.SLDLs.Include(s => s.DAILY).Include(s => s.SACH);
            return View(sLDLs.ToList());
        }

        // GET: SLDLs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLDL sLDL = db.SLDLs.Find(id);
            if (sLDL == null)
            {
                return HttpNotFound();
            }
            return View(sLDL);
        }

        // GET: SLDLs/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB");
            return View();
        }

        // POST: SLDLs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADL,MAS,SLTON")] SLDL sLDL)
        {
            if (ModelState.IsValid)
            {
                db.SLDLs.Add(sLDL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", sLDL.MADL);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", sLDL.MAS);
            return View(sLDL);
        }

        // GET: SLDLs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLDL sLDL = db.SLDLs.Find(id);
            if (sLDL == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", sLDL.MADL);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", sLDL.MAS);
            return View(sLDL);
        }

        // POST: SLDLs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADL,MAS,SLTON")] SLDL sLDL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sLDL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", sLDL.MADL);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", sLDL.MAS);
            return View(sLDL);
        }

        // GET: SLDLs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLDL sLDL = db.SLDLs.Find(id);
            if (sLDL == null)
            {
                return HttpNotFound();
            }
            return View(sLDL);
        }

        // POST: SLDLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SLDL sLDL = db.SLDLs.Find(id);
            db.SLDLs.Remove(sLDL);
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
