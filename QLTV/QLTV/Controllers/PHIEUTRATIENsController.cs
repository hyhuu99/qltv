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
    public class PHIEUTRATIENsController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: PHIEUTRATIENs
        public ActionResult Index()
        {
            var pHIEUTRATIENs = db.PHIEUTRATIENs.Include(p => p.DAILY);
            return View(pHIEUTRATIENs.ToList());
        }

        // GET: PHIEUTRATIENs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRATIEN ptt = db.PHIEUTRATIENs.Find(id);
            ptt.CTPTTs = db.CTPTTs.Include(s => s.SACH).Where(o => o.MATK == id).ToList();
            if (ptt == null)
            {
                return HttpNotFound();
            }
            return View(ptt);
        }
        public ActionResult thanhtoan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRATIEN ptt = db.PHIEUTRATIENs.Find(id);
            ptt.TRANGTHAI = 1;
            db.Entry(ptt).State = EntityState.Modified;
            List<CTPTT> ctptt = new List<CTPTT>();
            ctptt = ptt.CTPTTs.ToList();
            //foreach(CTPTT ct in ctptt)
            //{
            //    NXB nxb = db.NXBs.Find(ct.SACH.MANXB);
            //    nxb.SOTIENNO -= ct.TONG;
            //    db.Entry(nxb).State = EntityState.Modified;
               
            //}
            DAILY dl = db.DAILies.Find(ptt.MADL);
            dl.SOTIENNO -= ptt.SOTIENNO;
            db.Entry(dl).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.AddModelError(" ", "Đã thanh toán thành công");
            return RedirectToAction("Index");

        }

            // GET: PHIEUTRATIENs/Create
            public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "TENS");
            return View();
        }


        // POST: PHIEUTRATIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Prefix = "phieutratiens")] PHIEUTRATIEN ptt,
                                    [Bind(Prefix = "ct")] CTPTT[] ctptt)
        {
            if (ModelState.IsValid)
            {
                int maptt = 1;
                if (db.PHIEUTRATIENs.Any())
                    maptt = db.PHIEUTRATIENs.Max(o => o.MATK) + 1;
                Debug.WriteLine(ctptt.Length);
                foreach(CTPTT ct in ctptt)
                {
                    ct.MATK = maptt;
                    SLDL sl = db.SLDLs.FirstOrDefault(o => o.MADL == ptt.MADL && o.MAS == ct.MAS);
                    SACH s = new SACH();
                    s = db.SACHes.Find(ct.MAS);
                    if (sl!=null && sl.SLTON>0)
                    {
                        sl.SLTON = sl.SLTON - ct.SOLUONGN;
                        db.Entry(sl).State = EntityState.Modified;
                        ct.TONG = ct.SOLUONGN * s.GIABAN;
                        ptt.SOTIENNO += ct.TONG;
                    }
                    else
                    {
                        ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
                        ViewBag.MAS = new SelectList(db.SACHes, "MAS", "TENS");
                        ModelState.AddModelError("", "số sách đã bán lớn hơn số sách xuất cho đại lý");
                        return View();
                    }
                }
                ptt.CTPTTs = ctptt;
                ptt.TRANGTHAI = 0;
                db.PHIEUTRATIENs.Add(ptt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", ptt.MADL);
            return View(ptt);
        }

        // GET: PHIEUTRATIENs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRATIEN ptt = db.PHIEUTRATIENs.Find(id);
            if (ptt == null)
            {
                return HttpNotFound();
            }
            sachdaban sdb = new sachdaban();
            sdb.phieutratiens = ptt;
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", ptt.MADL);
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "TENS");
            return View(sdb);
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
