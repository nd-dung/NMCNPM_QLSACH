using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    public class cSachController : Controller
    {
        // GET: cSach
        dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();

        public ActionResult Index(string searchInput = "")
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "cDangnhap");
            }
            else
            {
                string searchParam = searchInput == "" ? "" : searchInput.Trim();
                List<tblSach> listSach = db.tblSaches.Where(
                row => row.sTensach.Contains(searchInput)).ToList();
                ViewBag.Search = searchInput;

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
                ViewBag.JsonListSach = JsonConvert.SerializeObject(listSach, Formatting.None, settings);

                // Đảm bảo ViewBag.JsonListSach không bao giờ null
                if (string.IsNullOrEmpty(ViewBag.JsonListSach))
                {
                    ViewBag.JsonListSach = "[]";
                }

                return View("~/Views/cSach/vSach.cshtml", listSach);
            }
        }

        public ActionResult Detail(string sMasach)
        {
            tblSach sach = db.tblSaches.Where(row => row.sMasach == sMasach).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Add(tblSach sach)
        {
            var existingBook = db.tblSaches.FirstOrDefault(b => b.sMasach == sach.sMasach);
            if (existingBook != null)
            {
                // Trả về mã lỗi và thông báo lỗi
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { error = "Mã sách đã tồn tại." });
            }

            db.tblSaches.Add(sach);
            db.SaveChanges();
            return Json(new { success = true });
            //return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(tblSach sach)
        {
            tblSach sachDB = db.tblSaches.Where(row => row.sMasach == sach.sMasach).FirstOrDefault();
            sachDB.sTensach = sach.sTensach;
            sachDB.sTenTG = sach.sTenTG;
            sachDB.iDongia = sach.iDongia;
            sachDB.iSoluong = sach.iSoluong;
            sachDB.sNXB = sach.sNXB;
            sachDB.sTheloai = sach.sTheloai;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string sMasach)
        {
            tblSach sachDB = db.tblSaches.Where(row => row.sMasach == sMasach).FirstOrDefault();
            db.tblSaches.Remove(sachDB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}