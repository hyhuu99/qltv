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
    public class TKTKsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: TKTKs
        public ActionResult Index()
        {
            return View(db.TKTKs.ToList());
        }

        // GET: TKTKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TKTK tKTK = db.TKTKs.Find(id);
            if (tKTK == null)
            {
                return HttpNotFound();
            }
            return View(tKTK);
        }

        // GET: TKTKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TKTKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKHO,NGAY,SLTON")] TKTK tKTK)
        {
            if (ModelState.IsValid)
            {
                db.TKTKs.Add(tKTK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tKTK);
        }

        // GET: TKTKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TKTK tKTK = db.TKTKs.Find(id);
            if (tKTK == null)
            {
                return HttpNotFound();
            }
            return View(tKTK);
        }

        // POST: TKTKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKHO,NGAY,SLTON")] TKTK tKTK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tKTK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tKTK);
        }

        // GET: TKTKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TKTK tKTK = db.TKTKs.Find(id);
            if (tKTK == null)
            {
                return HttpNotFound();
            }
            return View(tKTK);
        }

        // POST: TKTKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TKTK tKTK = db.TKTKs.Find(id);
            db.TKTKs.Remove(tKTK);
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
