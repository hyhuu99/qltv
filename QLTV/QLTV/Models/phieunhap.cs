using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class phieunhap
    {
        // GET: phieunhap
        public PHIEUNHAPSACH phieunhaps { get; set; }
        public CTPN ctpn { get; set; }
    }
}