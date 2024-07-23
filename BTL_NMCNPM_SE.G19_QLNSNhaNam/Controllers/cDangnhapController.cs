using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers
{
    public class cDangnhapController : Controller
    {
        // GET: cDangnhap
        dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public cDangnhapController(dbQuanlyBanHangNhaSachNhaNamEntities dbContext)
        {
            db = dbContext;
        }
        [HttpPost]
        public ActionResult Login(tblTaiKhoan user)
        {
            var userCheck = db.tblTaiKhoans.SingleOrDefault(m => m.sMaNV.Equals(user.sMaNV) && m.sMatkhau.Equals(user.sMatkhau));

            if (ModelState.IsValid)
            {
                var userNV = db.tblNhanViens.SingleOrDefault(x => x.sMaNV == user.sMaNV && x.bTrangthai.ToString().Equals("False"));
                if (userNV != null) { ViewBag.NhanVien = "Tài khoản của bạn đã bị khóa"; return View(); }
                else
                {
                    if (userCheck != null)
                    {
                        Session["User"] = userCheck.sMaNV;
                        return RedirectToAction("Index", "cBanhang");

                    }
                    else
                    {
                        ViewBag.LoginFail = "Đăng nhập thất bại, vui lòng kiểm tra lại!";
                        return View();
                    }
                }
            }
            else
            {

                ViewBag.LoginFail = "";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("User");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }
}