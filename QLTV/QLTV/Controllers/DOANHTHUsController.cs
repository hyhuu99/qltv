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
            var dOANHTHUs = db.DOANHTHUs.Include(d => d.NXB);
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
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            return View();
        }

        // POST: DOANHTHUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADT,MANXB,NGAY,SOTIENNXB")] DOANHTHU doanhthu)
        {
            if (ModelState.IsValid)
            {
                NXB nxb = db.NXBs.Find(doanhthu.MANXB);
                nxb.SOTIENNO -= doanhthu.SOTIENNXB;
                db.DOANHTHUs.Add(doanhthu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", doanhthu.MANXB);
            return View(doanhthu);
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
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", dOANHTHU.MANXB);
            return View(dOANHTHU);
        }

        // POST: DOANHTHUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADT,MANXB,NGAY,SOTIENNXB")] DOANHTHU dOANHTHU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOANHTHU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", dOANHTHU.MANXB);
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
