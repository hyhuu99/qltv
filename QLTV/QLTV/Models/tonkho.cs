using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class tonkho : Controller
    {
        // GET: tonkho
        public DateTime dt { get; set; }
        public List<SACH> sach { get; set; }
    }
}