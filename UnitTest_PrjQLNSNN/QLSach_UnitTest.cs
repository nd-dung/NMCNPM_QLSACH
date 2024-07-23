using BTL_NMCNPM_SE.G19_QLNSNhaNam.Controllers;
using BTL_NMCNPM_SE.G19_QLNSNhaNam.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnitTest_PrjQLNSNN
{
    [TestFixture]
    internal class QLSach_UnitTest
    {
        private cDangnhapController _controller;
        private Mock<dbQuanlyBanHangNhaSachNhaNamEntities> _dbContext;
        private Mock<tblSach> _mockSach;

        [Test]
        public void Detail_ReturnsCorrectView_WhenBookExists()
        {
            // Arrange
            var mockDb = new Mock<dbQuanlyBanHangNhaSachNhaNamEntities>();

            var book = new tblSach { sMasach = "S001", sTensach = "Đắc Nhân Tâm" };

            var bookList = new List<tblSach> { book }.AsQueryable();
            _dbContext.Setup(m => m.tblSaches).Returns((System.Data.Entity.DbSet<tblSach>)bookList);

            var controller = new cSachController();

            // Act
            var result = controller.Detail("1") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void Add_ReturnsError_WhenBookAlreadyExists()
        {
            // Arrange
           
            var existingBook = new tblSach { sMasach = "S001", sTensach = "Đắc Nhân Tâm" };

            var bookList = new List<tblSach> { existingBook }.AsQueryable();
            _dbContext.Setup(m => m.tblSaches).Returns((System.Data.Entity.DbSet<tblSach>)bookList);

            var controller = new cSachController();

            var newBook = new tblSach { sMasach = "S001", sTensach = "Đắc Nhân Tâm 2" };

            // Act
            var result = controller.Add(newBook) as JsonResult;
            var jsonResult = result.Data as IDictionary<string, object>;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Mã sách đã tồn tại.", jsonResult["error"]);
        }

        [Test]
        public void Add_ReturnsSuccess_WhenBookIsNew()
        {
            // Arrange
            var bookList = new List<tblSach>().AsQueryable();

            _dbContext.Setup(m => m.tblSaches).Returns((System.Data.Entity.DbSet<tblSach>)bookList);
            _dbContext.Setup(m => m.tblSaches.Add(It.IsAny<tblSach>()));

            var controller = new cSachController();

            var newBook = new tblSach { sMasach = "S001", sTensach = "Đắc Nhân Tâm" };

            // Act
            var result = controller.Add(newBook) as JsonResult;
            var jsonResult = result.Data as IDictionary<string, object>;

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)jsonResult["success"]);
        }

        [Test]
        public void Delete_ReturnsCorrectView_WhenBookExists()
        {
            // Arrange
           
            var book = new tblSach { sMasach = "S001", sTensach = "Đắc Nhân Tâm" };

            var bookList = new List<tblSach> { book }.AsQueryable();
            _dbContext.Setup(m => m.tblSaches).Returns((System.Data.Entity.DbSet<tblSach>)bookList);
            _dbContext.Setup(m => m.tblSaches.Remove(It.IsAny<tblSach>()));

            var controller = new cSachController();

            // Act
            var result = controller.Delete("S001") as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
