
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers
{
    public class cNhanvienController : Controller
    {
        // GET: cNhanvien
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "cDangnhap");
            }
            else if (Session["User"].Equals("ADMIN"))
            {
                dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();

                List<viewNVPW> listNV = db.viewNVPWs.ToList();
                return View("~/Views/cNhanvien/vNhanvien.cshtml", listNV);
            }
            else
            {
                return RedirectToAction("Index", "cBanhang");
            }
        }
        [HttpPost]
        public ActionResult Create(viewNVPW nv)
        {
            try
            {
                dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();
                if (db.tblNhanViens.Any(x => x.sMaNV == nv.sMaNV))
                {
                    throw new Exception($"Mã nhân viên đã tồn tại.");
                }
                if (db.tblNhanViens.Any(x => x.sSĐT == nv.sSĐT))
                {
                    throw new Exception($"Số điện thoại đã tồn tại.");
                }
                if (db.tblNhanViens.Any(x => x.sCCCD == nv.sCCCD))
                {
                    throw new Exception($"Số CCCD đã tồn tại.");
                }
                tblNhanVien nv2 = new tblNhanVien();
                nv2.sMaNV = nv.sMaNV;
                nv2.sTenNV = nv.sTenNV;
                nv2.sDiachi = nv.sDiachi;
                nv2.sCCCD = nv.sCCCD;
                nv2.bGioitinh = nv.bGioitinh;
                nv2.bTrangthai = nv.bTrangthai;
                nv2.bVaitro = false;
                nv2.dNgaysinh = nv.dNgaysinh;
                nv2.dNgayvaolam = nv.dNgayvaolam;
                nv2.fLuong = nv.fLuong;
                nv2.sSĐT = nv.sSĐT;
                db.tblNhanViens.Add(nv2);
                tblTaiKhoan tk = new tblTaiKhoan();
                tk.sMaNV = nv.sMaNV;
                tk.sMatkhau = nv.sMatkhau;
                db.tblTaiKhoans.Add(tk);
                db.SaveChanges();

                return Json(new { success = true, message = "Thêm nhân viên thành công" });
            }
            catch (Exception ex)
            {
                // Display an alert dialog with the error message
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult Update(viewNVPW nv)
        {


            try
            {
                dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();
                //kiem tra xem nếu mã nhân viên mà trùng với mã nhân viên thì ko cần kiểm tra
                //nếu mã nhân viên khác mã nhân viên thì kiểm tra tiếp nếu số điện thoại mà trùng với số điện thoại thì 
                if (db.tblNhanViens.Any(x => x.sMaNV != nv.sMaNV && x.sSĐT == nv.sSĐT))
                {
                    throw new Exception($"Số điện thoại đã tồn tại.");
                }
                if (db.tblNhanViens.Any(x => x.sMaNV != nv.sMaNV && x.sCCCD == nv.sCCCD))
                {
                    throw new Exception($"Số CCCD đã tồn tại.");
                }
                tblNhanVien nvien = db.tblNhanViens.Where(row => row.sMaNV == nv.sMaNV).FirstOrDefault();
                nvien.sTenNV = nv.sTenNV;
                nvien.dNgaysinh = nv.dNgaysinh;
                nvien.fLuong = nv.fLuong;
                nvien.sCCCD = nv.sCCCD;
                nvien.sDiachi = nv.sDiachi;
                nvien.sSĐT = nv.sSĐT;
                nvien.bGioitinh = nv.bGioitinh;
                nvien.bTrangthai = nv.bTrangthai;
                nvien.bVaitro = nv.bVaitro;
                nvien.dNgayvaolam = nv.dNgayvaolam;
                tblTaiKhoan tk = db.tblTaiKhoans.Where(row => row.sMaNV == nv.sMaNV).FirstOrDefault();
                if (tk != null)
                { tk.sMatkhau = nv.sMatkhau; }
                db.SaveChanges();

                return Json(new { success = true, message = "Cập nhật nhân viên thành công" });
            }
            catch (Exception ex)
            {
                // Display an alert dialog with the error message
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Search(string searchtext)
        {
            dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();
            List<viewNVPW> viewNVPWList = db.viewNVPWs.Where(
                v => v.sMaNV.ToLower().Contains(searchtext.ToLower()) ||
                     v.sTenNV.ToLower().Contains(searchtext.ToLower()) ||
                     v.sDiachi.ToLower().Contains(searchtext.ToLower()) ||
                     v.dNgaysinh.ToString().ToLower().Contains(searchtext.ToLower()) ||
                     (v.bTrangthai ?? false ? "Làm" : "Nghỉ").ToLower().Contains(searchtext.ToLower()) ||
                     (v.bGioitinh ?? false ? "Nam" : "Nữ").ToLower().Contains(searchtext.ToLower()) ||
                     v.dNgayvaolam.ToString().ToLower().Contains(searchtext.ToLower()) ||
                     v.sSĐT.ToLower().Contains(searchtext.ToLower()) ||
                     v.fLuong.ToString().ToLower().Contains(searchtext.ToLower()) ||
                     (v.bVaitro ?? false ? "QL" : "searchtext").ToLower().Contains(searchtext.ToLower()) ||
                     v.bTrangthai.ToString().ToLower().Contains(searchtext.ToLower()) ||
                     v.sCCCD.ToLower().Contains(searchtext.ToLower())
            ).ToList();

            if (viewNVPWList.Count == 0)
            {
                return Json(new { success = false, message = "Không tìm thấy nhân viên." });
            }

            var htmlString = RenderPartialViewToString("~/Views/Shared/ListNVView.cshtml", viewNVPWList);
            return Json(new { success = true, html = htmlString });
        }

        // Helper method to render partial view to string
        private string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }


    }
}