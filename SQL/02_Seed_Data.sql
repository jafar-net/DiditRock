USE [DiditRock];
GO

set identity_insert [UserType] on
insert into [UserType] ([Id], [Name]) VALUES (1, 'Admin'), (2, 'Author');
set identity_insert [UserType] off

set identity_insert [Artist] on
insert into [Artist] ([Id], [Name]) 
values (1, 'Tove Lo'), (2, 'Nas'), (3, 'Nashville Symphony'), (4, 'James Taylor'), (5, 'Jasckon Brown'),
	   (6, 'The Front Bottoms'), (7, 'Oso Oso'), (8, 'Rainbow Kitten Surprise'), (9, 'Bendigo Fletcher')
set identity_insert [Artist] off

set identity_insert [Venue] on
insert into [Venue] ([Id], [Name], [Location], [Capacity], [UpcomingShows], [VenueType])
values (1, 'Marathon Music Works', '1402 Clinton St, Nashville, TN 37203', 1700, 'Upcoming Shows', 'Warehouse Style/Standing Room Only'), (2, 'Ascend Ampitheater', '310 1st Ave S, Nashville, TN 37201', 6800, 'Upcoming Shows', 'Outdoor Ampitheater'), (3, 'Bridgestone Arena', '501 Broadway, Nashville, TN 37203
',20000, 'Upcoming Shows', 'Indoor Arena'), (4, 'Brooklyn Bowl', '925 3rd Ave N, Nashville, TN 37201', 2000, 'Upcoming Shows', 'Concert Hall/Standing Room Only');
set identity_insert [Venue] off

set identity_insert [Concert] on
insert into [Concert] ([Id], [Name], [Genre], [EncoreSongs], [Date], [VenueId])
values (1, 'Tove Lo: The Sunshine Kitty Tour', 'Alternative/Indie', 'Habits (Stay High), Sweettalk My Heart, and Bikini Porn', '2020-02-03', 1), (2, 'Lovenoise Presents: Nas with the Nashville Symphony', 'Hip Hop', 'n/a', '2021-09-12', 2), (3, 'James Taylor and his All-Star Band with Special Guest
Jackson Browne', 'Southern Rock', 'Take it Easy and Youve Got a Friend', '2021-08-16', 3), (4, 'The Front Bottoms 20201 Tour', 'Indie Rock', 'Flashlight, Leaf Pile, and Twelve Feet Deep', '2021-09-24', 4), (5, 'RKS: Crimbo Limbo', 'Indie Rock', 'When it Lands and Its Called: Freefall', '2021-12-26', 4)
set identity_insert [Concert] off

set identity_insert [User] on
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (1, 'Ben', 'Ben', 'Mosley', 'bmosley1132@gmail.com', '2021-01-26', 1, 'AUqMspbMRsXSZfLPY7PxWIPrUWu2');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (2, 'rdo1', 'Red', 'Do', 'rdo1@timesonline.co.ukx', '2020-04-20', 2, 'lXkotHqw0oNjdCD2waFEnq51xeT2');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (3, 'aotton2', 'Arnold', 'Otton', 'aotton2@ow.lyx', '2020-01-13', 1, 'wqhvgdjxjqkqecuridpvjtwpoacc');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (4, 'agrzeskowski3', 'Aharon', 'Grzeskowski', 'agrzeskowski3@fc2.comx', '2020-04-12', 1, 'exsjcqvnhydjofznqmtvecekcgno');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (5, 'ryakushkev4', 'Rosana', 'Yakushkev', 'ryakushkev4@weibo.comx', '2019-08-16', 1, 'djwoicosfnhexpmmsnukgcsbnqod');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (6, 'tfigiovanni5', 'Tobi', 'Figiovanni', 'tfigiovanni5@baidu.comx', '2019-10-17', 2, 'xiybslspeizewvkixqubnqjlwamz');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (7, 'gteanby6', 'Giuseppe', 'Teanby', 'gteanby6@craigslist.orgx', '2019-08-29', 1, 'lzxmysyzqrmcwjzxsyrkbczhspup');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (8, 'cvanderweedenburg7', 'Cristabel', 'Van Der Weedenburg', 'cvanderweedenburg7@wikimedia.orgx', '2019-11-02', 1, 'jqqyiksxkocmhphkylikwcehuvky');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (9, 'ecornfoot8', 'Emmaline', 'Cornfoot', 'ecornfoot8@cargocollective.comx', '2020-02-17', 2, 'smzswoscvmfpvugpmgvkflihdmka');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (10, 'jmaruska9', 'Jody', 'Maruska', 'jmaruska9@netscape.comx', '2020-02-09', 1, 'abcnibyohfhukxngaegjxxzionyt');
insert into [User] (Id, DisplayName, FirstName, LastName, Email, CreateDate, UserTypeId, FirebaseUserId) values (11, 'rsandwith0', 'Reina', 'Sandwith', 'rsandwith0@google.com.brx', '2020-04-23', 1, 'jpuhyzaicsokywncxveknzowfpdu');
set identity_insert [User] off

set identity_insert [Post] on
insert into Post (Id, Headline, Review, ImageUrl, CreateDateTime, SeatNumber, ConcertId, UserId) values (1, 'morph front-end markets', 'Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.', 'http://lorempixel.com/920/360/', '2021-01-04', 'A24', 3, 1);
insert into Post (Id, Headline, Review, ImageUrl, CreateDateTime, SeatNumber, ConcertId, UserId) values (2, 'orchestrate value-added communities', 'In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.
Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.', 'http://lorempixel.com/920/360/', '2021-01-16', 'A24', 4, 1);
insert into Post (Id, Headline, Review, ImageUrl, CreateDateTime, SeatNumber, ConcertId, UserId) values (3, 'engineer ubiquitous users', 'Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.
Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.', 'http://lorempixel.com/920/360/', '2021-01-29', 'A24', 2, 1);
insert into Post (Id, Headline, Review, ImageUrl, CreateDateTime, SeatNumber, ConcertId, UserId) values (4, 'deploy impactful architectures', 'Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.
Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.', 'http://lorempixel.com/920/360/', '2021-10-21', 'F14', 3, 1);
insert into Post (Id, Headline, Review, ImageUrl, CreateDateTime, SeatNumber, ConcertId, UserId) values (5, 'empower 24/7 systems', 'Vestibulum ac est lacinia nisi venenatis tristique. Fusce co.', 'http://lorempixel.com/920/360/', '2019-12-17', 'F14', 2, 1);
set identity_insert [Post] off