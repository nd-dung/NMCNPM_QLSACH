-- Đây chỉ là Database cơ bản. Mọi người thêm vào dưới dùng file này nhớ ghi thời gian thêm và message nhé
-- VD: Thêm 1 procedure mới:
    -- --06/07/2024 Update proc
    -- Message khi commit sẽ là: Update database
BEGIN TRANSACTION

BEGIN TRY

    CREATE DATABASE dbQuanlyBanHangNhaSachNhaNam
    
	USE dbQuanlyBanHangNhaSachNhaNam

    CREATE TABLE tblNhanVien (
        sMaNV VARCHAR(10) PRIMARY KEY,
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
    -- Tạo bảng tblTaiKhoan
    CREATE TABLE tblTaiKhoan (
        sMaNV VARCHAR(10) PRIMARY KEY,
        sMatkhau VARCHAR(10) NOT NULL,
        CONSTRAINT FK_tblTaiKhoan_tblNhanVien FOREIGN KEY (sMaNV) REFERENCES tblNhanVien(sMaNV)
    );

    -- Tạo bảng tblSach
    CREATE TABLE tblSach (
        sMasach VARCHAR(10) PRIMARY KEY,
        sTensach NVARCHAR(100),
        sTenTG NVARCHAR(100),
        iDongia INT,
        iSoluong INT,
        sNXB NVARCHAR(100),
        sTheloai NVARCHAR(100)
    );

    -- Tạo bảng tblHoaDon
    CREATE TABLE tblHoaDon (
        sMaHD VARCHAR(10) PRIMARY KEY,
        sMaNV VARCHAR(10) NOT NULL,
        dNgaylap DATE,
        fTongHD FLOAT,
        CONSTRAINT FK_tblHoaDon_tblNhanVien FOREIGN KEY (sMaNV) REFERENCES tblNhanVien(sMaNV)
    );

    -- Tạo bảng tblChiTietHD
    CREATE TABLE tblChiTietHD (
        sMaHD VARCHAR(10) NOT NULL,
        sMasach VARCHAR(10) NOT NULL,
        iSoluong INT,
        fDongia FLOAT,
        fThanhtien FLOAT,
        PRIMARY KEY (sMaHD, sMaSach),
        CONSTRAINT FK_tblChiTietHD_tblHoaDon FOREIGN KEY (sMaHD) REFERENCES tblHoaDon(sMaHD),
        CONSTRAINT FK_tblChiTietHD_tblSach FOREIGN KEY (sMaSach) REFERENCES tblSach(sMasach)
    );

    -- Tạo bảng tblNhap
    CREATE TABLE tblNhap (
        MaPN VARCHAR(10) PRIMARY KEY,
        sMaNV VARCHAR(10) NOT NULL,
        dNgaylap DATE,
        bLoai BIT,
        fTongGT FLOAT,
        CONSTRAINT FK_tblNhap_tblNhanVien FOREIGN KEY (sMaNV) REFERENCES tblNhanVien(sMaNV)
    );

    -- Tạo bảng tblChiTietNhap
    CREATE TABLE tblChiTietNhap (
        MaPN VARCHAR(10) NOT NULL,
        Masach VARCHAR(10) NOT NULL,
        iSoluong INT,
        fDongia FLOAT,
        fThanhtien FLOAT,
        PRIMARY KEY (MaPN, MaSach),
        CONSTRAINT FK_tblChiTietNhap_tblNhap FOREIGN KEY (MaPN) REFERENCES tblNhap(MaPN),
        CONSTRAINT FK_tblChiTietNhap_tblSach FOREIGN KEY (MaSach) REFERENCES tblSach(sMasach)
    );

    -- Thêm dữ liệu
    -- Thêm dữ liệu vào bảng tblNhanVien
    INSERT INTO tblNhanVien VALUES
    (N'NV001', N'Nguyễn Văn An', '1990-05-15', 1, N'Hà Nội', '2020-01-01', N'0901234567', 10000000, 0, 1, N'001234567890'),
    (N'NV002', N'Trần Thị Bình', '1992-08-20', 0, N'Hồ Chí Minh', '2020-03-15', N'0912345678', 9500000, 0, 1, N'002345678901'),
    (N'NV003', N'Lê Văn Cường', '1988-11-10', 1, N'Đà Nẵng', '2019-12-01', N'0923456789', 11000000, 0, 1, N'003456789012'),
    (N'NV004', N'Phạm Thị Dung', '1995-02-25', 0, N'Hải Phòng', '2021-06-01', N'0934567890', 9000000, 0, 1, N'004567890123'),
    (N'NV005', N'Hoàng Văn Em', '1993-07-30', 1, N'Cần Thơ', '2020-09-01', N'0945678901', 9800000, 0, 1, N'005678901234'),
    (N'ADMIN', N'Đỗ Thị Fương', '1985-04-12', 0, N'Hà Nội', '2018-01-01', N'0956789012', 15000000, 1, 1, N'006789012345');

    -- Thêm dữ liệu vào bảng tblTaiKhoan
    INSERT INTO tblTaiKhoan VALUES
    (N'NV001', N'pass001@'),
    (N'NV002', N'pass002@'),
    (N'NV003', N'pass003@'),
    (N'NV004', N'pass004@'),
    (N'NV005', N'pass005@'),
    (N'ADMIN', N'admin001@');

    -- Thêm dữ liệu vào bảng tblSach (50 bản ghi)
    INSERT INTO tblSach VALUES
    (N'S001', N'Đắc Nhân Tâm', N'Dale Carnegie', 150000, 100, N'NXB Tổng hợp TP.HCM', N'Kỹ năng sống'),
    (N'S002', N'Nhà Giả Kim', N'Paulo Coelho', 120000, 80, N'NXB Văn học', N'Tiểu thuyết'),
    (N'S003', N'Số Đỏ', N'Vũ Trọng Phụng', 100000, 90, N'NXB Văn học', N'Văn học Việt Nam'),
    (N'S004', N'Đời Ngắn Đừng Ngủ Dài', N'Robin Sharma', 130000, 75, N'NXB Trẻ', N'Kỹ năng sống'),
    (N'S005', N'Cách Nghĩ Để Thành Công', N'Napoleon Hill', 140000, 85, N'NXB Lao động', N'Kinh doanh'),
    (N'S006', N'Hạt Giống Tâm Hồn', N'Nhiều tác giả', 95000, 120, N'NXB Tổng hợp TP.HCM', N'Tâm lý - Kỹ năng sống'),
    (N'S007', N'Người Giàu Có Nhất Thành Babylon', N'George S. Clason', 88000, 70, N'NXB Tổng hợp TP.HCM', N'Kinh tế'),
    (N'S008', N'Đọc Vị Bất Kỳ Ai', N'David J. Lieberman', 110000, 65, N'NXB Lao động - Xã hội', N'Tâm lý học'),
    (N'S009', N'Bố Già', N'Mario Puzo', 150000, 55, N'NXB Văn học', N'Tiểu thuyết'),
    (N'S010', N'Tuổi Trẻ Đáng Giá Bao Nhiêu', N'Rosie Nguyễn', 78000, 100, N'NXB Hội Nhà Văn', N'Kỹ năng sống'),
    (N'S011', N'Cây Cam Ngọt Của Tôi', N'José Mauro de Vasconcelos', 108000, 80, N'NXB Hội Nhà Văn', N'Văn học'),
    (N'S012', N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', N'Nguyễn Nhật Ánh', 125000, 90, N'NXB Trẻ', N'Văn học Việt Nam'),
    (N'S013', N'Đừng Lựa Chọn An Nhàn Khi Còn Trẻ', N'Cảnh Thiên', 86000, 75, N'NXB Thế giới', N'Kỹ năng sống'),
    (N'S014', N'Từ Điển Tiếng "Em"', N'Khotudien', 69000, 110, N'NXB Phụ Nữ Việt Nam', N'Văn học'),
    (N'S015', N'Khéo Ăn Nói Sẽ Có Được Thiên Hạ', N'Trác Nhã', 119000, 60, N'NXB Văn học', N'Kỹ năng giao tiếp'),
    (N'S016', N'Đắc Nhân Tâm (Bản Mới)', N'Dale Carnegie', 86000, 130, N'NXB Tổng hợp TP.HCM', N'Kỹ năng sống'),
    (N'S017', N'Nhà Lãnh Đạo Không Chức Danh', N'Robin Sharma', 85000, 70, N'NXB Trẻ', N'Quản trị - Lãnh đạo'),
    (N'S018', N'Chiến Tranh Tiền Tệ', N'Song Hongbing', 180000, 50, N'NXB Lao động', N'Kinh tế - Tài chính'),
    (N'S019', N'Hành Trình Về Phương Đông', N'Baird T. Spalding', 92000, 85, N'NXB Hồng Đức', N'Tâm linh'),
    (N'S020', N'Luật Hấp Dẫn', N'Michael J. Losier', 68000, 100, N'NXB Lao động', N'Phát triển bản thân'),
    (N'S021', N'Đời Ngắn Đừng Ngủ Dài (Tái Bản)', N'Robin Sharma', 75000, 95, N'NXB Trẻ', N'Kỹ năng sống'),
    (N'S022', N'Tôi Tài Giỏi, Bạn Cũng Thế', N'Adam Khoo', 150000, 60, N'NXB Phụ nữ', N'Kỹ năng học tập'),
    (N'S023', N'Lược Sử Loài Người', N'Yuval Noah Harari', 229000, 40, N'NXB Thế giới', N'Lịch sử - Khoa học'),
    (N'S024', N'Thiên Tài Bên Trái, Kẻ Điên Bên Phải', N'Cao Minh', 145000, 75, N'NXB Thế giới', N'Tâm lý học'),
    (N'S025', N'Mắt Biếc', N'Nguyễn Nhật Ánh', 110000, 85, N'NXB Trẻ', N'Văn học Việt Nam'),
    (N'S026', N'Nếu Chỉ Còn Một Ngày Để Sống', N'Nicola Yoon', 79000, 70, N'NXB Văn học', N'Tiểu thuyết'),
    (N'S027', N'Cà Phê Cùng Tony', N'Tony Buổi Sáng', 90000, 100, N'NXB Trẻ', N'Kỹ năng sống'),
    (N'S028', N'Bí Mật Của May Mắn', N'Brian Tracy', 75000, 80, N'NXB Tổng hợp TP.HCM', N'Phát triển bản thân'),
    (N'S029', N'Totto-Chan Bên Cửa Sổ', N'Tetsuko Kuroyanagi', 82000, 65, N'NXB Văn học', N'Văn học Nhật Bản'),
    (N'S030', N'Cho Tôi Xin Một Vé Đi Tuổi Thơ', N'Nguyễn Nhật Ánh', 80000, 90, N'NXB Trẻ', N'Văn học Việt Nam'),
    (N'S031', N'Đừng Bao Giờ Đi Ăn Một Mình', N'Keith Ferrazzi', 109000, 55, N'NXB Trẻ', N'Kỹ năng kinh doanh'),
    (N'S032', N'Tôi Là Beto', N'Nguyễn Nhật Ánh', 78000, 95, N'NXB Trẻ', N'Văn học Việt Nam'),
    (N'S033', N'Không Gia Đình', N'Hector Malot', 115000, 70, N'NXB Văn học', N'Văn học nước ngoài'),
    (N'S034', N'Nghệ Thuật Tinh Tế Của Việc "Đếch" Quan Tâm', N'Mark Manson', 95000, 85, N'NXB Văn học', N'Kỹ năng sống'),
    (N'S035', N'Hoàng Tử Bé', N'Antoine de Saint-Exupéry', 69000, 100, N'NXB Hội Nhà Văn', N'Văn học nước ngoài');

    -- Thêm dữ liệu vào bảng tblHoaDon (50 bản ghi)
    INSERT INTO tblHoaDon VALUES
    (N'HD001', N'NV001', '2024-01-05', 450000),
    (N'HD002', N'NV002', '2024-01-06', 380000),
    (N'HD003', N'NV003', '2024-01-07', 520000),
    (N'HD004', N'NV004', '2024-01-08', 290000),
    (N'HD005', N'NV005', '2024-01-09', 610000),
    (N'HD006', N'NV001', '2024-01-10', 340000),
    (N'HD007', N'NV002', '2024-01-11', 720000),
    (N'HD008', N'NV003', '2024-01-12', 550000),
    (N'HD009', N'NV004', '2024-01-13', 480000),
    (N'HD010', N'NV005', '2024-01-14', 390000),
    (N'HD011', N'NV001', '2024-01-15', 670000),
    (N'HD012', N'NV002', '2024-01-16', 430000),
    (N'HD013', N'NV003', '2024-01-17', 580000),
    (N'HD014', N'NV004', '2024-01-18', 320000),
    (N'HD015', N'NV005', '2024-01-19', 490000),
    (N'HD016', N'NV001', '2024-01-20', 710000),
    (N'HD017', N'NV002', '2024-01-21', 360000),
    (N'HD018', N'NV003', '2024-01-22', 540000),
    (N'HD019', N'NV004', '2024-01-23', 630000),
    (N'HD020', N'NV005', '2024-01-24', 420000),
    (N'HD021', N'NV001', '2024-01-25', 590000),
    (N'HD022', N'NV002', '2024-01-26', 680000),
    (N'HD023', N'NV003', '2024-01-27', 370000),
    (N'HD024', N'NV004', '2024-01-28', 510000),
    (N'HD025', N'NV005', '2024-01-29', 740000),
    (N'HD026', N'NV001', '2024-01-30', 460000),
    (N'HD027', N'NV002', '2024-01-31', 620000),
    (N'HD028', N'NV003', '2024-02-01', 530000),
    (N'HD029', N'NV004', '2024-02-02', 410000),
    (N'HD030', N'NV005', '2024-02-03', 690000),
    (N'HD031', N'NV001', '2024-02-04', 350000),
    (N'HD032', N'NV002', '2024-02-05', 570000),
    (N'HD033', N'NV003', '2024-02-06', 640000),
    (N'HD034', N'NV004', '2024-02-07', 470000),
    (N'HD035', N'NV005', '2024-02-08', 730000),
    (N'HD036', N'NV001', '2024-02-09', 400000),
    (N'HD037', N'NV002', '2024-02-10', 560000),
    (N'HD038', N'NV003', '2024-02-11', 650000),
    (N'HD039', N'NV004', '2024-02-12', 330000),
    (N'HD040', N'NV005', '2024-02-13', 600000),
    (N'HD041', N'NV001', '2024-02-14', 700000),
    (N'HD042', N'NV002', '2024-02-15', 440000),
    (N'HD043', N'NV003', '2024-02-16', 500000),
    (N'HD044', N'NV004', '2024-02-17', 750000),
    (N'HD045', N'NV005', '2024-02-18', 380000),
    (N'HD046', N'NV001', '2024-02-19', 660000),
    (N'HD047', N'NV002', '2024-02-20', 520000),
    (N'HD048', N'NV003', '2024-02-21', 450000),
    (N'HD049', N'NV004', '2024-02-22', 770000),
    (N'HD050', N'NV005', '2024-02-23', 390000);

    -- Thêm dữ liệu vào bảng tblChiTietHD
    INSERT INTO tblChiTietHD VALUES
    (N'HD001', N'S001', 2, 150000, 300000),
    (N'HD001', N'S002', 1, 120000, 120000),
    (N'HD001', N'S003', 3, 100000, 300000),
    (N'HD002', N'S004', 1, 130000, 130000),
    (N'HD002', N'S005', 2, 140000, 280000),
    (N'HD003', N'S006', 3, 95000, 285000),
    (N'HD003', N'S007', 1, 88000, 88000),
    (N'HD004', N'S008', 2, 110000, 220000),
    (N'HD004', N'S009', 1, 150000, 150000),
    (N'HD005', N'S010', 3, 78000, 234000),
    (N'HD005', N'S011', 2, 108000, 216000),
    (N'HD006', N'S012', 1, 125000, 125000),
    (N'HD006', N'S013', 3, 86000, 258000),
    (N'HD007', N'S014', 2, 69000, 138000),
    (N'HD007', N'S015', 1, 119000, 119000),
    (N'HD008', N'S016', 2, 86000, 172000),
    (N'HD008', N'S017', 3, 85000, 255000),
    (N'HD009', N'S018', 1, 180000, 180000),
    (N'HD009', N'S019', 2, 92000, 184000),
    (N'HD010', N'S020', 1, 68000, 68000),
    (N'HD010', N'S021', 3, 75000, 225000),
    (N'HD011', N'S022', 1, 150000, 150000),
    (N'HD011', N'S023', 2, 229000, 458000),
    (N'HD012', N'S024', 1, 145000, 145000),
    (N'HD012', N'S025', 2, 110000, 220000),
    (N'HD013', N'S026', 3, 79000, 237000),
    (N'HD013', N'S027', 1, 90000, 90000),
    (N'HD014', N'S028', 2, 75000, 150000),
    (N'HD014', N'S029', 3, 82000, 246000),
    (N'HD015', N'S030', 1, 80000, 80000),
    (N'HD015', N'S031', 2, 109000, 218000),
    (N'HD016', N'S032', 3, 78000, 234000),
    (N'HD016', N'S033', 1, 115000, 115000),
    (N'HD017', N'S034', 2, 95000, 190000),
    (N'HD017', N'S035', 3, 69000, 207000),
    (N'HD018', N'S001', 1, 150000, 150000),
    (N'HD018', N'S002', 2, 120000, 240000),
    (N'HD019', N'S003', 3, 100000, 300000),
    (N'HD019', N'S004', 1, 130000, 130000),
    (N'HD020', N'S005', 2, 140000, 280000),
    (N'HD020', N'S006', 3, 95000, 285000),
    (N'HD021', N'S007', 1, 88000, 88000),
    (N'HD021', N'S008', 2, 110000, 220000),
    (N'HD022', N'S009', 1, 150000, 150000),
    (N'HD022', N'S010', 3, 78000, 234000),
    (N'HD023', N'S011', 2, 108000, 216000),
    (N'HD023', N'S012', 1, 125000, 125000),
    (N'HD024', N'S013', 3, 86000, 258000),
    (N'HD024', N'S014', 2, 69000, 138000),
    (N'HD025', N'S015', 1, 119000, 119000),
    (N'HD025', N'S016', 2, 86000, 172000),
    (N'HD026', N'S017', 3, 85000, 255000),
    (N'HD026', N'S018', 1, 180000, 180000),
    (N'HD027', N'S019', 2, 92000, 184000),
    (N'HD027', N'S020', 1, 68000, 68000),
    (N'HD028', N'S021', 3, 75000, 225000),
    (N'HD028', N'S022', 1, 150000, 150000),
    (N'HD029', N'S023', 2, 229000, 458000),
    (N'HD029', N'S024', 1, 145000, 145000),
    (N'HD030', N'S025', 2, 110000, 220000),
    (N'HD030', N'S026', 3, 79000, 237000),
    (N'HD031', N'S027', 1, 90000, 90000),
    (N'HD031', N'S028', 2, 75000, 150000),
    (N'HD032', N'S029', 3, 82000, 246000),
    (N'HD032', N'S030', 1, 80000, 80000),
    (N'HD033', N'S031', 2, 109000, 218000),
    (N'HD033', N'S032', 3, 78000, 234000),
    (N'HD034', N'S033', 1, 115000, 115000),
    (N'HD034', N'S034', 2, 95000, 190000),
    (N'HD035', N'S035', 3, 69000, 207000),
    (N'HD035', N'S001', 1, 150000, 150000),
    (N'HD036', N'S002', 2, 120000, 240000),
    (N'HD036', N'S003', 3, 100000, 300000),
    (N'HD037', N'S004', 1, 130000, 130000),
    (N'HD037', N'S005', 2, 140000, 280000),
    (N'HD038', N'S006', 3, 95000, 285000),
    (N'HD038', N'S007', 1, 88000, 88000),
    (N'HD039', N'S008', 2, 110000, 220000),
    (N'HD039', N'S009', 1, 150000, 150000),
    (N'HD040', N'S010', 3, 78000, 234000),
    (N'HD040', N'S011', 2, 108000, 216000),
    (N'HD041', N'S012', 1, 125000, 125000),
    (N'HD041', N'S013', 3, 86000, 258000),
    (N'HD042', N'S014', 2, 69000, 138000),
    (N'HD042', N'S015', 1, 119000, 119000),
    (N'HD043', N'S016', 2, 86000, 172000),
    (N'HD043', N'S017', 3, 85000, 255000),
    (N'HD044', N'S018', 1, 180000, 180000),
    (N'HD044', N'S019', 2, 92000, 184000),
    (N'HD045', N'S020', 1, 68000, 68000),
    (N'HD045', N'S021', 3, 75000, 225000),
    (N'HD046', N'S022', 1, 150000, 150000),
    (N'HD046', N'S023', 2, 229000, 458000),
    (N'HD047', N'S024', 1, 145000, 145000),
    (N'HD047', N'S025', 2, 110000, 220000),
    (N'HD048', N'S026', 3, 79000, 237000),
    (N'HD048', N'S027', 1, 90000, 90000),
    (N'HD049', N'S028', 2, 75000, 150000),
    (N'HD049', N'S029', 3, 82000, 246000),
    (N'HD050', N'S030', 1, 80000, 80000),
    (N'HD050', N'S031', 2, 109000, 218000);

    -- Thêm dữ liệu vào bảng tblNhap (50 bản ghi)
    INSERT INTO tblNhap VALUES
    (N'PN001', N'NV001', '2024-01-02', 1, 2500000),
    (N'PN002', N'NV002', '2024-01-03', 1, 3000000),
    (N'PN003', N'NV003', '2024-01-04', 1, 2800000),
    (N'PN004', N'NV004', '2024-01-05', 1, 3200000),
    (N'PN005', N'NV005', '2024-01-06', 1, 2700000),
    (N'PN006', N'NV001', '2024-01-07', 1, 3100000),
    (N'PN007', N'NV002', '2024-01-08', 1, 2900000),
    (N'PN008', N'NV003', '2024-01-09', 1, 3300000),
    (N'PN009', N'NV004', '2024-01-10', 1, 2600000),
    (N'PN010', N'NV005', '2024-01-11', 1, 3400000),
    (N'PN011', N'NV001', '2024-01-12', 1, 2800000),
    (N'PN012', N'NV002', '2024-01-13', 1, 3200000),
    (N'PN013', N'NV003', '2024-01-14', 1, 3000000),
    (N'PN014', N'NV004', '2024-01-15', 1, 2700000),
    (N'PN015', N'NV005', '2024-01-16', 1, 3500000),
    (N'PN016', N'NV001', '2024-01-17', 1, 2900000),
    (N'PN017', N'NV002', '2024-01-18', 1, 3100000),
    (N'PN018', N'NV003', '2024-01-19', 1, 3300000),
    (N'PN019', N'NV004', '2024-01-20', 1, 2800000),
    (N'PN020', N'NV005', '2024-01-21', 1, 3400000),
    (N'PN021', N'NV001', '2024-01-22', 1, 3000000),
    (N'PN022', N'NV002', '2024-01-23', 1, 2600000),
    (N'PN023', N'NV003', '2024-01-24', 1, 3200000),
    (N'PN024', N'NV004', '2024-01-25', 1, 2900000),
    (N'PN025', N'NV005', '2024-01-26', 1, 3500000),
    (N'PN026', N'NV001', '2024-01-27', 1, 3100000),
    (N'PN027', N'NV002', '2024-01-28', 1, 2700000),
    (N'PN028', N'NV003', '2024-01-29', 1, 3300000),
    (N'PN029', N'NV004', '2024-01-30', 1, 3000000),
    (N'PN030', N'NV005', '2024-01-31', 1, 2800000),
    (N'PN031', N'NV001', '2024-02-01', 1, 3400000),
    (N'PN032', N'NV002', '2024-02-02', 1, 2900000),
    (N'PN033', N'NV003', '2024-02-03', 1, 3200000),
    (N'PN034', N'NV004', '2024-02-04', 1, 3000000),
    (N'PN035', N'NV005', '2024-02-05', 1, 2600000),
    (N'PN036', N'NV001', '2024-02-06', 1, 3500000),
    (N'PN037', N'NV002', '2024-02-07', 1, 3100000),
    (N'PN038', N'NV003', '2024-02-08', 1, 2800000),
    (N'PN039', N'NV004', '2024-02-09', 1, 3300000),
    (N'PN040', N'NV005', '2024-02-10', 1, 3000000),
    (N'PN041', N'NV001', '2024-02-11', 1, 2700000),
    (N'PN042', N'NV002', '2024-02-12', 1, 3400000),
    (N'PN043', N'NV003', '2024-02-13', 1, 2900000),
    (N'PN044', N'NV004', '2024-02-14', 1, 3200000),
    (N'PN045', N'NV005', '2024-02-15', 1, 3100000),
    (N'PN046', N'NV001', '2024-02-16', 1, 2800000),
    (N'PN047', N'NV002', '2024-02-17', 1, 3300000),
    (N'PN048', N'NV003', '2024-02-18', 1, 3000000),
    (N'PN049', N'NV004', '2024-02-19', 1, 2600000),
    (N'PN050', N'NV005', '2024-02-20', 1, 3500000);

    -- Thêm dữ liệu vào bảng tblChiTietNhap
    INSERT INTO tblChiTietNhap VALUES
    (N'PN001', N'S001', 20, 120000, 2400000),
    (N'PN001', N'S002', 15, 100000, 1500000),
    (N'PN002', N'S003', 25, 80000, 2000000),
    (N'PN002', N'S004', 18, 110000, 1980000),
    (N'PN003', N'S005', 22, 115000, 2530000),
    (N'PN003', N'S006', 30, 75000, 2250000),
    (N'PN004', N'S007', 20, 70000, 1400000),
    (N'PN004', N'S008', 25, 90000, 2250000),
    (N'PN005', N'S009', 15, 130000, 1950000),
    (N'PN005', N'S010', 28, 60000, 1680000),
    (N'PN006', N'S011', 20, 85000, 1700000),
    (N'PN006', N'S012', 18, 100000, 1800000),
    (N'PN007', N'S013', 25, 70000, 1750000),
    (N'PN007', N'S014', 30, 55000, 1650000),
    (N'PN008', N'S015', 22, 95000, 2090000),
    (N'PN008', N'S016', 20, 70000, 1400000),
    (N'PN009', N'S017', 25, 65000, 1625000),
    (N'PN009', N'S018', 15, 150000, 2250000),
    (N'PN010', N'S019', 28, 75000, 2100000),
    (N'PN010', N'S020', 30, 55000, 1650000),
    (N'PN011', N'S021', 20, 60000, 1200000),
    (N'PN011', N'S022', 18, 120000, 2160000),
    (N'PN012', N'S023', 15, 180000, 2700000),
    (N'PN012', N'S024', 25, 115000, 2875000),
    (N'PN013', N'S025', 22, 90000, 1980000),
    (N'PN013', N'S026', 30, 65000, 1950000),
    (N'PN014', N'S027', 20, 75000, 1500000),
    (N'PN014', N'S028', 25, 60000, 1500000),
    (N'PN015', N'S029', 18, 65000, 1170000),
    (N'PN015', N'S030', 28, 65000, 1820000),
    (N'PN016', N'S031', 22, 85000, 1870000),
    (N'PN016', N'S032', 30, 60000, 1800000),
    (N'PN017', N'S033', 20, 90000, 1800000),
    (N'PN017', N'S034', 25, 75000, 1875000),
    (N'PN018', N'S035', 18, 55000, 990000),
    (N'PN018', N'S001', 28, 120000, 3360000),
    (N'PN019', N'S002', 22, 100000, 2200000),
    (N'PN019', N'S003', 30, 80000, 2400000),
    (N'PN020', N'S004', 25, 110000, 2750000),
    (N'PN020', N'S005', 20, 115000, 2300000),
    (N'PN021', N'S006', 18, 75000, 1350000),
    (N'PN021', N'S007', 28, 70000, 1960000),
    (N'PN022', N'S008', 22, 90000, 1980000),
    (N'PN022', N'S009', 30, 130000, 3900000),
    (N'PN023', N'S010', 25, 60000, 1500000),
    (N'PN023', N'S011', 20, 85000, 1700000),
    (N'PN024', N'S012', 18, 100000, 1800000),
    (N'PN024', N'S013', 28, 70000, 1960000),
    (N'PN025', N'S014', 22, 55000, 1210000),
    (N'PN025', N'S015', 30, 95000, 2850000),
    (N'PN026', N'S016', 25, 70000, 1750000),
    (N'PN026', N'S017', 20, 65000, 1300000),
    (N'PN027', N'S018', 18, 150000, 2700000),
    (N'PN027', N'S019', 28, 75000, 2100000),
    (N'PN028', N'S020', 22, 55000, 1210000),
    (N'PN028', N'S021', 30, 60000, 1800000),
    (N'PN029', N'S022', 25, 120000, 3000000),
    (N'PN029', N'S023', 20, 180000, 3600000),
    (N'PN030', N'S024', 18, 115000, 2070000),
    (N'PN030', N'S025', 28, 90000, 2520000),
    (N'PN031', N'S026', 22, 65000, 1430000),
    (N'PN031', N'S027', 30, 75000, 2250000),
    (N'PN032', N'S028', 25, 60000, 1500000),
    (N'PN032', N'S029', 20, 65000, 1300000),
    (N'PN033', N'S030', 18, 65000, 1170000),
    (N'PN033', N'S031', 28, 109000, 3052000),
    (N'PN034', N'S032', 22, 78000, 1716000),
    (N'PN034', N'S033', 30, 115000, 3450000),
    (N'PN035', N'S034', 25, 95000, 2375000),
    (N'PN035', N'S035', 20, 69000, 1380000),
    (N'PN036', N'S001', 18, 150000, 2700000),
    (N'PN036', N'S002', 28, 120000, 3360000),
    (N'PN037', N'S003', 22, 100000, 2200000),
    (N'PN037', N'S004', 30, 130000, 3900000),
    (N'PN038', N'S005', 25, 140000, 3500000),
    (N'PN038', N'S006', 20, 95000, 1900000),
    (N'PN039', N'S007', 18, 88000, 1584000),
    (N'PN039', N'S008', 28, 110000, 3080000),
    (N'PN040', N'S009', 22, 150000, 3300000),
    (N'PN040', N'S010', 30, 78000, 2340000),
    (N'PN041', N'S011', 25, 108000, 2700000),
    (N'PN041', N'S012', 20, 125000, 2500000),
    (N'PN042', N'S013', 18, 86000, 1548000),
    (N'PN042', N'S014', 28, 69000, 1932000),
    (N'PN043', N'S015', 22, 119000, 2618000),
    (N'PN043', N'S016', 30, 86000, 2580000),
    (N'PN044', N'S017', 25, 85000, 2125000),
    (N'PN044', N'S018', 20, 180000, 3600000),
    (N'PN045', N'S019', 18, 92000, 1656000),
    (N'PN045', N'S020', 28, 68000, 1904000),
    (N'PN046', N'S021', 22, 75000, 1650000),
    (N'PN046', N'S022', 30, 150000, 4500000),
    (N'PN047', N'S023', 25, 229000, 5725000),
    (N'PN047', N'S024', 20, 145000, 2900000),
    (N'PN048', N'S025', 18, 110000, 1980000),
    (N'PN048', N'S026', 28, 79000, 2212000),
    (N'PN049', N'S027', 22, 90000, 1980000),
    (N'PN049', N'S028', 30, 75000, 2250000),
    (N'PN050', N'S029', 25, 82000, 2050000),
    (N'PN050', N'S030', 20, 80000, 1600000);

COMMIT TRANSACTION
    PRINT 'Script executed successfully'
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION
    PRINT 'An error occurred. Changes have been rolled back.'
    SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
END CATCH
-- View

CREATE VIEW View_XemDanhSachSach AS
SELECT * FROM tblSach;
GO

CREATE VIEW View_XemDanhSachNhanVien AS
SELECT * FROM tblNhanVien;
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
    FROM tblNhanVien NV
    INNER JOIN tblTaiKhoan TK ON NV.sMaNV = TK.sMaNV;
END;

CREATE VIEW vwNhanvienHoadonChitietHD
AS
SELECT cthd.sMaHD, hd.dNgaylap, hd.sMaNV, nv.sTenNV, cthd.sMaSach, s.sTensach, cthd.iSoLuong, cthd.fDongia, cthd.fThanhtien
FROM     dbo.tblChiTietHD AS cthd INNER JOIN
                  dbo.tblHoaDon AS hd ON cthd.sMaHD = hd.sMaHD INNER JOIN
                  dbo.tblNhanVien AS nv ON hd.sMaNV = nv.sMaNV INNER JOIN
                  dbo.tblSach AS s ON cthd.sMaSach = s.sMasach
GO

CREATE VIEW viewNVPW
AS
SELECT nv.*,
       COALESCE(tk.sMatkhau, '') AS sMatkhau
FROM tblNhanVien nv
LEFT JOIN tblTaiKhoan tk ON nv.sMaNV = tk.sMaNV;
Go