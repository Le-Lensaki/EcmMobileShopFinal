create table tb_MAUSAC(
IdMau int identity(1,1) primary key,
MaMau nvarchar(20),
TenMau nvarchar(20))

Insert into tb_MAUSAC(MaMau,TenMau)
values
('#ffd60a', N'Vàng'),
('#ef233c', N'Đỏ'),
('#003566', N'Xanh'),
('#ffb4a2', N'Magenta'),
('#000814', N'Đen'),
('#ffffff', N'Trắng'),
('#48cae4', N'Lam'),
('#e76f51', N'Cam'),
('#ddbea9', N'Nâu'),
('#ced4da', N'Xám'),
('#dee2ff', N'Tím'),
('#d9ed92', N'Xanh lá'),
('#52b788', N'Lục'),
('#b5ffe1', N'Mint'),
('#ff5d8f', N'Hồng')
