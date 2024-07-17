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
        dbQuanlyBanHangNhaSachNhaNamEntities db1 = new dbQuanlyBanHangNhaSachNhaNamEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblTaiKhoan user)
        {
            var userCheck = db1.tblTaiKhoans.SingleOrDefault(m=>m.sMaNV.Equals(user.sMaNV) && m.sMatkhau.Equals(user.sMatkhau));
            if (userCheck != null )
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
        public ActionResult Logout()
        {
            Session.Remove("User");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }
}