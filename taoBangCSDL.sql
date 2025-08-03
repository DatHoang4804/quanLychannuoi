create table taikhoan(
      id varchar(100) primary key,
	  email varchar(100) unique not null,
	  matkhau varchar(100) not null,
	  tendaydu nvarchar(150) not null,
	  role nvarchar(50) not null
); 

create table huyen(
      id_Huyen varchar(100) primary key,
	  tenHuyen nvarchar(100) unique not null,
);

create table xa(
      id_Xa varchar(100) primary key, 
	  id_Huyen varchar(100) not null,
	  tenXa nvarchar(100) unique not null,
	  foreign key( id_Huyen) references Huyen(id_Huyen)
);

create table SXsphamXlycThai(
      id_sXuat varchar(100) primary key,
	  tensXuat nvarchar(150) not null,
	  id_Huyen varchar(100) not null,
	  id_Xa varchar(100) not null,
	  diachisXuat varchar(255) not null,
	  sdtsXuat varchar(20) not null,
	  emailsXuat varchar(100) not null,
	  foreign key (id_Huyen) references huyen(id_Huyen),
	  foreign key (id_Xa) references xa(id_Xa)
);

create table KNsphamXlycThai(
     id_kNghiem varchar(100) primary key,
	 tenkNghiem nvarchar(100) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 diachikNghiem nvarchar(255) not null,
	 sdtkNghiem varchar(20) not null,
	 emailkNghiem varchar(100) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
	 foreign key (id_Xa) references xa(id_Xa)
);

create table CSChanNuoi(
     id_cNuoi varchar(100) primary key,
	 tencNuoi nvarchar(100) not null,
	 id_dkcNuoi varchar(100) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 diachicNuoi nvarchar(255) not null,
	 sdtcNuoi varchar(20) not null,
	 emailcNuoi varchar(100) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
	 foreign key (id_Xa) references xa(id_Xa),
	 foreign key (id_dkcNuoi) references dkcNuoi(id_dkcNuoi)
);

create table ToChuc(
     id_cNuoi varchar (100),
     id_tChuc varchar(100) primary key,
	 tentChuc nvarchar(100) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 nguoidaidien nvarchar(100) not null,
	 email varchar(100),
	 sdttChuc varchar(100) not null,
	 diachiNguoidaidien nvarchar(250) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
	 foreign key (id_Xa) references xa(id_Xa),
	 foreign key (id_cNuoi) references CSChanNuoi(id_cNuoi)
);

create table CaNhan(
     id_cNuoi varchar (100),
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
     id_cNhan varchar(100) primary key,
	 diachicNhan nvarchar(250) not null,
	 tencNhan nvarchar(100) not null,
	 sdtcNhan varchar(20) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
	 foreign key (id_Xa) references xa(id_Xa),
	 foreign key (id_cNuoi) references CSChanNuoi(id_cNuoi)
);

create table dkcNuoi(
     id_dkcNuoi varchar(100) primary key,
	 tendkCnuoi nvarchar(100) not null,
	 ngaycapdkCnuoi date not null,
	 ngayhethandkCnuoi date
);

create table tochucChNhan(
     id_TCChNhan varchar(100) primary key,
	 tenTCChNhan nvarchar(100) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 diachiTCChNhan nvarchar(255) not null,
	 sdtTCChNhan varchar(20) not null,
	 emailTCChNhan varchar(255) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
	 foreign key (id_Xa) references xa(id_Xa)
);

CREATE TABLE giayChNhan (
    id_ChNhan varchar(100) PRIMARY KEY,
    id_TCChNhan varchar(100) NOT NULL,
    tenChNhan nvarchar(100) NOT NULL,
    ngayChNhan date NOT NULL,
    ngayHetHan date,
    FOREIGN KEY (id_TCChNhan) REFERENCES tochucChNhan(id_TCChNhan)
);

create table hoCnuoiNhole(
     id_hoCnuoi varchar(100) primary key,
	 tenChuHo nvarchar(100) not null,
	 sdtChuHo nvarchar(25) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
     thongke nvarchar(1000),
	 foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa)
);

create table csCheBien(
     id_cBien varchar(100) primary key,
     tencBien nvarchar(100) not null,
     id_Huyen varchar(100) not null, 
     id_Xa varchar(100) not null,
     diachicBien nvarchar(255) not null,
     sdtcBien varchar(20) not null,
     emailcBien varchar(100) not null,
     foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa)
);

create table dichBenh(
    id_dichBenh varchar(100) primary key,
	tendichBenh nvarchar(100) not null,
	mota nvarchar(1000) not null,
	tinhtrang bit not null,
); 

create table VungdichBenh(
     id_Vungdich varchar(100) primary key,
	 tenVungdich nvarchar(255) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 id_dichBenh varchar(100) not null,
	 ngaykhoiphat date not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa),
	 foreign key (id_dichBenh) references dichBenh(id_dichBenh)
);

create table cCucTY(
     id_cCucTY varchar(100) primary key,
	 tencCucTY nvarchar(255) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 diachicCucTY nvarchar(255) not null,
	 sdtcCucTY varchar(20) not null,
	 emailcCucTY varchar(255) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa)
);

create table dLythuoc(
     id_dLy varchar(100) primary key,
	 tendLy nvarchar(255) not null,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 diachidLY nvarchar(255) not null,
	 sdtdLy varchar(20) not null,
	 emaildLy varchar(255) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa),
);

create table khuTGiu(
     id_tGiu varchar(100) primary key,
	 id_Huyen varchar(100) not null,
	 id_Xa varchar(100) not null,
	 diachitGiu nvarchar(255) not null,
	 stttGiu varchar(20) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa),
);

create table CsGietMo(
     id_GietMo varchar(100) primary key,
	 tenGietMo nvarchar(255) not null,
	 id_Huyen varchar(100) not null,
     id_Xa varchar(100) not null,
	 diachiGietMo nvarchar(255) not null,
	 sdtGietMo varchar(20) not null,
	 foreign key (id_Huyen) references huyen(id_Huyen),
     foreign key (id_Xa) references xa(id_Xa),
);

CREATE TABLE LogDangNhap (
    id int IDENTITY(1,1) PRIMARY KEY, 
    thoiGianDangNhap DATETIME NOT NULL,
    thoiGianDangXuat DATETIME NULL,
    id_TaiKhoan varchar(100) NOT NULL,
    FOREIGN KEY (id_TaiKhoan) REFERENCES TaiKhoan(id)
);


