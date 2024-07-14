using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();
            List<tblNhap> listNV = db.tblNhaps.ToList();
            return View("~/Views/cPhieunhap/vPhieunhap.cshtml", listNV);
        }
        [HttpPost]
        public ActionResult Create(tblNhap bn)
        {

            dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();
            tblNhanVien nv2 = new tblNhanVien();
            tblNhap n = new tblNhap();
            n.MaPN = bn.MaPN;
            n.sMaNV = bn.sMaNV;
            n.dNgaylap = bn.dNgaylap;
            n.bLoai = bn.bLoai;
            db.tblNhaps.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}