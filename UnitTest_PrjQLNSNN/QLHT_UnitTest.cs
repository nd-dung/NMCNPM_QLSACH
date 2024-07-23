using BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
namespace UnitTest_PrjQLNSNN.Tests.Controllers
{
    [TestFixture]
    internal class QLHT_UnitTest
    {
        private cDangnhapController _controller;
        private Mock<dbQuanlyBanHangNhaSachNhaNamEntities> _dbContext;
        private Mock<tblTaiKhoan> _mockUser;
        private Mock<tblNhanVien> _mockNhanVien;

        [SetUp]
        public void Setup()
        {
            _dbContext = new Mock<dbQuanlyBanHangNhaSachNhaNamEntities>();
            _mockUser = new Mock<tblTaiKhoan>();
            _mockNhanVien = new Mock<tblNhanVien>();
            _controller = new cDangnhapController(_dbContext.Object);
        }
        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }
        [Test]
        public void Login_WithValidUser_ReturnsRedirectToAction()
        {
            // Arrange
            _mockUser.Setup(u => u.sMaNV).Returns("testuser");
            _mockUser.Setup(u => u.sMatkhau).Returns("testpassword");
            _dbContext.Setup(db => db.tblTaiKhoans.SingleOrDefault(It.IsAny<Func<tblTaiKhoan, bool>>()))
                      .Returns(_mockUser.Object);
            _dbContext.Setup(db => db.tblNhanViens.SingleOrDefault(It.IsAny<Func<tblNhanVien, bool>>()))
                      .Returns((tblNhanVien)null);

            var model = new tblTaiKhoan
            {
                sMaNV = "testuser",
                sMatkhau = "testpassword"
            };
            /* Act
            var result = _controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("cBanhang", result.ControllerName);*/
            // Act
            var result = _controller.Login(model);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual("/cBanhang/Index", ((RedirectResult)result).Url);
        }

        [Test]
        public void Login_WithInvalidUser_ReturnsViewWithError()
        {
            // Arrange
            _mockUser.Setup(u => u.sMaNV).Returns("testuser");
            _mockUser.Setup(u => u.sMatkhau).Returns("wrongpassword");
            _dbContext.Setup(db => db.tblTaiKhoans.SingleOrDefault(It.IsAny<Func<tblTaiKhoan, bool>>()))
                      .Returns((tblTaiKhoan)null);

            var model = new tblTaiKhoan
            {
                sMaNV = "testuser",
                sMatkhau = "wrongpassword"
            };

            // Act
            var result = _controller.Login(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
            Assert.IsNotNull(result.ViewBag.LoginFail);
        }

        [Test]
        public void Logout_ReturnsRedirectToAction()
        {
            // Act
            var result = _controller.Logout() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
            Assert.AreEqual("Login", result.RouteName);
        }
    }
}
