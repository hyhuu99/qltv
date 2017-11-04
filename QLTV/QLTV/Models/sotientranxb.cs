using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class sotientranxb
    {
        // GET: sotientranxb
        public DateTime dt { get; set; }
        public List<NXB> nxb { get; set; }
    }
}