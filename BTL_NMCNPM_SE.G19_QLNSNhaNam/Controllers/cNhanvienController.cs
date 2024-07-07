using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers
{
    public class cNhanvienController : Controller
    {
        // GET: cNhanvien
        public ActionResult Index()
        {

            databaseEntities2 db = new databaseEntities2();
            
            List<viewNVPW> listNV = db.viewNVPWs.ToList();
            return View("~/Views/cNhanvien/vNhanvien.cshtml", listNV);
        }
        [HttpPost]
        public ActionResult Create(viewNVPW nv)
        {
           
            databaseEntities2 db = new databaseEntities2();
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
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            databaseEntities2 db = new databaseEntities2();
            tblNhanVien nvien = db.tblNhanViens.Where(row => row.sMaNV == id).FirstOrDefault();
            nvien.bTrangthai = false;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(viewNVPW nv)
        {
            try
            {
                databaseEntities2 db = new databaseEntities2();
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
                tk.sMatkhau = nv.sMatkhau;
                db.SaveChanges();
            }
            catch { };
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchtext)
        {

            databaseEntities2 db = new databaseEntities2();
            List<viewNVPW> viewNVPWList = db.viewNVPWs.Where(
    v => v.sMaNV.ToLower().Contains(searchtext.ToLower()) ||
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

            return View("~/Views/cNhanvien/vNhanvien.cshtml", viewNVPWList);
        }

    }
}