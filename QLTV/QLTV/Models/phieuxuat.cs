using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class phieuxuat
    {
        // GET: phieuxuat
        public PHIEUXUATSACH phieuxuats { get; set; }
        public CTPX ctpx { get; set; }
    }
}