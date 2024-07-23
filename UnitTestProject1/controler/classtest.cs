using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTestProject1.controler
{
    [TestFixture]
    public class cDoanhthuControllerTests
    {
        private Mock<BSACHEntities> mockContext;
        private cDoanhthuController controller;
        private Mock<DbSet<tblHoaDon>> mockSetHoaDon;
        private Mock<DbSet<tblChiTietHD>> mockSetChiTietHD;

        [SetUp]
        public void SetUp()
        {
            // Thiết lập mock context và controller
            mockContext = new Mock<BSACHEntities>();
            controller = new cDoanhthuController();
            controller.db = mockContext.Object;

            // Dữ liệu giả cho tblHoaDon
            var dataHoaDon = new List<tblHoaDon>
            {
                new tblHoaDon { sMaHD = "HD01", sMaNV = "NV01", dNgaylap = new DateTime(2023, 1, 1), fTongHD = 1000 },
                new tblHoaDon { sMaHD = "HD02", sMaNV = "NV02", dNgaylap = new DateTime(2023, 2, 1), fTongHD = 2000 },
            }.AsQueryable();

            mockSetHoaDon = new Mock<DbSet<tblHoaDon>>();
            mockSetHoaDon.As<IQueryable<tblHoaDon>>().Setup(m => m.Provider).Returns(dataHoaDon.Provider);
            mockSetHoaDon.As<IQueryable<tblHoaDon>>().Setup(m => m.Expression).Returns(dataHoaDon.Expression);
            mockSetHoaDon.As<IQueryable<tblHoaDon>>().Setup(m => m.ElementType).Returns(dataHoaDon.ElementType);
            mockSetHoaDon.As<IQueryable<tblHoaDon>>().Setup(m => m.GetEnumerator()).Returns(dataHoaDon.GetEnumerator());

            // Dữ liệu giả cho tblChiTietHD
            var dataChiTietHD = new List<tblChiTietHD>
            {
                new tblChiTietHD { sMaHD = "HD01", sMasach = "S01", tblSach = new tblSach { sMasach = "S01", sTheloai = "TL01", sTenTG = "TG01", sTensach = "Sach01" }, fThanhtien = 1000 },
                new tblChiTietHD { sMaHD = "HD02", sMasach = "S02", tblSach = new tblSach { sMasach = "S02", sTheloai = "TL02", sTenTG = "TG02", sTensach = "Sach02" }, fThanhtien = 2000 },
            }.AsQueryable();

            mockSetChiTietHD = new Mock<DbSet<tblChiTietHD>>();
            mockSetChiTietHD.As<IQueryable<tblChiTietHD>>().Setup(m => m.Provider).Returns(dataChiTietHD.Provider);
            mockSetChiTietHD.As<IQueryable<tblChiTietHD>>().Setup(m => m.Expression).Returns(dataChiTietHD.Expression);
            mockSetChiTietHD.As<IQueryable<tblChiTietHD>>().Setup(m => m.ElementType).Returns(dataChiTietHD.ElementType);
            mockSetChiTietHD.As<IQueryable<tblChiTietHD>>().Setup(m => m.GetEnumerator()).Returns(dataChiTietHD.GetEnumerator());

            mockContext.Setup(c => c.tblHoaDons).Returns(mockSetHoaDon.Object);
            mockContext.Setup(c => c.tblChiTietHDs).Returns(mockSetChiTietHD.Object);
        }

        [Test]
        public void FilterRevenue_ReturnsCorrectData()
        {
            // Arrange
            var employee = "NV01";
            var book = "";
            DateTime? startDate = new DateTime(2023, 1, 1);
            DateTime? endDate = new DateTime(2023, 12, 31);

            // Act
            var result = controller.FilterRevenue(employee, book, startDate, endDate) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            dynamic data = result.Data;
            Assert.IsTrue(data.success);
            Assert.AreEqual(1000, data.totalRevenue);
            Assert.AreEqual(1, data.orderCount);
        }

        // Thêm các unit test khác để kiểm tra các trường hợp khác nhau
    }
}
