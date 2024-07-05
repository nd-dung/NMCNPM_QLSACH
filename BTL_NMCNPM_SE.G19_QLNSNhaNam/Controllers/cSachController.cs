using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    public class cSachController : Controller
    {
        // GET: cSach
        public ActionResult Index()
        {
            return View("~/Views/cSach/vSach.cshtml");
        }
    }
}