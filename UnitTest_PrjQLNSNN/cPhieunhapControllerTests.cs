using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;

namespace UnitTest_PrjQLNSNN
{
    [TestFixture]
    public class cPhieunhapControllerTests
    {
        private cPhieunhapController _controller;
        private Mock<DB1> _dbContext;
        private Mock<DbSet<bangnhap>> _mockBangnhaps;
        private Mock<DbSet<tblNhap>> _mockTblNhaps;

        [SetUp]
        public void Setup()
        {
            _dbContext = new Mock<DB1>();
            _mockBangnhaps = new Mock<DbSet<bangnhap>>();
            _mockTblNhaps = new Mock<DbSet<tblNhap>>();
            _controller = new cPhieunhapController(_dbContext.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public void Index_ReturnsViewWithList()
        {
            // Arrange
            var bangnhapList = new List<bangnhap>
            {
                new bangnhap { MaPN = "PN1", sMaNV = "NV1", dNgaylap = DateTime.Now, bLoai = true },
                new bangnhap { MaPN = "PN2", sMaNV = "NV2", dNgaylap = DateTime.Now, bLoai = false }
            }.AsQueryable();

            _mockBangnhaps.As<IQueryable<bangnhap>>().Setup(m => m.Provider).Returns(bangnhapList.Provider);
            _mockBangnhaps.As<IQueryable<bangnhap>>().Setup(m => m.Expression).Returns(bangnhapList.Expression);
            _mockBangnhaps.As<IQueryable<bangnhap>>().Setup(m => m.ElementType).Returns(bangnhapList.ElementType);
            _mockBangnhaps.As<IQueryable<bangnhap>>().Setup(m => m.GetEnumerator()).Returns(bangnhapList.GetEnumerator());

            _dbContext.Setup(db => db.bangnhaps).Returns(_mockBangnhaps.Object);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<bangnhap>;
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public void Create_AddsNewBangNhap_ReturnsJsonResult()
        {
            // Arrange
            var newBangNhap = new bangnhap
            {
                MaPN = "PN1",
                sMaNV = "NV1",
                dNgaylap = DateTime.Now,
                bLoai = true
            };

            var mockTblNhap = new tblNhap
            {
                MaPN = "PN1",
                sMaNV = "NV1",
                dNgaylap = DateTime.Now,
                bLoai = true
            };

            _mockTblNhaps.Setup(m => m.Add(It.IsAny<tblNhap>())).Returns(mockTblNhap);

            _dbContext.Setup(db => db.tblNhaps).Returns(_mockTblNhaps.Object);

            // Act
            var result = _controller.Create(newBangNhap) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            dynamic data = result.Data;
            Assert.AreEqual(true, data.success);
            Assert.AreEqual("Thêm thành công", data.message);

            // Verify that the Add method was called once
            _mockTblNhaps.Verify(m => m.Add(It.IsAny<tblNhap>()), Times.Once);
            _dbContext.Verify(db => db.SaveChanges(), Times.Once);
        }
    }
}
