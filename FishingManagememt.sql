-- Tạo Database nếu cần
CREATE DATABASE FishingManagement;
GO

USE FishingManagement;
GO

DROP DATABASE FishingManagement

-- 1. USERS: khách và chủ hồ
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255),           -- chỉ dùng cho chủ hồ
    Role INT NOT NULL DEFAULT 2,           -- 1 = Chủ hồ, 2 = Khách
    TotalBookings INT DEFAULT 0,
    IsVIP BIT DEFAULT 0
);

select * from Users

INSERT INTO Users (Name, Phone, PasswordHash, Role)
VALUES (N'Chủ Hồ Long', '0965120204', '123', 1);

-- 2. PONDS: hồ câu
CREATE TABLE Pond (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255),
    Description NVARCHAR(MAX),
    Capacity INT NOT NULL DEFAULT 20,      -- số chỗ tối đa mỗi slot
    OwnerId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
);

select * from Pond

-- Chèn lại dữ liệu vào Ponds
INSERT INTO Pond (Name, Location, Description, Capacity, OwnerId)
VALUES 
    (N'Hồ Câu A', N'123 Đường Láng, Hà Nội', N'Hồ câu đẹp, nhiều cá chép', 20, 1),
    (N'Hồ Câu B', N'456 Bạch Đằng, Đà Nẵng', N'Hồ câu rộng, thích hợp câu đêm', 25, 1),
    (N'Hồ Câu C', N'789 Lê Lợi, Huế', N'Hồ câu yên tĩnh, nhiều cá lóc', 15, 1);


-- 3. FISHSPECIES: loài cá
CREATE TABLE FishSpecies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    AverageWeight FLOAT,
    ImageUrl NVARCHAR(255)
);
select * from FishSpecies
-- Chèn dữ liệu mẫu vào FishSpecies (nếu chưa có)
INSERT INTO FishSpecies (Name, Description, AverageWeight, ImageUrl)
VALUES 
    (N'Cá chép', N'Loài cá phổ biến', 2.5, N'https://example.com/cachép.jpg'),
    (N'Cá rô', N'Cá nhỏ, dễ câu', 0.5, N'https://example.com/caro.jpg'),
    (N'Cá trắm', N'Cá lớn, khỏe', 5.0, N'https://example.com/catrăm.jpg'),
    (N'Cá lóc', N'Cá nước ngọt, ngon', 1.0, N'https://example.com/calóc.jpg');
-- 4. PONDFISH: cá có trong từng hồ
CREATE TABLE PondFish (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL FOREIGN KEY REFERENCES Pond(Id),
    FishId INT NOT NULL FOREIGN KEY REFERENCES FishSpecies(Id),
    Quantity INT DEFAULT 0
);
INSERT INTO PondFish (PondId, FishId, Quantity)
VALUES 
    (1, 1, 50),
    (1, 2, 30),
    (2, 3, 40),
    (2, 4, 20),
    (3, 4, 25);



-- Nếu bảng Booking đã tạo rồi ➜ xóa đi tạo lại hoặc ALTER TABLE

DROP TABLE IF EXISTS Bookings;

CREATE TABLE Bookings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL FOREIGN KEY REFERENCES Pond(Id),
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    BookingDate DATE NOT NULL,               -- ngày câu
    Note NVARCHAR(MAX),
    Status NVARCHAR(20) DEFAULT 'Pending',   -- Pending, Confirmed, Done, Cancelled, Expired
    Price MONEY DEFAULT 0,                   -- giá tiền
    IsPaid BIT DEFAULT 0,                    -- đã thanh toán chưa
    PaymentTime DATETIME NULL,               -- thời gian thanh toán
    PaymentMethod NVARCHAR(50) DEFAULT 'Cash'-- mặc định là tiền mặt
);

select * from Bookings