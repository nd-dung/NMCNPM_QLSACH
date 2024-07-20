using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using System.Data.Entity;
using System.IO;
using Moq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using Moq.Protected;

namespace UnitTest_PrjQLNSNN.Tests
{
    public class FakeViewEngine : IViewEngine
    {
        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return new ViewEngineResult(new FakeView(), this);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return new ViewEngineResult(new FakeView(), this);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
        }
    }
    public class FakeView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
        }
    }

    [TestFixture]
    public class UnitTest1
    {
        private Mock<dbQuanlyBanHangNhaSachNhaNamEntities> mockDb;
        private cBanhangController controller;

        [SetUp]
        public void Setup()
        {
            mockDb = new Mock<dbQuanlyBanHangNhaSachNhaNamEntities>();
            controller = new cBanhangController(mockDb.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (controller != null)
            {
                controller.Dispose(); // Giải phóng controller nếu nó triển khai IDisposable
                controller = null;
            }

            if (mockDb != null)
            {
                mockDb = null; // Giải phóng mockDb
            }
        }

        [Test]
        public void GetSearchResults_ReturnsCorrectResults()
        {
            // Arrange
            var searchQuery = "test";
            var mockData = new List<View_XemDanhSachSach>
    {
        new View_XemDanhSachSach { sMasach = "BOOK1", sTensach = "Test Book", sTenTG = "Test Author", iDongia = 50000 }
    }.AsQueryable();

            var mockSet = new Mock<DbSet<View_XemDanhSachSach>>();
            mockSet.As<IQueryable<View_XemDanhSachSach>>().Setup(m => m.Provider).Returns(mockData.Provider);
            mockSet.As<IQueryable<View_XemDanhSachSach>>().Setup(m => m.Expression).Returns(mockData.Expression);
            mockSet.As<IQueryable<View_XemDanhSachSach>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
            mockSet.As<IQueryable<View_XemDanhSachSach>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

            mockDb.Setup(m => m.View_XemDanhSachSach).Returns(mockSet.Object);

            // Act
            var result = controller.GetSearchResults(searchQuery);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].sMasach, Is.EqualTo("BOOK1"));
            Assert.That(result[0].sTensach, Is.EqualTo("Test Book"));
            Assert.That(result[0].sTenTG, Is.EqualTo("Test Author"));
            Assert.That(result[0].iDongia, Is.EqualTo(50000));
        }

        [Test]
        public void GetRecentInvoicesData_ReturnsCorrectData()
        {
            // Arrange
            var mockData = new List<View_XemDanhSachHoaDon>
    {
        new View_XemDanhSachHoaDon { sMaHD = "HD001", dNgaylap = DateTime.Now },
        new View_XemDanhSachHoaDon { sMaHD = "HD002", dNgaylap = DateTime.Now.AddDays(-1) }
    }.AsQueryable();

            var mockSet = new Mock<DbSet<View_XemDanhSachHoaDon>>();
            mockSet.As<IQueryable<View_XemDanhSachHoaDon>>().Setup(m => m.Provider).Returns(mockData.Provider);
            mockSet.As<IQueryable<View_XemDanhSachHoaDon>>().Setup(m => m.Expression).Returns(mockData.Expression);
            mockSet.As<IQueryable<View_XemDanhSachHoaDon>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
            mockSet.As<IQueryable<View_XemDanhSachHoaDon>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

            mockDb.Setup(db => db.View_XemDanhSachHoaDon).Returns(mockSet.Object);

            // Act
            var result = controller.GetRecentInvoicesData();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].sMaHD, Is.EqualTo("HD001"));
            Assert.That(result[1].sMaHD, Is.EqualTo("HD002"));
        }

        //[Test]
        //public void SaveInvoice_ValidModel_ReturnsSuccessJson()
        //{
        //    // Arrange
        //    var model = new tblHoaDon
        //    {
        //        sMaNV = "NV001",
        //        fTongHD = 100000,
        //        tblChiTietHDs = new List<tblChiTietHD>
        //{
        //    new tblChiTietHD { sMaSach = "BOOK1", iSoLuong = 1, fDongia = 100000, fThanhtien = 100000 }
        //}
        //    };

        //    var mockHoaDonSet = new Mock<DbSet<tblHoaDon>>();
        //    var mockChiTietHDSet = new Mock<DbSet<tblChiTietHD>>();

        //    var mockController = new Mock<cBanhangController>(mockDb.Object) { CallBase = true };
        //    mockController.Setup(x => x.GenerateNewMaHD()).Returns("HD001");

        //    // Assign the mocked controller to 'controller'
        //    var dbContextMock = new Mock<BSACHEntities>();
        //    var controller = new cBanhangController(dbContextMock.Object);


        //    mockDb.Setup(m => m.tblHoaDons.Add(It.IsAny<tblHoaDon>())).Verifiable();
        //    mockDb.Setup(m => m.tblChiTietHDs.Add(It.IsAny<tblChiTietHD>())).Verifiable();
        //    mockDb.Setup(m => m.SaveChanges()).Returns(1);

        //    // Act
        //    var result = controller.SaveInvoice(model) as JsonResult;

        //    // Debug
        //    Console.WriteLine(JObject.FromObject(result.Data).ToString());

        //    // Assert
        //    Assert.IsNotNull(result);
        //    var jsonData = JObject.FromObject(result.Data);
        //    Assert.IsTrue(jsonData["success"].Value<bool>(), $"Expected success to be true, but got: {jsonData["success"]}. Full response: {jsonData}");

        //    // Verify that entities were added
        //    mockHoaDonSet.Verify(m => m.Add(It.IsAny<tblHoaDon>()), Times.Once());
        //    mockChiTietHDSet.Verify(m => m.Add(It.IsAny<tblChiTietHD>()), Times.Once());

        //    // Verify that SaveChanges was called
        //    mockDb.Verify(m => m.SaveChanges(), Times.Once());
        //}



        [Test]
        public void GenerateNewMaHD_ReturnsCorrectFormat()
        {
            // Arrange
            // Tạo một danh sách mẫu
            var data = new List<tblHoaDon>().AsQueryable();

            var mockSet = new Mock<DbSet<tblHoaDon>>();
            mockSet.As<IQueryable<tblHoaDon>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<tblHoaDon>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<tblHoaDon>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<tblHoaDon>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDb.Setup(m => m.tblHoaDons).Returns(mockSet.Object);

            // Act
            var result = controller.GenerateNewMaHD();

            // Assert
            Assert.That(result, Does.Match(@"^HD\d{3}$"));
        }

        [Test]
        public void SaveInvoice_ModelStateInvalid_ReturnsErrorJson()
        {
            // Arrange
            controller.ModelState.AddModelError("error", "test error");

            // Act
            var result = controller.SaveInvoice(new tblHoaDon()) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = JObject.FromObject(result.Data);
            Assert.IsFalse(jsonData["success"].Value<bool>());
            Assert.That(jsonData["message"].Value<string>(), Is.EqualTo("Dữ liệu không hợp lệ."));
        }
    }
}