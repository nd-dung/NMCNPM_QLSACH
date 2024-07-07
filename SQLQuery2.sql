CREATE VIEW vwNhanVienWithPassword
AS
SELECT nv.*, tk.sMatKhau
FROM vwNhanVienWithPassword nv
JOIN tblTaiKhoan tk ON nv.sMaNV = tk.sMaNV;
select * from  vwNhanVienWithPassword