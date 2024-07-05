using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    public class cDoanhthuController : Controller
    {
        // GET: cDoanhthu
        public ActionResult Index()
        {
            return View("~/Views/cDoanhthu/vDoanhthu.cshtml");
        }
    }
}