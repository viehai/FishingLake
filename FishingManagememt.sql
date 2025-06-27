-- Tạo Database nếu cần
CREATE DATABASE FishingManagement;
GO

USE FishingManagement;
GO

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

-- 2. PONDS: hồ câu
CREATE TABLE Ponds (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255),
    Description NVARCHAR(MAX),
    Capacity INT NOT NULL DEFAULT 20,      -- số chỗ tối đa mỗi slot
    OwnerId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
);

-- 3. FISHSPECIES: loài cá
CREATE TABLE FishSpecies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    AverageWeight FLOAT,
    ImageUrl NVARCHAR(255)
);

-- 4. PONDFISH: cá có trong từng hồ
CREATE TABLE PondFish (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL FOREIGN KEY REFERENCES Ponds(Id),
    FishId INT NOT NULL FOREIGN KEY REFERENCES FishSpecies(Id),
    Quantity INT DEFAULT 0
);

-- 5. BOOKINGS: lịch đặt câu + thanh toán
CREATE TABLE Bookings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL FOREIGN KEY REFERENCES Ponds(Id),
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    BookingDate DATE NOT NULL,               -- ngày câu
    StartTime TIME NOT NULL,                 -- giờ bắt đầu câu
    DurationMinutes INT NOT NULL DEFAULT 120,-- thời lượng
    Note NVARCHAR(MAX),
    Status NVARCHAR(20) DEFAULT 'Pending',   -- Pending, Confirmed, Done, Cancelled
    Price MONEY DEFAULT 0,                   -- giá tiền
    IsPaid BIT DEFAULT 0,                    -- đã thanh toán chưa
    PaymentTime DATETIME NULL,               -- thời gian thanh toán
    PaymentMethod NVARCHAR(50) DEFAULT 'Cash'-- mặc định là tiền mặt
);
