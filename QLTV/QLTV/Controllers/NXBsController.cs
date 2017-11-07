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
    public class NXBsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: NXBs
        public ActionResult Index()
        {
            return View(db.NXBs.ToList());
        }

        // GET: NXBs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // GET: NXBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NXBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANXB,TENNXB,DIACHI,SDT,STK,SOTIENNO")] NXB nXB)
        {
            if (ModelState.IsValid)
            {
                db.NXBs.Add(nXB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nXB);
        }

        // GET: NXBs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: NXBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANXB,TENNXB,DIACHI,SDT,STK")] NXB nXB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nXB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nXB);
        }

        // GET: NXBs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: NXBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NXB nXB = db.NXBs.Find(id);
            db.NXBs.Remove(nXB);
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
