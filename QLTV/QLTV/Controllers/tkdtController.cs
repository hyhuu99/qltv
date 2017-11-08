using QLTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace QLTV.Controllers
{
    public class tkdtController : Controller
    {
        private QLTVEntities db = new QLTVEntities();
        public ActionResult Index(String startdate, String enddate)
        {
            tkdt dtvm = new tkdt();
            DateTime start;
            DateTime end;
            if (DateTime.TryParse(startdate, out start) && DateTime.TryParse(enddate, out end))
            {
                dtvm.startdate = start;
                dtvm.enddate = end;
                dtvm.ctdt = db.CTPTTs
                              .Include(o => o.PHIEUTRATIEN)
                              .Include(o => o.SACH)
                              .Where(o => o.PHIEUTRATIEN.NGAY > start && o.PHIEUTRATIEN.NGAY < end)
                              .AsEnumerable()
                              .Select(c => new ctdt(c)).ToList();
                dtvm.doanhthu = tinhdoanhthu(dtvm.ctdt);
                return View(dtvm);
            }
            else
                return View(dtvm);
            
        }

        private decimal tinhdoanhthu(List<ctdt> ctdt)
        {
            decimal tongdoanhthu = 0;
            foreach (ctdt ct in ctdt)
            {
                tongdoanhthu += ct.loinhuan;
            }
            return tongdoanhthu;
        }
    }
}