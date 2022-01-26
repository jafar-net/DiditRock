CREATE TABLE [Venue] (
  [Id] int,
  [Name] nvarchar(255),
  [Location] nvarchar(255),
  [Capacity] int,
  [UpcomingShows] nvarchar(255),
  [VenueType] nvarchar(255)
)
GO

CREATE TABLE [User] (
  [id] int,
  [FirebaseUserId] nvarchar(255),
  [DisplayName] nvarchar(255),
  [FirstName] nvarchar(255),
  [LastName] nvarchar(255),
  [Email] nvarchar(255)
)
GO

CREATE TABLE [Post] (
  [id] int,
  [UserId] int,
  [ConcertId] int,
  [ImageUrl] nvarchar(255),
  [SeatNumber] nvarchar(255),
  [StarRank] int,
  [Review] nvarchar(255)
)
GO

CREATE TABLE [Concert] (
  [id] int,
  [EncoreSongs] nvarchar(255),
  [Date] datetime,
  [VenueId] int,
  [StarRank] int,
  [Genre] nvarchar(255)
)
GO

CREATE TABLE [Parking] (
  [id] int,
  [Name] nvarchar(255),
  [Location] nvachar (255),
  [VenueId] int
)
GO

CREATE TABLE [Artist] (
  [id] int,
  [Name] nvarchar(255)
)
GO

CREATE TABLE [ConcertArtist] (
  [id] int,
  [ArtistId] int,
  [ConcertId] int
)
GO

CREATE TABLE [Comment] (
  [userID] int,
  [PostId] int,
  [Comment] nvarchar(255)
)
GO

CREATE TABLE [VenueParking] (
  [id] int,
  [VenueId] int,
  [ParkingId] int
)
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([id])
GO

ALTER TABLE [Concert] ADD FOREIGN KEY ([id]) REFERENCES [Post] ([ConcertId])
GO

ALTER TABLE [Parking] ADD FOREIGN KEY ([VenueId]) REFERENCES [Venue] ([Id])
GO

ALTER TABLE [ConcertArtist] ADD FOREIGN KEY ([ArtistId]) REFERENCES [Artist] ([id])
GO

ALTER TABLE [Concert] ADD FOREIGN KEY ([id]) REFERENCES [ConcertArtist] ([ConcertId])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([id]) REFERENCES [Comment] ([userID])
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([id]) REFERENCES [Comment] ([PostId])
GO

ALTER TABLE [Parking] ADD FOREIGN KEY ([id]) REFERENCES [VenueParking] ([ParkingId])
GO

ALTER TABLE [Venue] ADD FOREIGN KEY ([Id]) REFERENCES [VenueParking] ([VenueId])
GO
