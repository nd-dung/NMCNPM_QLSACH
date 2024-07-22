using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;

namespace UnitTest_PrjQLNSNN
{
    [TestFixture]
    public class QLNV_UnitTest
    {
        private Mock<dbQuanlyBanHangNhaSachNhaNamEntities> _mockDb;
        private cNhanvienController _controller;

        [SetUp]
        public void Setup()
        {
            _mockDb = new Mock<dbQuanlyBanHangNhaSachNhaNamEntities>();
            _controller = new cNhanvienController();

            // Mock Session
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new Moq.Mock<HttpContextBase>().Object
            };
            _controller.Session = new Moq.Mock<HttpSessionStateBase>().Object;
        }

        [Test]
        public void Index_UserNotLoggedIn_RedirectToLogin()
        {
            // Arrange
            _controller.Session["User"] = null;

            // Act
            var result = _controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
            Assert.AreEqual("cDangnhap", result.RouteValues["controller"]);
        }

        [Test]
        public void Index_UserIsAdmin_ReturnsViewWithList()
        {
            // Arrange
            _controller.Session["User"] = "ADMIN";
            var mockList = new List<viewNVPW> { new viewNVPW { sMaNV = "NV001" } };
            _mockDb.Setup(db => db.viewNVPWs.ToList()).Returns(mockList);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<viewNVPW>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
        }

        [Test]
        public void Create_DuplicateEmployeeId_ReturnsError()
        {
            // Arrange
            var model = new viewNVPW { sMaNV = "NV001", sSĐT = "0123456789", sCCCD = "123456789" };
            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sMaNV == model.sMaNV)).Returns(true);

            // Act
            var result = _controller.Create(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsFalse(jsonData.success);
            Assert.AreEqual("Mã nhân viên đã tồn tại.", jsonData.message);
        }

        [Test]
        public void Create_DuplicatePhoneNumber_ReturnsError()
        {
            // Arrange
            var model = new viewNVPW { sMaNV = "NV002", sSĐT = "0123456789", sCCCD = "123456788" };
            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sSĐT == model.sSĐT)).Returns(true);

            // Act
            var result = _controller.Create(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsFalse(jsonData.success);
            Assert.AreEqual("Số điện thoại đã tồn tại.", jsonData.message);
        }

        [Test]
        public void Create_DuplicateCCCD_ReturnsError()
        {
            // Arrange
            var model = new viewNVPW { sMaNV = "NV003", sSĐT = "0123456790", sCCCD = "123456789" };
            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sCCCD == model.sCCCD)).Returns(true);

            // Act
            var result = _controller.Create(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsFalse(jsonData.success);
            Assert.AreEqual("Số CCCD đã tồn tại.", jsonData.message);
        }

        [Test]
        public void Create_ValidModel_ReturnsSuccess()
        {
            // Arrange
            var model = new viewNVPW
            {
                sMaNV = "NV004",
                sSĐT = "0123456789",
                sCCCD = "987654321",
                sTenNV = "Nguyen Van A",
                sDiachi = "Ha Noi",
                bGioitinh = true,
                bTrangthai = true,
                dNgaysinh = DateTime.Now.AddYears(-30),
                dNgayvaolam = DateTime.Now,
                fLuong = 10000,
                sMatkhau = "password"
            };

            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sMaNV == model.sMaNV)).Returns(false);
            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sSĐT == model.sSĐT)).Returns(false);
            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sCCCD == model.sCCCD)).Returns(false);

            // Act
            var result = _controller.Create(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsTrue(jsonData.success);
            Assert.AreEqual("Thêm nhân viên thành công", jsonData.message);
        }

        [Test]
        public void Update_DuplicatePhoneNumber_ReturnsError()
        {
            // Arrange
            var model = new viewNVPW { sMaNV = "NV005", sSĐT = "0123456789", sCCCD = "987654321" };
            var existingNv = new tblNhanVien { sMaNV = "NV005" };

            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sMaNV != model.sMaNV && x.sSĐT == model.sSĐT)).Returns(true);
            _mockDb.Setup(db => db.tblNhanViens.Where(row => row.sMaNV == model.sMaNV).FirstOrDefault()).Returns(existingNv);

            // Act
            var result = _controller.Update(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsFalse(jsonData.success);
            Assert.AreEqual("Số điện thoại đã tồn tại.", jsonData.message);
        }

        [Test]
        public void Update_DuplicateCCCD_ReturnsError()
        {
            // Arrange
            var model = new viewNVPW { sMaNV = "NV006", sSĐT = "0123456780", sCCCD = "123456789" };
            var existingNv = new tblNhanVien { sMaNV = "NV006" };

            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sMaNV != model.sMaNV && x.sCCCD == model.sCCCD)).Returns(true);
            _mockDb.Setup(db => db.tblNhanViens.Where(row => row.sMaNV == model.sMaNV).FirstOrDefault()).Returns(existingNv);

            // Act
            var result = _controller.Update(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsFalse(jsonData.success);
            Assert.AreEqual("Số CCCD đã tồn tại.", jsonData.message);
        }

        [Test]
        public void Update_ValidModel_ReturnsSuccess()
        {
            // Arrange
            var model = new viewNVPW
            {
                sMaNV = "NV007",
                sSĐT = "0123456780",
                sCCCD = "987654321",
                sTenNV = "Nguyen Van B",
                sDiachi = "Hai Phong",
                bGioitinh = false,
                bTrangthai = false,
                dNgaysinh = DateTime.Now.AddYears(-25),
                dNgayvaolam = DateTime.Now.AddYears(-1),
                fLuong = 15000,
                sMatkhau = "newpassword"
            };

            var existingNv = new tblNhanVien { sMaNV = "NV007" };
            var existingTk = new tblTaiKhoan { sMaNV = "NV007" };

            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sMaNV != model.sMaNV && x.sSĐT == model.sSĐT)).Returns(false);
            _mockDb.Setup(db => db.tblNhanViens.Any(x => x.sMaNV != model.sMaNV && x.sCCCD == model.sCCCD)).Returns(false);
            _mockDb.Setup(db => db.tblNhanViens.Where(row => row.sMaNV == model.sMaNV).FirstOrDefault()).Returns(existingNv);
            _mockDb.Setup(db => db.tblTaiKhoans.Where(row => row.sMaNV == model.sMaNV).FirstOrDefault()).Returns(existingTk);

            // Act
            var result = _controller.Update(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsTrue(jsonData.success);
            Assert.AreEqual("Cập nhật nhân viên thành công", jsonData.message);
        }

        [Test]
        public void Search_ValidSearchText_ReturnsResults()
        {
            // Arrange
            var searchText = "Nguyen";
            var mockList = new List<viewNVPW> { new viewNVPW { sTenNV = "Nguyen Van C" } };
            _mockDb.Setup(db => db.viewNVPWs.Where(It.IsAny<Func<viewNVPW, bool>>()).ToList()).Returns(mockList);

            // Act
            var result = _controller.Search(searchText) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsTrue(jsonData.success);
            Assert.IsNotNull(jsonData.html);
        }

        [Test]
        public void Search_NoResults_ReturnsError()
        {
            // Arrange
            var searchText = "NotFound";
            _mockDb.Setup(db => db.viewNVPWs.Where(It.IsAny<Func<viewNVPW, bool>>()).ToList()).Returns(new List<viewNVPW>());

            // Act
            var result = _controller.Search(searchText) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var jsonData = result.Data as dynamic;
            Assert.IsFalse(jsonData.success);
            Assert.AreEqual("Không tìm thấy nhân viên.", jsonData.message);
        }
    }
}
//Tests:

//Index_UserNotLoggedIn_RedirectToLogin: Kiểm tra khi người dùng không đăng nhập thì chuyển hướng đến trang đăng nhập.
//Index_UserIsAdmin_ReturnsViewWithList: Kiểm tra khi người dùng là admin thì trả về view với danh sách nhân viên.
//Create_DuplicateEmployeeId_ReturnsError: Kiểm tra khi mã nhân viên đã tồn tại trong cơ sở dữ liệu.
//Create_DuplicatePhoneNumber_ReturnsError: Kiểm tra khi số điện thoại đã tồn tại.
//Create_DuplicateCCCD_ReturnsError: Kiểm tra khi số CCCD đã tồn tại.
//Create_ValidModel_ReturnsSuccess: Kiểm tra khi thêm nhân viên thành công với mô hình hợp lệ.
//Update_DuplicatePhoneNumber_ReturnsError: Kiểm tra khi cập nhật số điện thoại trùng với số điện thoại của nhân viên khác.
//Update_DuplicateCCCD_ReturnsError: Kiểm tra khi cập nhật số CCCD trùng với số CCCD của nhân viên khác.
//Update_ValidModel_ReturnsSuccess: Kiểm tra khi cập nhật thông tin nhân viên thành công với mô hình hợp lệ.
//Search_ValidSearchText_ReturnsResults: Kiểm tra khi tìm kiếm với văn bản hợp lệ và có kết quả trả về.
//Search_NoResults_ReturnsError: Kiểm tra khi tìm kiếm không có kết quả.