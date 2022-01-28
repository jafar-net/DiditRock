USE [master]

IF db_id('DiditRock') IS NULl
  CREATE DATABASE [DiditRock]
GO

USE [DiditRock]
GO


DROP TABLE IF EXISTS [Venue];
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [Post];
DROP TABLE IF EXISTS [Concert];
DROP TABLE IF EXISTS [Comment];
DROP TABLE IF EXISTS [Parking];
DROP TABLE IF EXISTS [Artist];
DROP TABLE IF EXISTS [ConcertArtist];
DROP TABLE IF EXISTS [VenueParking];
DROP TABLE IF EXISTS [UserType];
GO

CREATE TABLE [Venue] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [Location] nvarchar(255),
  [Capacity] int,
  [UpcomingShows] nvarchar(255),
  [VenueType] nvarchar(255)
)
GO

CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] nvarchar(255),
  [DisplayName] nvarchar(255),
  [FirstName] nvarchar(255),
  [LastName] nvarchar(255),
  [Email] nvarchar(255),
  [UserTypeId] int,
  [CreateDate] datetime
)
GO

CREATE TABLE [Post] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int,
  [ConcertId] int,
  [Headline] nvarchar(255),
  [ImageUrl] nvarchar(255),
  [SeatNumber] nvarchar(255),
  [StarRank] int,
  [Review] nvarchar(1000)
)
GO

CREATE TABLE [Concert] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [EncoreSongs] nvarchar(255),
  [Date] datetime,
  [VenueId] int,
  [StarRank] int,
  [Genre] nvarchar(255)
)
GO

CREATE TABLE [Parking] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [Location] nvarchar(255)
)
GO

CREATE TABLE [Artist] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255)
)
GO

CREATE TABLE [ConcertArtist] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ArtistId] int,
  [ConcertId] int
)
GO

CREATE TABLE [Comment] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [userID] int,
  [PostId] int,
  [Comment] nvarchar(1000)
)
GO

CREATE TABLE [VenueParking] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [VenueId] int,
  [ParkingId] int
)
GO

CREATE TABLE [UserType] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255)
)
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([ConcertId]) REFERENCES [Concert] ([Id])
GO

ALTER TABLE [ConcertArtist] ADD FOREIGN KEY ([ArtistId]) REFERENCES [Artist] ([Id]) 
GO

ALTER TABLE [ConcertArtist] ADD FOREIGN KEY ([ConcertId]) REFERENCES [Concert] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([userID]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([PostId]) REFERENCES [Post] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [VenueParking] ADD FOREIGN KEY ([ParkingId]) REFERENCES [Parking] ([Id])
GO

ALTER TABLE [Concert] ADD FOREIGN KEY ([VenueId]) REFERENCES [Venue] ([Id])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id])
GO

ALTER TABLE [VenueParking] ADD FOREIGN KEY ([VenueId]) REFERENCES [Venue] ([Id])
GO
