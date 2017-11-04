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
    public class PHIEUNHAPSACHesController : Controller
    {
        private QLTVEntities db = new QLTVEntities();

        // GET: PHIEUNHAPSACHes
        public ActionResult Index()
        {
            var pHIEUNHAPSACHes = db.PHIEUNHAPSACHes.Include(p => p.NXB);
            return View(pHIEUNHAPSACHes.ToList());
        }

        // GET: PHIEUNHAPSACHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSACH phieunhap = db.PHIEUNHAPSACHes.Find(id);
            phieunhap.CTPNS = db.CTPNS.Include(s => s.SACH).Where(o => o.MAPNS == id).ToList();
            //List<CTPN> ctpn = new List<CTPN>();
            //var query = (from u in db.CTPNS
            //            where u.MAPNS==id
            //            select u).Include(s => s.SACH);
            //foreach (var row in query)
            //{
            //    CTPN pn = new CTPN();
            //    pn.MAPNS = row.MAPNS;
            //    pn.MAS = row.MAS;
            //    pn.SOLUONGN = row.SOLUONGN;
            //    pn.TONG = row.TONG;
            //    ctpn.Add(pn);
            //}
            //phieunhap.CTPNS = ctpn;  
            if (phieunhap == null)
            {
                return HttpNotFound();
            }
           
            return View(phieunhap);
        }

        // GET: PHIEUNHAPSACHes/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "TENS");
            return View();
        }

        // POST: PHIEUNHAPSACHes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Prefix = "phieunhaps")] PHIEUNHAPSACH phieunhaps,
                                    [Bind(Prefix = "ct")] CTPN[] ctpn)
        {
            if (ModelState.IsValid)
            {
                int mapn = 1;
                if (db.PHIEUNHAPSACHes.Any())
                    mapn = db.PHIEUNHAPSACHes.Max(o => o.MAPNS) + 1;                
                foreach(CTPN ct in ctpn)
                {
                    ct.MAPNS = mapn;
                    //TKTK tk = new TKTK();
                    //tk.NGAY = DateTime.Now;                  
                    //TKTK tktk = db.TKTKs.OrderByDescending(o => o.MAKHO).FirstOrDefault(o => o.MAS == ct.MAS);
                    //if (tktk != null)
                    //{
                    //    tk.MAS = ct.MAS;
                    //    tk.SLTON = tk.SLTON + ct.SOLUONGN;
                    //}
                    //db.TKTKs.Add(tk);
                    SACH s = new SACH();
                    s = db.SACHes.Find(ct.MAS);
                    s.SOLUONG = s.SOLUONG + ct.SOLUONGN;
                    ct.TONG = ct.SOLUONGN * s.GIANHAP;
                    phieunhaps.THANHTIEN += ct.TONG;                  
                }
                NXB nxb = db.NXBs.Find(phieunhaps.MANXB);
                nxb.SOTIENNO += phieunhaps.THANHTIEN;
                phieunhaps.CTPNS = ctpn;
                db.PHIEUNHAPSACHes.Add(phieunhaps);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", phieunhaps.MANXB);
            return View(phieunhaps);
        }

        // GET: PHIEUNHAPSACHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSACH phieunhaps = db.PHIEUNHAPSACHes.Find(id);
            phieunhaps.CTPNS = db.CTPNS.Include(s => s.SACH).Where(o => o.MAPNS == id).ToList();
            if (phieunhaps == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", phieunhaps.MANXB);
            return View(phieunhaps);
        }

        // POST: PHIEUNHAPSACHes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPNS,MANXB,NGUOIGS,NGAYNHAP,THANHTIEN")] PHIEUNHAPSACH pHIEUNHAPSACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUNHAPSACH).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUNHAPSACH.MANXB);
            return View(pHIEUNHAPSACH);
        }

        // GET: PHIEUNHAPSACHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSACH pHIEUNHAPSACH = db.PHIEUNHAPSACHes.Find(id);
            if (pHIEUNHAPSACH == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPSACH);
        }

        // POST: PHIEUNHAPSACHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUNHAPSACH pHIEUNHAPSACH = db.PHIEUNHAPSACHes.Find(id);
            db.PHIEUNHAPSACHes.Remove(pHIEUNHAPSACH);
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
