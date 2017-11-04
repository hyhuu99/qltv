using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTV.Models
{
    public class congnodaily
    {
        public DateTime dt { get; set; }
        public List<DAILY> daily { get; set; }
    }
}