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
    public class DOANHTHUsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: DOANHTHUs
        public ActionResult Index()
        {
            var dOANHTHUs = db.DOANHTHUs.Include(d => d.PHIEUTRATIEN);
            return View(dOANHTHUs.ToList());
        }

        // GET: DOANHTHUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOANHTHU dOANHTHU = db.DOANHTHUs.Find(id);
            if (dOANHTHU == null)
            {
                return HttpNotFound();
            }
            return View(dOANHTHU);
        }

        // GET: DOANHTHUs/Create
        public ActionResult Create()
        {
            ViewBag.MATK = new SelectList(db.PHIEUTRATIENs, "MATK", "MADL");
            return View();
        }

        // POST: DOANHTHUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADT,MATK,NGAY,SOTIENNXB,DOANHTHU1")] DOANHTHU dOANHTHU)
        {
            if (ModelState.IsValid)
            {
                db.DOANHTHUs.Add(dOANHTHU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATK = new SelectList(db.PHIEUTRATIENs, "MATK", "MADL", dOANHTHU.MATK);
            return View(dOANHTHU);
        }

        // GET: DOANHTHUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOANHTHU dOANHTHU = db.DOANHTHUs.Find(id);
            if (dOANHTHU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATK = new SelectList(db.PHIEUTRATIENs, "MATK", "MADL", dOANHTHU.MATK);
            return View(dOANHTHU);
        }

        // POST: DOANHTHUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADT,MATK,NGAY,SOTIENNXB,DOANHTHU1")] DOANHTHU dOANHTHU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOANHTHU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATK = new SelectList(db.PHIEUTRATIENs, "MATK", "MADL", dOANHTHU.MATK);
            return View(dOANHTHU);
        }

        // GET: DOANHTHUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOANHTHU dOANHTHU = db.DOANHTHUs.Find(id);
            if (dOANHTHU == null)
            {
                return HttpNotFound();
            }
            return View(dOANHTHU);
        }

        // POST: DOANHTHUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DOANHTHU dOANHTHU = db.DOANHTHUs.Find(id);
            db.DOANHTHUs.Remove(dOANHTHU);
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
