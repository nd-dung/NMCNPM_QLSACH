﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers
{
    public class cPhieunhapController : Controller
    {
        // GET: cPhieunhap
        public ActionResult Index()
        {
            return View("~/Views/cPhieunhap/vPhieunhap.cshtml");
        }
    }
}