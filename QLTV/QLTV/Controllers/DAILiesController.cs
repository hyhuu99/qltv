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
    public class DAILiesController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: DAILies
        public ActionResult Index()
        {
            return View(db.DAILies.ToList());
        }

        // GET: DAILies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = db.DAILies.Find(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // GET: DAILies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DAILies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADL,TENDL,DIACHI,SDT,SOTIENNO")] DAILY dAILY)
        {
            if (ModelState.IsValid)
            {
                db.DAILies.Add(dAILY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dAILY);
        }

        // GET: DAILies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = db.DAILies.Find(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // POST: DAILies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADL,TENDL,DIACHI,SDT,SOTIENNO")] DAILY dAILY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dAILY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dAILY);
        }

        // GET: DAILies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = db.DAILies.Find(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // POST: DAILies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DAILY dAILY = db.DAILies.Find(id);
            db.DAILies.Remove(dAILY);
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
