CREATE DATABASE FishingManagement;
GO

USE FishingManagement;
GO


CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255),           
    Role INT NOT NULL DEFAULT 2,   -- 1 = Chủ hồ, 2 = Khách
    TotalBookings INT DEFAULT 0,
    IsVIP BIT DEFAULT 0
);


INSERT INTO Users (Name, Phone, PasswordHash, Role)
VALUES (N'Chủ Hồ Long', '0965120204', '123', 1);

CREATE TABLE Pond (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255),
    Description NVARCHAR(MAX),
    Capacity INT NOT NULL DEFAULT 20,
    OwnerId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
	IsDeleted BIT NOT NULL DEFAULT 0
);

select * from Pond

INSERT INTO Pond (Name, Location, Description, Capacity, OwnerId)
VALUES 
    (N'Hồ Câu A', N'123 Đường Láng, Hà Nội', N'Hồ câu đẹp, nhiều cá chép', 20, 1),
    (N'Hồ Câu B', N'456 Bạch Đằng, Đà Nẵng', N'Hồ câu rộng, thích hợp câu đêm', 25, 1),
    (N'Hồ Câu C', N'789 Lê Lợi, Huế', N'Hồ câu yên tĩnh, nhiều cá lóc', 15, 1);

INSERT INTO Pond (Name, Location, Description, Capacity, OwnerId)
VALUES 
    (N'Hồ Câu Z', N'10 Trần Phú, TP.HCM', N'Hồ rộng, có cá tra và cá trắm', 30, 1),
    (N'Hồ Câu X', N'55 Nguyễn Huệ, Cần Thơ', N'Không gian thoáng đãng, phù hợp nhóm bạn', 18, 1),
    (N'Hồ Câu C', N'99 Phan Đình Phùng, Đà Lạt', N'Nước mát, nhiều cá hồi', 22, 1);


CREATE TABLE FishSpecies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    AverageWeight FLOAT
);



INSERT INTO FishSpecies (Name, Description, AverageWeight)
VALUES 
    (N'Cá chép', N'Loài cá phổ biến', 2.5),
    (N'Cá rô', N'Cá nhỏ, dễ câu', 0.5),
    (N'Cá trắm', N'Cá lớn, khỏe', 5.0),
    (N'Cá lóc', N'Cá nước ngọt, ngon', 1.0);

INSERT INTO FishSpecies (Name, Description, AverageWeight)
VALUES 
    (N'Cá tra', N'Cá da trơn, dễ nuôi và tăng trưởng nhanh', 4.0),
    (N'Cá mè', N'Cá ăn lọc, thường sống theo bầy', 3.5),
    (N'Cá chim trắng', N'Cá thân dẹp, thường nhảy mạnh khi mắc câu', 1.8),
    (N'Cá chạch', N'Cá nhỏ, thường ẩn dưới bùn', 0.3),
    (N'Cá vược', N'Cá săn mồi, phản ứng nhanh', 2.0),
    (N'Cá trê', N'Cá da trơn, sống dai, thường câu ban đêm', 1.5),
    (N'Cá trôi', N'Cá hiền, ăn tạp, dễ câu bằng mồi bột', 2.2),
    (N'Cá rô phi', N'Cá phổ biến, sinh sản mạnh, dễ nuôi', 1.2),
    (N'Cá ngạnh', N'Cá da trơn, có ngạnh sắc nhọn', 1.7),
    (N'Cá lăng', N'Cá to, chuyên sống nơi nước chảy', 6.0);


CREATE TABLE PondFish (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL FOREIGN KEY REFERENCES Pond(Id),
    FishId INT NOT NULL FOREIGN KEY REFERENCES FishSpecies(Id)
);


select * from PondFish

INSERT INTO PondFish (PondId, FishId)
VALUES 
    (1, 1),
    (1, 2),
    (2, 3),
    (2, 4),
    (3, 4);


CREATE TABLE Bookings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL FOREIGN KEY REFERENCES Pond(Id),
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    BookingDate DATE NOT NULL,               
    Note NVARCHAR(MAX),
	Price MONEY DEFAULT 0,
	PaymentTime DATETIME NULL,               
    PaymentMethod NVARCHAR(50) DEFAULT 'Cash'
);

select * from Bookings

select * from Users
select * from Pond

SELECT * FROM Bookings WHERE BookingDate = CAST(GETDATE() AS DATE)

SELECT Id, Name, OwnerId FROM Pond;
SELECT * FROM Pond WHERE OwnerId = 1