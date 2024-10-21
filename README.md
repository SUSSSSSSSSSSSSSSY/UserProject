# UserProject

Запросы:

CREATE DATABASE UserProject

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE UserSettings (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT UNIQUE,
    Theme NVARCHAR(20),
    Notifications BIT,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

Результат:

Users and their settings inserted.
Name: User 2, Theme: Dark, Notifications: True
Deleted 0 user(s).
