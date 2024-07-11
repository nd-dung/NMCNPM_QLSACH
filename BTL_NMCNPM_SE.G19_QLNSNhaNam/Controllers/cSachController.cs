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
        DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index(string searchInput="")
        {
            List<tblSach> listSach =  db.tblSaches.Where(
                row => row.sTensach.Contains(searchInput)).ToList();

            ViewBag.Search = searchInput.Trim();   
            return View("~/Views/cSach/vSach.cshtml", listSach);
        }

        public ActionResult Detail(string sMasach)
        {
            tblSach sach = db.tblSaches.Where(row => row.sMasach ==  sMasach).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Add(tblSach sach)
        {
            db.tblSaches.Add(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
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