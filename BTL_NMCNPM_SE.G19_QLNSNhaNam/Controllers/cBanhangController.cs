using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers
{
    public class cBanhangController : Controller
    {
        protected readonly dbQuanlyBanHangNhaSachNhaNamEntities1 db = new dbQuanlyBanHangNhaSachNhaNamEntities1();

        //public cBanhangController(dbQuanlyBanHangNhaSachNhaNamEntities1 dbContext)
        //{
        //     db = dbContext;
        //}

        // GET: cBanhang
        public ActionResult Index()
        {
            return View("~/Views/cBanhang/vBanhang.cshtml");
        }

        public ActionResult Search(string searchQuery)
        {
            var DSSachList = GetSearchResults(searchQuery);
            return PartialView("~/Views/cBanhang/vDSSP.cshtml", DSSachList);
        }

        public List<View_XemDanhSachSach> GetSearchResults(string searchQuery)
        {
            return db.View_XemDanhSachSach.Where(
                v => v.sMasach.ToLower().Contains(searchQuery.ToLower()) ||
                     v.sTensach.ToLower().Contains(searchQuery.ToLower()) ||
                     v.sTenTG.ToLower().Contains(searchQuery.ToLower())
            ).ToList();
        }

        public ActionResult GetRecentInvoices()
        {
            var recentInvoices = GetRecentInvoicesData();
            return PartialView("~/Views/cBanhang/vHoadonGanday.cshtml", recentInvoices);
        }

        public List<View_XemDanhSachHoaDon> GetRecentInvoicesData()
        {
            return db.View_XemDanhSachHoaDon
                .OrderByDescending(h => h.dNgaylap)
                .Take(10)
                .ToList();
        }



        [HttpPost]
        public ActionResult ViewInvoiceDetails(string maHD)
        {
            var details = ViewInvoiceDetailsData(maHD);
            return PartialView("~/Views/cBanhang/vChitietHD.cshtml", details);
        }

        public List<vwNhanvienHoadonChitietHD> ViewInvoiceDetailsData(string RepMaHD)
        {
            return db.vwNhanvienHoadonChitietHDs
                .Where(c => c.sMaHD == RepMaHD)
                .ToList();
        }

        [HttpPost]
        public virtual ActionResult SaveInvoice(tblHoaDon model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo mã hóa đơn mới
                    var maHD = GenerateNewMaHD();

                    // Lưu hóa đơn
                    var hoaDon = new tblHoaDon
                    {
                        sMaHD = maHD,
                        sMaNV = model.sMaNV,
                        dNgaylap = DateTime.Now,
                        fTongHD = model.fTongHD
                    };
                    db.tblHoaDons.Add(hoaDon);

                    // Lưu chi tiết hóa đơn
                    foreach (var detail in model.tblChiTietHDs)
                    {
                        var chiTietHD = new tblChiTietHD
                        {
                            sMaHD = maHD,
                            sMaSach = detail.sMaSach,
                            iSoLuong = detail.iSoLuong,
                            fDongia = detail.fDongia,
                            fThanhtien = detail.fThanhtien
                        };
                        db.tblChiTietHDs.Add(chiTietHD);
                    }
                    db.SaveChanges();

                    return Json(new { success = true, message = "Hóa đơn đã được lưu thành công.", invoiceId = maHD });
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Có lỗi xảy ra khi lưu hóa đơn." });
                }
            }
            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }


        public static readonly object _lock = new object();
        public virtual string GenerateNewMaHD()
        {
            lock (_lock)
            {
                // Lấy mã hóa đơn gần đây nhất từ cơ sở dữ liệu
                var lastInvoice = db.tblHoaDons
                    .OrderByDescending(h => h.sMaHD)
                    .FirstOrDefault();

                string newMaHD;

                if (lastInvoice != null)
                {
                    // Tách phần tiền tố "HD" và phần số
                    string lastMaHD = lastInvoice.sMaHD;
                    string prefix = new string(lastMaHD.TakeWhile(char.IsLetter).ToArray());
                    string numberPart = new string(lastMaHD.SkipWhile(char.IsLetter).ToArray());

                    if (int.TryParse(numberPart, out int number))
                    {
                        number++;
                        newMaHD = $"{prefix}{number:D3}";
                    }
                    else
                    {
                        newMaHD = "HD001";
                    }
                }
                else
                {
                    newMaHD = "HD001";
                }

                return newMaHD;
            }
        }


        public ActionResult PrintInvoice(string maHD)
        {
            var invoiceDetails = db.vwNhanvienHoadonChitietHDs
                .Where(h => h.sMaHD == maHD)
                .ToList();

            if (invoiceDetails == null || !invoiceDetails.Any())
            {
                return Content("Không tìm thấy thông tin hóa đơn.");
            }
            return PartialView("vChitietHD", invoiceDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}