CREATE TABLE KichThuoc(
	MaKichThuoc VARCHAR(50)PRIMARY KEY,
	TenKichThuoc NVARCHAR(100) NOT NULL
	);

CREATE TABLE Loai (
    MaLoai VARCHAR(50) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL
);

CREATE TABLE KhachHang (
    MaKhach VARCHAR(50)PRIMARY KEY,
    TenKhach NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    DienThoai CHAR(10) CHECK (DienThoai LIKE '[0-9]%')
);

CREATE TABLE NhaCungCap (
    MaNCC VARCHAR(50)PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    DienThoai CHAR(10) CHECK (DienThoai LIKE '[0-9]%')
);

CREATE TABLE CongViec (
    MaCV VARCHAR(50)PRIMARY KEY,
    TenCV NVARCHAR(100) NOT NULL,
    MucLuong DECIMAL(18, 2) NOT NULL
);

CREATE TABLE NhanVien (
    MaNhanVien VARCHAR(50)PRIMARY KEY ,
    TenNhanVien NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    NgaySinh DATE,
    DienThoai CHAR(10) CHECK (DienThoai LIKE '[0-9]%'),
    DiaChi NVARCHAR(200),
    MaCV VARCHAR(50) FOREIGN KEY REFERENCES CongViec(MaCV) ON DELETE CASCADE
);

CREATE TABLE NhomTranhAnh (
    MaNhom VARCHAR(50)PRIMARY KEY,
    TenNhom NVARCHAR(100) NOT NULL
);

CREATE TABLE ChatLieu (
    MaChatLieu VARCHAR(50)PRIMARY KEY,
    TenChatLieu NVARCHAR(100) NOT NULL
);

CREATE TABLE NoiSanXuat (
    MaNoiSX VARCHAR(50)PRIMARY KEY,
    TenNoiSX NVARCHAR(100) NOT NULL
);

CREATE TABLE MauSac (
    MaMau VARCHAR(50)PRIMARY KEY,
    TenMau NVARCHAR(100) NOT NULL
);

CREATE TABLE KhungAnh (
    MaKhung VARCHAR(50)PRIMARY KEY,
    TenKhung NVARCHAR(100) NOT NULL
);
CREATE TABLE CongDong (
    MaCongDong VARCHAR(50)PRIMARY KEY,
    TenCongDong NVARCHAR(100) NOT NULL
);
CREATE TABLE DMHangHoa (
    MaHang VARCHAR(50)PRIMARY KEY,
    TenHangHoa NVARCHAR(100) NOT NULL,
    MaLoai VARCHAR(50) FOREIGN KEY REFERENCES Loai(MaLoai) ON DELETE CASCADE,
    MaKichThuoc VARCHAR(50) FOREIGN KEY REFERENCES KichThuoc(MaKichThuoc) ON DELETE CASCADE,
    MaNhom VARCHAR(50) FOREIGN KEY REFERENCES NhomTranhAnh(MaNhom) ON DELETE CASCADE,
    MaChatLieu VARCHAR(50) FOREIGN KEY REFERENCES ChatLieu(MaChatLieu) ON DELETE CASCADE,
	MaCongDong VARCHAR(50) FOREIGN KEY REFERENCES CongDong(MaCongDong) ON DELETE CASCADE,
    MaKhung VARCHAR(50) FOREIGN KEY REFERENCES KhungAnh(MaKhung) ON DELETE CASCADE,
    MaMau VARCHAR(50) FOREIGN KEY REFERENCES MauSac(MaMau) ON DELETE CASCADE,
    MaNoiSX VARCHAR(50) FOREIGN KEY REFERENCES NoiSanXuat(MaNoiSX) ON DELETE CASCADE,
    SoLuong INT,
    DonGiaNhap DECIMAL(18, 2),
    DonGiaBan DECIMAL(18, 2),
    ThoiGianBaoHanh INT,
    Anh NVARCHAR(MAX), -- Dùng để lưu link ảnh
    GhiChu NVARCHAR(200)
);

CREATE TABLE HoaDonBan (
    SoHDB VARCHAR(50)PRIMARY KEY,
    MaNhanVien VARCHAR(50) FOREIGN KEY REFERENCES NhanVien(MaNhanVien) ON DELETE CASCADE,
    NgayBan DATE,
    MaKhach VARCHAR(50) FOREIGN KEY REFERENCES KhachHang(MaKhach) ON DELETE CASCADE,
    TongTien DECIMAL(18, 2) NOT NULL DEFAULT 0
);

CREATE TABLE HoaDonNhap (
    SoHDN VARCHAR(50)PRIMARY KEY,
    MaNhanVien VARCHAR(50) FOREIGN KEY REFERENCES NhanVien(MaNhanVien) ON DELETE CASCADE,
    NgayNhap DATE,
    MaNCC VARCHAR(50) FOREIGN KEY REFERENCES NhaCungCap(MaNCC) ON DELETE CASCADE,
    TongTien DECIMAL(18, 2) NOT NULL DEFAULT 0
);

CREATE TABLE ChiTietHoaDonBan (
    SoHDB VARCHAR(50) FOREIGN KEY REFERENCES HoaDonBan(SoHDB) ON DELETE CASCADE,
    MaHang VARCHAR(50) FOREIGN KEY REFERENCES DMHangHoa(MaHang) ON DELETE CASCADE,
    SoLuong INT NOT NULL,
    DonGiaBan DECIMAL(18, 2) NOT NULL, -- Thêm cột DonGiaBan
    GiamGia DECIMAL(5, 2),
    ThanhTien AS (SoLuong * (DonGiaBan * (1 - GiamGia / 100))) PERSISTED, -- Sử dụng DonGiaBan trong công thức
    PRIMARY KEY (SoHDB,MaHang)
);

CREATE TABLE ChiTietHoaDonNhap (
    SoHDN VARCHAR(50) FOREIGN KEY REFERENCES HoaDonNhap(SoHDN) ON DELETE CASCADE,
    MaHang VARCHAR(50) FOREIGN KEY REFERENCES DMHangHoa(MaHang) ON DELETE CASCADE,
    SoLuong VARCHAR(50) NOT NULL,
    DonGia DECIMAL(18, 2), -- DonGia được dùng trực tiếp trong bảng này
    GiamGia DECIMAL(5, 2),
    ThanhTien AS (SoLuong * (DonGia * (1 - GiamGia / 100))) PERSISTED, -- ThanhTien dựa trên DonGia
    PRIMARY KEY (SoHDN,MaHang)
);


INSERT INTO KichThuoc (MaKichThuoc,TenKichThuoc) VALUES ('KT001',N'10x10');
INSERT INTO KichThuoc (MaKichThuoc,TenKichThuoc) VALUES ('KT002',N'20x20');
INSERT INTO KichThuoc (MaKichThuoc,TenKichThuoc) VALUES ('KT003',N'30x30');
INSERT INTO KichThuoc (MaKichThuoc,TenKichThuoc) VALUES ('KT004',N'40x40');
INSERT INTO KichThuoc (MaKichThuoc,TenKichThuoc) VALUES ('KT005',N'50x50');

INSERT INTO Loai (MaLoai,TenLoai) VALUES ('L001',N'Tranh Sơn Dầu');
INSERT INTO Loai (MaLoai,TenLoai) VALUES ('L002',N'Ảnh Phong Cảnh');
INSERT INTO Loai (MaLoai,TenLoai) VALUES ('L003','Tranh 3D');
INSERT INTO Loai (MaLoai,TenLoai) VALUES ('L004',N'Ảnh Chân Dung');
INSERT INTO Loai (MaLoai,TenLoai) VALUES ('L005',N'Tranh Trừu Tượng');

INSERT INTO KhachHang (MaKhach,TenKhach, DiaChi, DienThoai) VALUES ('KH001',N'Nguyễn Văn A', N'Hà Nội', '0987654321');
INSERT INTO KhachHang (MaKhach,TenKhach, DiaChi, DienThoai) VALUES ('KH002',N'Trần Thị B', N'Hồ Chí Minh', '0971234567');
INSERT INTO KhachHang (MaKhach,TenKhach, DiaChi, DienThoai) VALUES ('KH003',N'Phạm Văn C', N'Đà Nẵng', '0965432187');
INSERT INTO KhachHang (MaKhach,TenKhach, DiaChi, DienThoai) VALUES ('KH004',N'Ngô Thị D', N'Bắc Ninh', '0998765432');
INSERT INTO KhachHang (MaKhach,TenKhach, DiaChi, DienThoai) VALUES ('KH005',N'Hoàng Văn E', N'Cần Thơ', '0945678901');

INSERT INTO NhaCungCap (MaNCC,TenNCC, DiaChi, DienThoai) VALUES ('NCC001',N'Công ty Tranh X', N'Hà Nội', '0912345678');
INSERT INTO NhaCungCap (MaNCC,TenNCC, DiaChi, DienThoai) VALUES ('NCC002',N'Công ty Tranh Y', N'Hồ Chí Minh', '0938765432');
INSERT INTO NhaCungCap (MaNCC,TenNCC, DiaChi, DienThoai) VALUES ('NCC003',N'Công ty Tranh Z', N'Đà Nẵng', '0921345678');
INSERT INTO NhaCungCap (MaNCC,TenNCC, DiaChi, DienThoai) VALUES ('NCC004',N'Công ty Tranh W', N'Bắc Ninh', '0945678902');
INSERT INTO NhaCungCap (MaNCC,TenNCC, DiaChi, DienThoai) VALUES ('NCC005',N'Công ty Tranh V', N'Cần Thơ', '0956789012');

INSERT INTO CongViec (MaCV,TenCV, MucLuong) VALUES ('CV001',N'Quản lý', 15000000);
INSERT INTO CongViec (MaCV,TenCV, MucLuong) VALUES ('CV002',N'Nhân viên kinh doanh', 12000000);
INSERT INTO CongViec (MaCV,TenCV, MucLuong) VALUES ('CV003',N'Thợ làm khung', 10000000);
INSERT INTO CongViec (MaCV,TenCV, MucLuong) VALUES ('CV004',N'Thợ vẽ tranh', 13000000);
INSERT INTO CongViec (MaCV,TenCV, MucLuong) VALUES ('CV005',N'Nhân viên giao hàng', 8000000);

INSERT INTO NhanVien (MaNhanVien,TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) 
VALUES ('NV001',N'Nguyễn Văn Nam', N'Nam', '1985-10-10', '0988888888', N'Hà Nội', 'CV001');
INSERT INTO NhanVien (MaNhanVien,TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) 
VALUES ('NV002',N'Trần Thị Mai', N'Nữ', '1990-05-12', '0977777777', N'Hồ Chí Minh', 'CV002');
INSERT INTO NhanVien (MaNhanVien,TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) 
VALUES ('NV003',N'Phạm Văn Hòa', N'Nam', '1992-08-15', '0966666666', N'Đà Nẵng', 'CV003');
INSERT INTO NhanVien (MaNhanVien,TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) 
VALUES ('NV004',N'Hoàng Thị Lan', N'Nữ', '1995-12-20', '0999999999', N'Bắc Ninh', 'CV004');
INSERT INTO NhanVien (MaNhanVien,TenNhanVien, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) 
VALUES ('NV005',N'Ngô Văn Long', N'Nam', '2000-03-10', '0944444444', N'Cần Thơ', 'CV005');

INSERT INTO ChatLieu (MaChatLieu,TenChatLieu) VALUES ('CL001',N'Gỗ');
INSERT INTO ChatLieu (MaChatLieu,TenChatLieu) VALUES ('CL002',N'Nhựa');
INSERT INTO ChatLieu (MaChatLieu,TenChatLieu) VALUES ('CL003',N'Kim loại');
INSERT INTO ChatLieu (MaChatLieu,TenChatLieu) VALUES ('CL004',N'Sợi tổng hợp');
INSERT INTO ChatLieu (MaChatLieu,TenChatLieu) VALUES ('CL005',N'Giấy');

INSERT INTO NoiSanXuat (MaNoiSX,TenNoiSX) VALUES ('NSX001',N'Hà Nội');
INSERT INTO NoiSanXuat (MaNoiSX,TenNoiSX) VALUES ('NSX002',N'Hồ Chí Minh');
INSERT INTO NoiSanXuat (MaNoiSX,TenNoiSX) VALUES ('NSX003',N'Đà Nẵng');
INSERT INTO NoiSanXuat (MaNoiSX,TenNoiSX) VALUES ('NSX004',N'Cần Thơ');
INSERT INTO NoiSanXuat (MaNoiSX,TenNoiSX) VALUES ('NSX005',N'Hải Phòng');

INSERT INTO MauSac (MaMau,TenMau) VALUES ('M001',N'Xanh');
INSERT INTO MauSac (MaMau,TenMau) VALUES ('M002',N'Đỏ');
INSERT INTO MauSac (MaMau,TenMau) VALUES ('M003',N'Vàng');
INSERT INTO MauSac (MaMau,TenMau) VALUES ('M004',N'Tím');
INSERT INTO MauSac (MaMau,TenMau) VALUES ('M005',N'Hồng');

INSERT INTO KhungAnh (MaKhung,TenKhung) VALUES ('K001',N'Khung Gỗ');
INSERT INTO KhungAnh (MaKhung,TenKhung) VALUES ('K002',N'Khung Nhựa');
INSERT INTO KhungAnh (MaKhung,TenKhung) VALUES ('K003',N'Khung Kim Loại');
INSERT INTO KhungAnh (MaKhung,TenKhung) VALUES ('K004',N'Khung Composite');
INSERT INTO KhungAnh (MaKhung,TenKhung) VALUES ('K005',N'Khung Tre');

INSERT INTO CongDong (MaCongDong, TenCongDong) 
VALUES 
    ('CD01', N'Cộng Đồng Nghệ Thuật'),
    ('CD02', N'Cộng Đồng Sáng Tạo'),
    ('CD03', N'Cộng Đồng Đồ Gỗ'),
    ('CD04', N'Cộng Đồng Tranh 3D'),
    ('CD05', N'Cộng Đồng Sơn Dầu')

INSERT INTO NhomTranhAnh (MaNhom,TenNhom) VALUES ('N001',N'Phong Cảnh');
INSERT INTO NhomTranhAnh (MaNhom,TenNhom) VALUES ('N002',N'Chân Dung');
INSERT INTO NhomTranhAnh (MaNhom,TenNhom) VALUES ('N003',N'Tĩnh Vật');
INSERT INTO NhomTranhAnh (MaNhom,TenNhom) VALUES ('N004',N'Abstract');
INSERT INTO NhomTranhAnh (MaNhom,TenNhom) VALUES ('N005',N'3D');

-- Inserting into DMHangHoa (Product List)
INSERT INTO DMHangHoa (MaHang,TenHangHoa, MaLoai, MaKichThuoc, MaNhom, MaChatLieu, MaKhung, MaCongDong, MaMau, MaNoiSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh, GhiChu)
VALUES 
    ('HH001',N'Tranh Sơn Dầu 1', 'L001', 'KT001', 'N001', 'CL001', 'K001', 'CD01', 'M001', 'NSX001', 10, 200000, 300000, 12, N'https://example.com/image1.jpg', N'Sản phẩm đẹp'),
    ('HH002',N'Tranh Sơn Dầu 2', 'L002', 'KT002', 'N002', 'CL002', 'K002', 'CD02', 'M002', 'NSX002', 20, 250000, 350000, 24, N'https://example.com/image2.jpg', N'Mẫu mới'),
    ('HH003',N'Tranh Sơn Dầu 3', 'L003', 'KT003', 'N003', 'CL003', 'K003', 'CD03', 'M003', 'NSX003', 15, 300000, 450000, 18, N'https://example.com/image3.jpg', N'Giá ưu đãi'),
    ('HH004',N'Tranh Sơn Dầu 4', 'L004', 'KT004', 'N004', 'CL004', 'K004', 'CD04', 'M004', 'NSX004', 12, 400000, 600000, 36, N'https://example.com/image4.jpg', N'Tranh cao cấp'),
    ('HH005',N'Tranh Sơn Dầu 5', 'L005', 'KT005', 'N005', 'CL005', 'K005', 'CD05', 'M005', 'NSX005', 8, 500000, 750000, 24, N'https://example.com/image5.jpg', N'Khuyến mãi');


INSERT INTO HoaDonBan (SoHDB,MaNhanVien, NgayBan, MaKhach, TongTien) 
VALUES ('HDB001','NV001', '2024-01-01', 'KH001', 1500000);
INSERT INTO HoaDonBan (SoHDB,MaNhanVien, NgayBan, MaKhach, TongTien) 
VALUES ('HDB002','NV002', '2024-01-02', 'KH002', 1545000);
INSERT INTO HoaDonBan (SoHDB,MaNhanVien, NgayBan, MaKhach, TongTien) 
VALUES ('HDB003','NV004', '2024-01-01', 'KH004', 1400200);
INSERT INTO HoaDonBan (SoHDB,MaNhanVien, NgayBan, MaKhach, TongTien) 
VALUES ('HDB004','NV003', '2024-01-01', 'KH003', 1540000);
INSERT INTO HoaDonBan (SoHDB,MaNhanVien, NgayBan, MaKhach, TongTien) 
VALUES ('HDB005','NV005', '2024-01-01', 'KH005', 1550000);

-- Inserting into HoaDonNhap (Purchase Invoice)
INSERT INTO HoaDonNhap (SoHDN,MaNhanVien, NgayNhap, MaNCC, TongTien)
VALUES 
    ('HDN001','NV001', '2024-01-10', 'NCC001', 1000000),
    ('HDN002','NV002', '2024-02-15', 'NCC002', 1500000),
    ('HDN003','NV003', '2024-03-20', 'NCC003', 1200000),
    ('HDN004','NV004', '2024-04-25', 'NCC004', 2000000),
    ('HDN005','NV005', '2024-05-30', 'NCC005', 2500000);


-- Inserting into ChiTietHoaDonBan (Sales Invoice Details)
INSERT INTO ChiTietHoaDonBan (SoHDB, MaHang, SoLuong, DonGiaBan, GiamGia)
VALUES 
    ('HDB001', 'HH001', 2, 300000, 10),
    ('HDB002', 'HH002', 3, 350000, 5),
    ('HDB003', 'HH003', 1, 450000, 0),
    ('HDB004', 'HH004', 4, 600000, 15),
    ('HDB005', 'HH005', 2, 750000, 20);


-- Inserting into ChiTietHoaDonNhap (Purchase Invoice Details)
INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia)
VALUES 
    ('HDN001', 'HH001', 5, 200000, 10),
    ('HDN002', 'HH002', 10, 250000, 5),
    ('HDN003', 'HH003', 3, 300000, 0),
    ('HDN004', 'HH004', 7, 400000, 15),
    ('HDN005', 'HH005', 2, 500000, 20);
