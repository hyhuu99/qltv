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
    public class CTPNsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: CTPNs
        public ActionResult Index()
        {
            var cTPNS = db.CTPNS.Include(c => c.PHIEUNHAPSACH).Include(c => c.SACH);
            return View(cTPNS.ToList());
        }

        // GET: CTPNs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPN cTPN = db.CTPNS.Find(id);
            if (cTPN == null)
            {
                return HttpNotFound();
            }
            return View(cTPN);
        }

        // GET: CTPNs/Create
        public ActionResult Create()
        {
            ViewBag.MAPNS = new SelectList(db.PHIEUNHAPSACHes, "MAPNS", "MANXB");
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB");
            return View();
        }

        // POST: CTPNs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPNS,MAS,SOLUONGN,TONG")] CTPN cTPN)
        {
            if (ModelState.IsValid)
            {
                db.CTPNS.Add(cTPN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAPNS = new SelectList(db.PHIEUNHAPSACHes, "MAPNS", "MANXB", cTPN.MAPNS);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", cTPN.MAS);
            return View(cTPN);
        }

        // GET: CTPNs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPN cTPN = db.CTPNS.Find(id);
            if (cTPN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPNS = new SelectList(db.PHIEUNHAPSACHes, "MAPNS", "MANXB", cTPN.MAPNS);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", cTPN.MAS);
            return View(cTPN);
        }

        // POST: CTPNs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPNS,MAS,SOLUONGN,TONG")] CTPN cTPN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPNS = new SelectList(db.PHIEUNHAPSACHes, "MAPNS", "MANXB", cTPN.MAPNS);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "MANXB", cTPN.MAS);
            return View(cTPN);
        }

        // GET: CTPNs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPN cTPN = db.CTPNS.Find(id);
            if (cTPN == null)
            {
                return HttpNotFound();
            }
            return View(cTPN);
        }

        // POST: CTPNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTPN cTPN = db.CTPNS.Find(id);
            db.CTPNS.Remove(cTPN);
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
