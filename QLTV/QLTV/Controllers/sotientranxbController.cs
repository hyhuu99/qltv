using QLTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Controllers
{
    public class sotientranxbController : Controller
    {
        // GET: sotientranxb
        private QLTVEntities db = new QLTVEntities();
        public ActionResult Index(String ngay,String MANXB)
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            sotientranxb stt = new sotientranxb();
            stt.dt = DateTime.Now;
            DateTime searchDate;
            if (DateTime.TryParse(ngay, out searchDate))
            {
                List<NXB> nxbs = new List<NXB>();
                nxbs = db.NXBs.Where(o => o.MANXB == MANXB).ToList();
                foreach (NXB o in nxbs)
                {
                    int sotienduoctra = (int)db.CTPTTs.Where(ct => ct.SACH.MANXB == o.MANXB && ct.PHIEUTRATIEN.NGAY > searchDate &&ct.PHIEUTRATIEN.TRANGTHAI==1)
                                                 .Select(ct => ct.PHIEUTRATIEN.SOTIENNO)
                                                 .DefaultIfEmpty(0)
                                                 .Sum();
                    int sotiendatra = (int)db.DOANHTHUs.Where(ct => ct.NXB.MANXB == o.MANXB && ct.NGAY > searchDate )
                                                  .Select(ct => ct.SOTIENNXB)
                                                  .DefaultIfEmpty(0)
                                                  .Sum();
                    o.SOTIENNO = o.SOTIENNO + sotiendatra - sotienduoctra;
                }
                stt.nxb = nxbs;
                return View(stt);
            }
            stt.nxb = db.NXBs.ToList();
            return View(stt);
        }

    }
}