using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class tkdt : Controller
    {
        public List<ctdt> ctdt { get; set; }
        public decimal doanhthu { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}