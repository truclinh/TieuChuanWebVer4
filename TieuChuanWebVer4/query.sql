/* Store procedure Thêm mới Tiêu chuẩn */
CREATE PROCEDURE sp_ThemMoiTieuChuan
    @id uniqueidentifier,
    @ma_tieuchuan nvarchar(50),
    @ten_tieuchuan nvarchar(256),
	@nguoitao nvarchar(50),
	@ngaytao datetime,
	@noidung nvarchar(MAX)
AS
BEGIN
   Insert into dm_tieuchuan(id,ma_tieuchuan,ten_tieuchuan,ghichu,nguoitao,ngaytao,nguoisua,ngaysua,noidung)
    Values(@id,@ma_tieuchuan,@ten_tieuchuan,NULL,@nguoitao,@ngaytao,NULL,NULL,@noidung)
END

/* Store procedure Cập nhật Tiêu chuẩn */
CREATE PROCEDURE sp_CapNhatTieuChuan
    @id uniqueidentifier,
    @ma_tieuchuan nvarchar(50),
    @ten_tieuchuan nvarchar(256),
	@nguoisua nvarchar(50),
	@ngaysua datetime,
	@noidung nvarchar(MAX)
AS
BEGIN
    UPDATE dm_tieuchuan
    SET ma_tieuchuan=ISNULL(@ma_tieuchuan,ma_tieuchuan), 
        ten_tieuchuan=ISNULL(@ten_tieuchuan,ten_tieuchuan), 
        nguoisua=ISNULL(@nguoisua, nguoisua),
		ngaysua=ISNULL(@ngaysua, ngaysua),
		noidung=ISNULL(@noidung, noidung)
    WHERE id=@id
END

/* Store procedure Thêm mới Tiêu chí */
CREATE PROCEDURE sp_ThemMoiTieuChi
    @id uniqueidentifier,
    @ma_tieuchi nvarchar(50),
    @ten_tieuchi nvarchar(512),
	@ma_tieuchuan nvarchar(50),
	@nguoitao nvarchar(50),
	@ngaytao datetime,
	@noidung nvarchar(512)
AS
BEGIN
   Insert into dm_tieuchi(id,ma_tieuchi,ten_tieuchi,ma_tieuchuan,ghichu,nguoitao,ngaytao,nguoisua,ngaysua,noidung)
    Values(@id,@ma_tieuchi,@ten_tieuchi,@ma_tieuchuan,NULL,@nguoitao,@ngaytao,NULL,NULL,@noidung)
END

/* Store procedure Cập nhật Tiêu chí */
CREATE PROCEDURE sp_CapNhatTieuChi
    @id uniqueidentifier,
    @ma_tieuchi nvarchar(50),
    @ten_tieuchi nvarchar(512),
	@ma_tieuchuan nvarchar(50),
	@nguoisua nvarchar(50),
	@ngaysua datetime,
	@noidung nvarchar(512)
AS
BEGIN
    UPDATE dm_tieuchi
    SET ma_tieuchi=ISNULL(@ma_tieuchi,ma_tieuchi), 
        ten_tieuchi=ISNULL(@ten_tieuchi,ten_tieuchi), 
		ma_tieuchuan=ISNULL(@ma_tieuchuan,ma_tieuchuan),
        nguoisua=ISNULL(@nguoisua, nguoisua),
		ngaysua=ISNULL(@ngaysua, ngaysua),
		noidung=ISNULL(@noidung, noidung)
    WHERE id=@id
END

/* Store procedure Thêm mới Quản lý người dùng */
CREATE PROCEDURE sp_ThemMoiQLNguoiDung
    @id uniqueidentifier,
    @ma_nsd nvarchar(50),
    @ten_nsd nvarchar(256),
	@matkhau nvarchar(50),
	@makhoa nvarchar(50),
	@mabomon nvarchar(50),
	@ma_nhom nvarchar(50),
	@ghichu nvarchar(128),
	@nguoitao nvarchar(50),
	@ngaytao datetime
AS
BEGIN
   Insert into ht_dm_nsd(id,ma_nsd,ten_nsd,matkhau,makhoa,mabomon,ma_nhom,ghichu,nguoitao,ngaytao,nguoisua,ngaysua,uyquyen,ds_tk)
    Values(@id,@ma_nsd,@ten_nsd,@matkhau,@makhoa,@mabomon,@ma_nhom,@ghichu,@nguoitao,@ngaytao,NULL,NULL,NULL,NULL)
END

/* Store procedure Cập nhật Quản lý người dùng */
CREATE PROCEDURE sp_CapNhatQLNguoiDung
    @id uniqueidentifier,
    @ma_nsd nvarchar(50),
    @ten_nsd nvarchar(256),
	@matkhau nvarchar(50),
	@makhoa nvarchar(50),
	@mabomon nvarchar(50),
	@ma_nhom nvarchar(50),
	@ghichu nvarchar(128),
	@nguoisua nvarchar(50),
	@ngaysua datetime
AS
BEGIN
    UPDATE ht_dm_nsd
    SET ma_nsd=ISNULL(@ma_nsd,ma_nsd), 
        ten_nsd=ISNULL(@ten_nsd,ten_nsd), 
		matkhau=ISNULL(@matkhau,matkhau), 
		makhoa=ISNULL(@makhoa,makhoa),
		mabomon=ISNULL(@mabomon,mabomon),
		ma_nhom=ISNULL(@ma_nhom,ma_nhom),
		ghichu=ISNULL(@ghichu,ghichu),
		nguoisua=ISNULL(@nguoisua, nguoisua),
		ngaysua=ISNULL(@ngaysua, ngaysua)
    WHERE id=@id
END

/* Store procedure Thêm mới Thông tin khoa */
CREATE PROCEDURE sp_ThemMoiThongTinKhoa
    @id uniqueidentifier,
    @makhoa nvarchar(50),
    @tenkhoa nvarchar(200)
AS
BEGIN
   Insert into dm_khoa(id,makhoa,tenkhoa)
    Values(@id,@makhoa,@tenkhoa)
END

/* Store procedure Cập nhật Thông tin khoa */
CREATE PROCEDURE sp_CapNhatThongTinKhoa
    @id uniqueidentifier,
    @makhoa nvarchar(50),
    @tenkhoa nvarchar(200)
AS
BEGIN
    UPDATE dm_khoa
    SET makhoa=ISNULL(@makhoa,makhoa), 
        tenkhoa=ISNULL(@tenkhoa,tenkhoa)
    WHERE id=@id
END
/* Store procedure Thêm mới Thông tin bộ môn */
CREATE PROCEDURE sp_ThemMoiThongTinBM
    @id uniqueidentifier,
    @mabomon nvarchar(50),
    @tenbomon nvarchar(200),
	@makhoa nvarchar(50),
	@googledrive nvarchar(MAX)
AS
BEGIN
   Insert into dm_bomon(id,mabomon,tenbomon,makhoa,googledrive)
    Values(@id,@mabomon,@tenbomon,@makhoa,@googledrive)
END

/* Store procedure Cập nhật Thông tin khoa */
CREATE PROCEDURE sp_CapNhatThongTinBM
    @id uniqueidentifier,
    @mabomon nvarchar(50),
    @tenbomon nvarchar(200),
	@makhoa nvarchar(50),
	@googledrive nvarchar(MAX)
AS
BEGIN
    UPDATE dm_bomon
    SET mabomon=ISNULL(@mabomon,mabomon), 
        tenbomon=ISNULL(@tenbomon,tenbomon),
		makhoa=ISNULL(@makhoa,makhoa),
		googledrive=ISNULL(@googledrive,googledrive)
    WHERE id=@id
END
/* Store procedure Thêm mới Tập tin Master */
CREATE PROCEDURE sp_CapNhapMaster
    @soid uniqueidentifier,
    @dinhdanh nvarchar(256),
    @ma_tieuchi nvarchar(50),
	@nguoitao nvarchar(50),
	@ngaytao datetime
AS
BEGIN
   Insert into hs_tapting(soid,dinhdanh,ma_tieuchi,nguoitao,ngaytao)
    Values(@soid,@dinhdanh,@ma_tieuchi,@nguoitao,@ngaytao)
END

/* Store procedure Thêm mới Tập tin Detail */
CREATE PROCEDURE sp_CapNhapDetail
	@id  uniqueidentifier,
    @soid uniqueidentifier,
    @tentaptin nvarchar(512),
    @driveid nvarchar(512),
	@sott int
AS
BEGIN
   Insert into hs_taptinct(id,soid,tentaptin,tinhtrang,driveid,ghichu,sott)
    Values(@id,@soid,@tentaptin,NULL,@driveid,NULL,@sott)
END

/*Lấy dữ liệu*/

CREATE PROCEDURE sp_LayDuLieu
AS
BEGIN
 DELETE FROM hs_tapting
  DELETE FROM hs_taptinct
END

select *
from hs_taptinct
where soid='d7edd494-6af0-4d39-a1a5-578c77e84710'

/* Store procedure Thêm mới Nội dung */
CREATE PROCEDURE sp_ThemMoiND
    @id uniqueidentifier,
    @ma_tieuchi nvarchar(50),
    @ten_tieuchi nvarchar(512),
	@nguoitao nvarchar(50),
	@ngaytao datetime,
	@noidung nvarchar(MAX)
AS
BEGIN
   Insert into hs_noidung(id,ma_tieuchi,ten_tieuchi,ghichu,nguoitao,ngaytao,nguoisua,ngaysua,noidung)
    Values(@id,@ma_tieuchi,@ten_tieuchi,NULL,@nguoitao,@ngaytao,NULL,NULL,@noidung)
END

/* Store procedure Cập nhật Nội dung */
CREATE PROCEDURE sp_CapNhatND
     @id uniqueidentifier,
    @ma_tieuchi nvarchar(50),
    @ten_tieuchi nvarchar(512),
	@nguoisua nvarchar(50),
	@ngaysua datetime,
	@noidung nvarchar(MAX)
AS
BEGIN
    UPDATE hs_noidung
    SET ma_tieuchi=ISNULL(@ma_tieuchi,ma_tieuchi), 
        ten_tieuchi=ISNULL(@ten_tieuchi,ten_tieuchi), 
        nguoisua=ISNULL(@nguoisua, nguoisua),
		ngaysua=ISNULL(@ngaysua, ngaysua),
		noidung=ISNULL(@noidung, noidung)
    WHERE id=@id
END