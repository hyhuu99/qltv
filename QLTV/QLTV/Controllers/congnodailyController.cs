using QLTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Controllers
{
    public class congnodailyController : Controller
    {
        private QLTVEntities db = new QLTVEntities();
        // GET: congnodaily
        public ActionResult Index(string ngay, string MADL)
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            congnodaily cndl = new congnodaily();
            cndl.dt = DateTime.Now;
            DateTime searchDate;
            if (DateTime.TryParse(ngay, out searchDate))
            {
                List<DAILY> dls = new List<DAILY>();
                dls = db.DAILies.Where(o => o.MADL == MADL).ToList();

                foreach (DAILY o in dls)
                {
                    int tonggiaxuat = (int)db.CTPXS.Where(ct => ct.PHIEUXUATSACH.MADL == o.MADL && ct.PHIEUXUATSACH.NGAYXUAT > searchDate)
                                                  .Select(ct => ct.TONG)
                                                  .DefaultIfEmpty(0)
                                                  .Sum();
                    int tongsotientra = (int)db.CTPTTs.Where(ct => ct.PHIEUTRATIEN.MADL == o.MADL && ct.PHIEUTRATIEN.NGAY > searchDate)
                                                  .Select(ct => ct.SOLUONGN * ct.SACH.GIABAN)
                                                  .DefaultIfEmpty(0)
                                                  .Sum();

                    o.SOTIENNO = o.SOTIENNO - tonggiaxuat + tongsotientra;
                }
                cndl.daily = dls;
                return View(cndl);
            }
            cndl.daily = db.DAILies.ToList();
            return View(cndl);
        }
    }
}