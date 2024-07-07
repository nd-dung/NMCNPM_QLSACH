-- Đây chỉ là Database cơ bản. Mọi người thêm vào dưới dùng file này nhớ ghi thời gian thêm và message nhé
-- VD: Thêm 1 procedure mới:
    -- --06/07/2024 Update proc
    -- Message khi commit sẽ là: Update database
-- Tạo bảng vwNhanVienWithPassword
CREATE DATABASE dbQuanlyBanHangNhaSachNhaNam
GO

CREATE TABLE tblNhanVien (
    sMaNV NCHAR(10) PRIMARY KEY,
    sTenNV NVARCHAR(100),
    dNgaysinh DATE,
    bGioitinh BIT,
    sDiachi NVARCHAR(100),
    dNgayvaolam DATE,
    sSĐT NVARCHAR(10),
    fLuong FLOAT,
    bVaitro BIT,
    bTrangthai BIT,
    sCCCD NVARCHAR(20)
);
drop table vwNhanVienWithPassword
-- Tạo bảng tblTaiKhoan
CREATE TABLE tblTaiKhoan (
    sMaNV NCHAR(10) PRIMARY KEY,
    sMatKhau NCHAR(10) NOT NULL,
    CONSTRAINT FK_tblTaiKhoan_tblNhanVien FOREIGN KEY (sMaNV) REFERENCES tblNhanVien(sMaNV)
);

-- Tạo bảng tblSach
CREATE TABLE tblSach (
    sMasach NCHAR(10) PRIMARY KEY,
    sTensach NVARCHAR(100),
    sTenTG NVARCHAR(100),
    iDongia INT,
    iSoluong INT,
    sNXB NVARCHAR(100),
    sTheloai NVARCHAR(100)
);

-- Tạo bảng tblHoaDon
CREATE TABLE tblHoaDon (
    sMaHD NCHAR(10) PRIMARY KEY,
    sMaNV NCHAR(10),
    dNgaylap DATE,
    fTongHD FLOAT,
    CONSTRAINT FK_tblHoaDon_vwNhanVienWithPassword FOREIGN KEY (sMaNV) REFERENCES vwNhanVienWithPassword(sMaNV)
);

-- Tạo bảng tblChiTietHD
CREATE TABLE tblChiTietHD (
    sMaHD NCHAR(10),
    sMaSach NCHAR(10),
    iSoLuong INT,
    fDongia FLOAT,
    fThanhtien FLOAT,
    PRIMARY KEY (sMaHD, sMaSach),
    CONSTRAINT FK_tblChiTietHD_tblHoaDon FOREIGN KEY (sMaHD) REFERENCES tblHoaDon(sMaHD),
    CONSTRAINT FK_tblChiTietHD_tblSach FOREIGN KEY (sMaSach) REFERENCES tblSach(sMasach)
);

-- Tạo bảng tblNhap
CREATE TABLE tblNhap (
    sMaPN NCHAR(10) PRIMARY KEY,
    sMaNV NCHAR(10),
    dNgaylap DATE,
    bLoai BIT,
    fTongGT FLOAT,
    CONSTRAINT FK_tblNhap_vwNhanVienWithPassword FOREIGN KEY (sMaNV) REFERENCES vwNhanVienWithPassword(sMaNV)
);

-- Tạo bảng tblChiTietNhap
CREATE TABLE tblChiTietNhap (
    sMaPN NCHAR(10),
    sMasach NCHAR(10),
    iSoluong INT,
    fDonGia FLOAT,
    fThanhTien FLOAT,
    PRIMARY KEY (sMaPN, sMaSach),
    CONSTRAINT FK_tblChiTietNhap_tblNhap FOREIGN KEY (sMaPN) REFERENCES tblNhap(sMaPN),
    CONSTRAINT FK_tblChiTietNhap_tblSach FOREIGN KEY (sMaSach) REFERENCES tblSach(sMasach)
);

-- Thêm dữ liệu
INSERT INTO vwNhanVienWithPassword (sMaNV, sTenNV, dNgaysinh, bGioiTinh, sDiaChi, dNgayvaolam, sSĐT, fLuong, bVaitro, bTrangthai, sCCCD)
VALUES 
('NV001', N'Nguyễn Văn A', '1990-01-01', 1, N'123 Đường ABC, Quận XYZ, TP HCM', '2020-01-01', '0987654321', 10000000, 0, 1, '1234567890123'),
('NV002', N'Trần Thị B', '1995-05-05', 0, N'456 Đường XYZ, Quận ABC, TP HCM', '2021-01-01', '0901234567', 8000000, 0, 1, '2345678901234'),
('NV003', N'Phạm Văn C', '1992-12-15', 1, N'789 Đường DEF, Quận UVW, TP HCM', '2019-12-01', '0978123456', 12000000, 0, 1, '3456789012345'),
('NV004', N'Nguyễn Thị D', '1988-03-20', 0, N'012 Đường UVW, Quận DEF, TP HCM', '2018-01-01', '0912345678', 9000000, 0, 1, '4567890123456'),
('ADMIN', 'Admin', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL);

INSERT INTO tblTaiKhoan (sMaNV, sMatKhau)
VALUES 
('NV001', 'passNV001@'),
('NV002', 'passNV002@'),
('NV003', 'passNV003@'),
('NV004', 'passNV004@'),
('ADMIN', 'passADMIN@');

INSERT INTO tblSach (sMasach, sTensach, sTenTG, iDongia, iSoluong, sNXB, sTheloai)
VALUES 
('S001', 'Sách 1', 'Tác giả A', 50000, 100, 'NXB ABC', 'Khoa học'),
('S002', 'Sách 2', 'Tác giả B', 60000, 150, 'NXB XYZ', 'Văn học'),
('S003', 'Sách 3', 'Tác giả C', 45000, 120, 'NXB DEF', 'Lịch sử'),
('S004', 'Sách 4', 'Tác giả D', 55000, 80, 'NXB UVW', 'Khoa học'),
('S005', 'Sách 5', 'Tác giả E', 40000, 200, 'NXB MNO', 'Văn học'),
('S006', 'Sách 6', 'Tác giả F', 65000, 90, 'NXB GHI', 'Lịch sử'),
('S007', 'Sách 7', 'Tác giả G', 48000, 110, 'NXB JKL', 'Khoa học'),
('S008', 'Sách 8', 'Tác giả H', 70000, 60, 'NXB PQR', 'Văn học'),
('S009', 'Sách 9', 'Tác giả I', 52000, 130, 'NXB STU', 'Lịch sử'),
('S010', 'Sách 10', 'Tác giả J', 58000, 70, 'NXB VWX', 'Khoa học');

-- Thêm các hóa đơn
INSERT INTO tblHoaDon (sMaHD, sMaNV, dNgaylap, fTongHD)
VALUES 
('HD001', 'NV001', '2024-07-05', 150000),
('HD002', 'NV002', '2024-07-04', 180000),
('HD003', 'NV003', '2024-07-03', 135000),
('HD004', 'NV001', '2024-07-02', 165000),
('HD005', 'NV002', '2024-07-01', 120000),
('HD006', 'NV004', '2024-06-30', 165000),
('HD007', 'NV001', '2024-06-29', 140000),
('HD008', 'NV003', '2024-06-28', 155000),
('HD009', 'NV002', '2024-06-27', 130000),
('HD010', 'NV004', '2024-06-26', 145000),
('HD011', 'NV001', '2024-06-25', 175000),
('HD012', 'NV003', '2024-06-24', 160000),
('HD013', 'NV002', '2024-06-23', 185000),
('HD014', 'NV004', '2024-06-22', 170000),
('HD015', 'NV001', '2024-06-21', 125000);

-- Thêm chi tiết hóa đơn
INSERT INTO tblChiTietHD (sMaHD, sMasach, iSoLuong, fDongia, fThanhtien)
VALUES 
('HD001', 'S001', 2, 50000, 100000),
('HD001', 'S002', 3, 60000, 180000),
('HD002', 'S003', 2, 45000, 90000),
('HD002', 'S004', 4, 55000, 220000),
('HD003', 'S005', 3, 40000, 120000),
('HD003', 'S006', 2, 65000, 130000),
('HD004', 'S007', 3, 48000, 144000),
('HD004', 'S008', 2, 70000, 140000),
('HD005', 'S009', 4, 52000, 208000),
('HD005', 'S010', 2, 58000, 116000),
('HD006', 'S001', 3, 50000, 150000),
('HD006', 'S002', 2, 60000, 120000),
('HD007', 'S003', 3, 45000, 135000),
('HD007', 'S004', 2, 55000, 110000),
('HD008', 'S005', 4, 40000, 160000),
('HD008', 'S006', 2, 65000, 130000),
('HD009', 'S007', 3, 48000, 144000),
('HD009', 'S008', 2, 70000, 140000),
('HD010', 'S009', 4, 52000, 208000),
('HD010', 'S010', 2, 58000, 116000),
('HD011', 'S001', 3, 50000, 150000),
('HD011', 'S002', 2, 60000, 120000),
('HD012', 'S003', 3, 45000, 135000),
('HD012', 'S004', 2, 55000, 110000),
('HD013', 'S005', 4, 40000, 160000),
('HD013', 'S006', 2, 65000, 130000),
('HD014', 'S007', 3, 48000, 144000),
('HD014', 'S008', 2, 70000, 140000),
('HD015', 'S009', 4, 52000, 208000),
('HD015', 'S010', 2, 58000, 116000);

-- Thêm các phiếu nhập
-- Ví dụ 20 phiếu nhập (10 nhập kho, 10 đổi trả)
INSERT INTO tblNhap (sMaPN, sMaNV, dNgaylap, bLoai, fTongGT)
VALUES 
('PN001', 'NV001', '2024-07-05', 1, 150000),
('PN002', 'NV002', '2024-07-04', 0, 180000),
('PN003', 'NV003', '2024-07-03', 1, 135000),
('PN004', 'NV001', '2024-07-02', 0, 165000),
('PN005', 'NV002', '2024-07-01', 1, 120000),
('PN006', 'NV004', '2024-06-30', 0, 165000),
('PN007', 'NV001', '2024-06-29', 1, 140000),
('PN008', 'NV003', '2024-06-28', 0, 155000),
('PN009', 'NV002', '2024-06-27', 1, 130000),
('PN010', 'NV004', '2024-06-26', 0, 145000),
('PN011', 'NV001', '2024-06-25', 1, 175000),
('PN012', 'NV003', '2024-06-24', 0, 160000),
('PN013', 'NV002', '2024-06-23', 1, 185000),
('PN014', 'NV004', '2024-06-22', 0, 170000),
('PN015', 'NV001', '2024-06-21', 1, 125000),
('PN016', 'NV003', '2024-06-20', 0, 165000),
('PN017', 'NV002', '2024-06-19', 1, 160000),
('PN018', 'NV004', '2024-06-18', 0, 145000),
('PN019', 'NV001', '2024-06-17', 1, 180000),
('PN020', 'NV003', '2024-06-16', 0, 175000);

-- Thêm chi tiết phiếu nhập
-- Ví dụ 30 chi tiết phiếu nhập
INSERT INTO tblChiTietNhap (sMaPN, sMasach, iSoLuong, fDonGia, fThanhTien)
VALUES 
('PN001', 'S001', 2, 50000, 100000),
('PN001', 'S002', 3, 60000, 180000),
('PN002', 'S003', 2, 45000, 90000),
('PN002', 'S004', 4, 55000, 220000),
('PN003', 'S005', 3, 40000, 120000),
('PN003', 'S006', 2, 65000, 130000),
('PN004', 'S007', 3, 48000, 144000),
('PN004', 'S008', 2, 70000, 140000),
('PN005', 'S009', 4, 52000, 208000),
('PN005', 'S010', 2, 58000, 116000),
('PN006', 'S001', 3, 50000, 150000),
('PN006', 'S002', 2, 60000, 120000),
('PN007', 'S003', 3, 45000, 135000),
('PN007', 'S004', 2, 55000, 110000),
('PN008', 'S005', 4, 40000, 160000),
('PN008', 'S006', 2, 65000, 130000),
('PN009', 'S007', 3, 48000, 144000),
('PN009', 'S008', 2, 70000, 140000),
('PN010', 'S009', 4, 52000, 208000),
('PN010', 'S010', 2, 58000, 116000),
('PN011', 'S001', 3, 50000, 150000),
('PN011', 'S002', 2, 60000, 120000),
('PN012', 'S003', 3, 45000, 135000),
('PN012', 'S004', 2, 55000, 110000),
('PN013', 'S005', 4, 40000, 160000),
('PN013', 'S006', 2, 65000, 130000),
('PN014', 'S007', 3, 48000, 144000),
('PN014', 'S008', 2, 70000, 140000),
('PN015', 'S009', 4, 52000, 208000),
('PN015', 'S010', 2, 58000, 116000),
('PN016', 'S001', 3, 50000, 150000),
('PN016', 'S002', 2, 60000, 120000),
('PN017', 'S003', 3, 45000, 135000),
('PN017', 'S004', 2, 55000, 110000),
('PN018', 'S005', 4, 40000, 160000),
('PN018', 'S006', 2, 65000, 130000),
('PN019', 'S007', 3, 48000, 144000),
('PN019', 'S008', 2, 70000, 140000),
('PN020', 'S009', 4, 52000, 208000),
('PN020', 'S010', 2, 58000, 116000);
GO

-- View

CREATE VIEW View_XemDanhSachSach AS
SELECT * FROM tblSach;
GO

CREATE VIEW View_XemDanhSachNhanVien AS
SELECT * FROM vwNhanVienWithPassword;
GO

CREATE VIEW View_XemDanhSachHoaDon AS
SELECT * FROM tblHoaDon;
GO

CREATE VIEW View_XemChiTietHoaDon AS
SELECT * FROM tblChiTietHD;
go

CREATE VIEW View_XemDanhSachPhieuNhap AS
SELECT * FROM tblNhap;
GO

CREATE VIEW View_XemChiTietNhap AS
SELECT * FROM tblChiTietNhap;
GO

CREATE PROCEDURE Proc_XemThongTinNhanVienVaTaiKhoan
AS
BEGIN
    SELECT NV.sMaNV, NV.sTenNV, NV.dNgaySinh, NV.bGioiTinh, NV.sDiaChi, NV.dNgayVaoLam, NV.sSĐT, NV.fLuong, NV.sCCCD, TK.sMatKhau, NV.bVaitro
    FROM vwNhanVienWithPassword NV
    INNER JOIN tblTaiKhoan TK ON NV.sMaNV = TK.sMaNV;
END;

CREATE VIEW vwNhanVienWithPassword
AS
SELECT nv.*, tk.sMatKhau
FROM vwNhanVienWithPassword nv
JOIN tblTaiKhoan tk ON nv.sMaNV = tk.sMaNV;
