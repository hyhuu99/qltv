using QLTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Controllers
{
    public class thongketonkhoController : Controller
    {
        private QLTVEntities db = new QLTVEntities();
        // GET: thongketonkho
        public ActionResult Index(string ngay, string MAS)
        {
            ViewBag.MAS = new SelectList(db.SACHes, "MAS", "TENS");
            tonkho tktk = new tonkho();
            tktk.dt = DateTime.Now;
            DateTime searchDate;
            if (DateTime.TryParse(ngay, out searchDate))
            {
                List<SACH> sach = new List<SACH>();
                sach = db.SACHes.Where(o => o.MAS == MAS).ToList();
                foreach(SACH o in sach)
                {
                    int tongslxuat = (int)db.CTPXS.Where(ct => ct.MAS == o.MAS && ct.PHIEUXUATSACH.NGAYXUAT > searchDate)
                                                 .Select(ct => ct.SOLUONGN)
                                                 .DefaultIfEmpty(0)
                                                 .Sum();
                    int tongslnhan = (int)db.CTPNS.Where(ct => ct.MAS == o.MAS && ct.PHIEUNHAPSACH.NGAYNHAP > searchDate)
                                                  .Select(ct => ct.SOLUONGN)
                                                  .DefaultIfEmpty(0)
                                                  .Sum();
                    o.SOLUONG = o.SOLUONG + tongslxuat - tongslnhan;
                }
                tktk.sach = sach;
                return View(tktk);
            }
            tktk.sach = db.SACHes.ToList();
            return View();
        }
    }
}