using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTV.Models;
using System.Diagnostics;

namespace QLTV.Controllers
{
    public class PHIEUXUATSACHesController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: PHIEUXUATSACHes
        public ActionResult Index()
        {
            var pHIEUXUATSACHes = db.PHIEUXUATSACHes.Include(p => p.DAILY);
            return View(pHIEUXUATSACHes.ToList());
        }

        // GET: PHIEUXUATSACHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUATSACH phieuxuat= db.PHIEUXUATSACHes.Find(id);
            phieuxuat.CTPXS = db.CTPXS.Include(s => s.SACH).Where(o => o.MAPXS == id).ToList();
            if (phieuxuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuxuat);
        }

        // GET: PHIEUXUATSACHes/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "TENS");
            return View();
        }

        // POST: PHIEUXUATSACHes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Prefix = "phieuxuats")] PHIEUXUATSACH phieuxuats,
                                    [Bind(Prefix = "ct")] CTPX[] ctpx)
        {
            if (ModelState.IsValid)
            {
                int mapx = 1;
                if (db.PHIEUXUATSACHes.Any())
                    mapx = db.PHIEUXUATSACHes.Max(o => o.MAPXS) + 1;
                DAILY dl = new DAILY();
                dl = db.DAILies.Find(phieuxuats.MADL);
                Debug.Write(" ma phieu xuat:" + mapx);
                foreach (CTPX ct in ctpx)
                {
                    ct.MAPXS = mapx;
                    SACH s = new SACH();
                    s = db.SACHes.Find(ct.MAS);
                                     
                    if(s.SOLUONG>ct.SOLUONGN)
                    {
                        s.SOLUONG = s.SOLUONG - ct.SOLUONGN;
                        ct.TONG = ct.SOLUONGN * s.GIABAN;
                        phieuxuats.THANHTIEN += ct.TONG;
                        SLDL sl = db.SLDLs.Where(c => c.MAS == ct.MAS && c.MADL == phieuxuats.MADL).FirstOrDefault();
                        sl.SLTON += ct.SOLUONGN;
                        db.Entry(sl).State = EntityState.Modified;                    
                    }
                    else
                    {
                        ModelState.AddModelError("", "Số lượng sách hiện có không đủ");
                        ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", phieuxuats.MADL);
                        ViewBag.MANXB = new SelectList(db.NXBs, "MADL", "TENDL", phieuxuats.MADL);
                        return View(phieuxuats);
                    }
                }
                dl.SOTIENNO += phieuxuats.THANHTIEN;
                db.Entry(dl).State = EntityState.Modified;
                phieuxuats.CTPXS = ctpx;
                db.PHIEUXUATSACHes.Add(phieuxuats);              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", phieuxuats.MADL);
            return View(phieuxuats);
        }

        // GET: PHIEUXUATSACHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUATSACH pHIEUXUATSACH = db.PHIEUXUATSACHes.Find(id);
            if (pHIEUXUATSACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUXUATSACH.MADL);
            return View(pHIEUXUATSACH);
        }

        // POST: PHIEUXUATSACHes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPXS,MADL,NGUOINS,NGAYXUAT,THANHTIEN")] PHIEUXUATSACH pHIEUXUATSACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUXUATSACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUXUATSACH.MADL);
            return View(pHIEUXUATSACH);
        }

        // GET: PHIEUXUATSACHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUATSACH pHIEUXUATSACH = db.PHIEUXUATSACHes.Find(id);
            if (pHIEUXUATSACH == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUXUATSACH);
        }

        // POST: PHIEUXUATSACHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUXUATSACH pHIEUXUATSACH = db.PHIEUXUATSACHes.Find(id);
            db.PHIEUXUATSACHes.Remove(pHIEUXUATSACH);
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
