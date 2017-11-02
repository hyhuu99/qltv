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
    public class CTPXesController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: CTPXes
        public ActionResult Index()
        {
            var cTPXS = db.CTPXS.Include(c => c.PHIEUXUATSACH).Include(c => c.SACH);
            return View(cTPXS.ToList());
        }

        // GET: CTPXes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPX cTPX = db.CTPXS.Find(id);
            if (cTPX == null)
            {
                return HttpNotFound();
            }
            return View(cTPX);
        }

        // GET: CTPXes/Create
        public ActionResult Create()
        {
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL");
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB");
            return View();
        }

        // POST: CTPXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPXS,MAS,SOLUONGN,TONG")] CTPX cTPX)
        {
            if (ModelState.IsValid)
            {
                db.CTPXS.Add(cTPX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL", cTPX.MAPXS);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", cTPX.MAS);
            return View(cTPX);
        }

        // GET: CTPXes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPX cTPX = db.CTPXS.Find(id);
            if (cTPX == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL", cTPX.MAPXS);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", cTPX.MAS);
            return View(cTPX);
        }

        // POST: CTPXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPXS,MAS,SOLUONGN,TONG")] CTPX cTPX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPXS = new SelectList(db.PHIEUXUATSACHes, "MAPXS", "MADL", cTPX.MAPXS);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", cTPX.MAS);
            return View(cTPX);
        }

        // GET: CTPXes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPX cTPX = db.CTPXS.Find(id);
            if (cTPX == null)
            {
                return HttpNotFound();
            }
            return View(cTPX);
        }

        // POST: CTPXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTPX cTPX = db.CTPXS.Find(id);
            db.CTPXS.Remove(cTPX);
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
