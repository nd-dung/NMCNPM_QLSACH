using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers
{
    public class cBanhangController : Controller
    {
        // GET: cBanhang
        public ActionResult Index()
        {
            return View("~/Views/cBanhang/vBanhang.cshtml");
        }
    }
}