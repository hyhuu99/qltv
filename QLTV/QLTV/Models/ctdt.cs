using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class ctdt : Controller
    {
        public DateTime ngaygiaodich { get; set; }
        public string tensach { get; set; }
        public int soluongdaban { get; set; }
        public decimal thanhtien { get; set; }
        public decimal sotienphaitrachonxb { get; set; }
        public decimal loinhuan { get; set; }

        public ctdt(CTPTT ctptt)
        {
            ngaygiaodich = (DateTime)ctptt.PHIEUTRATIEN.NGAY;
            tensach = ctptt.SACH.TENS;
            soluongdaban = (int)ctptt.SOLUONGN;
            thanhtien = (decimal)(ctptt.TONG);
            sotienphaitrachonxb = (decimal)(ctptt.SOLUONGN*ctptt.SACH.GIANHAP);
            loinhuan = thanhtien - sotienphaitrachonxb;
        }
    }
}