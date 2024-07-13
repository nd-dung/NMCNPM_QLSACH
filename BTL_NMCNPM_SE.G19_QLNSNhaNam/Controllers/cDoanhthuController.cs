using System;
using System.Linq;
using System.Web.Mvc;

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    public class cDoanhthuController : Controller
    {
        protected readonly dbQuanlyBanHangNhaSachNhaNamEntities db = new dbQuanlyBanHangNhaSachNhaNamEntities();

        public ActionResult Index()
        {
            return View("~/Views/cDoanhthu/vDoanhthu.cshtml");
        }

        [HttpGet]
        public ActionResult FilterRevenue(string employee, string book, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = db.tblHoaDons.AsQueryable();

                if (!string.IsNullOrEmpty(employee))
                {
                    query = query.Where(hd => hd.sMaNV.Contains(employee) || hd.tblNhanVien.sTenNV.Contains(employee));
                }
                if (!string.IsNullOrEmpty(book))
                {
                    query = query.Where(hd => hd.tblChiTietHDs.Any(ct =>
                        ct.tblSach.sMasach.Contains(book) ||
                        ct.tblSach.sTensach.Contains(book) ||
                        ct.tblSach.sNXB.Contains(book) ||
                        ct.tblSach.sTheloai.Contains(book)
                    ));
                }

                if (startDate.HasValue)
                {
                    query = query.Where(hd => hd.dNgaylap >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(hd => hd.dNgaylap <= endDate.Value);
                }

                var revenueData = query.Select(hd => new
                {
                    hd.sMaHD,
                    hd.sMaNV,
                    hd.dNgaylap,
                    hd.fTongHD
                }).ToList();

                decimal totalRevenue = Convert.ToDecimal(revenueData.Sum(hd => hd.fTongHD ?? 0));
                int orderCount = revenueData.Count;

                var revenueByDate = revenueData
                    .GroupBy(hd => hd.dNgaylap)
                    .Select(g => new { date = g.Key, revenue = g.Sum(hd => hd.fTongHD ?? 0) })
                    .OrderBy(x => x.date)
                    .ToList();

                var invoiceIds = revenueData.Select(hd => hd.sMaHD).ToList();

                var revenueByCategory = db.tblChiTietHDs
                    .Where(ct => invoiceIds.Contains(ct.sMaHD))
                    .GroupBy(ct => ct.tblSach.sTheloai)
                    .Select(g => new { category = g.Key, revenue = g.Sum(ct => ct.fThanhtien ?? 0) })
                    .ToList();

                var revenueByAuthor = db.tblChiTietHDs
                    .Where(ct => invoiceIds.Contains(ct.sMaHD))
                    .GroupBy(ct => ct.tblSach.sTenTG)
                    .Select(g => new { author = g.Key, revenue = g.Sum(ct => ct.fThanhtien ?? 0) })
                    .ToList();

                var bestSellers = db.tblChiTietHDs
                    .Where(ct => invoiceIds.Contains(ct.sMaHD))
                    .GroupBy(ct => ct.tblSach.sTensach)
                    .Select(g => new
                    {
                        bookName = g.Key,
                        soldQuantity = g.Sum(ct => ct.iSoluong ?? 0),
                        revenue = g.Sum(ct => ct.fThanhtien ?? 0)
                    })
                    .OrderByDescending(x => x.soldQuantity)
                    .Take(10)
                    .ToList();

                var result = new
                {
                    success = true,
                    totalRevenue = totalRevenue,
                    orderCount = orderCount,
                    data = revenueData.Select(hd => new
                    {
                        hd.sMaHD,
                        hd.sMaNV,
                        dNgaylap = hd.dNgaylap.HasValue ? hd.dNgaylap.Value.ToString("yyyy-MM-dd") : "",
                        fTongHD = hd.fTongHD.HasValue ? hd.fTongHD.Value.ToString("N0") : "0"
                    }).ToList(),
                    revenueByDate = revenueByDate,
                    revenueByCategory = revenueByCategory,
                    revenueByAuthor = revenueByAuthor,
                    bestSellers = bestSellers
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi xử lý dữ liệu: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}