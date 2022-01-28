using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiditRock.Models;
using DiditRock.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;



namespace DiditRock.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        private readonly IUserProfileRepository _userprofileRepository;


        public PostRepository(IConfiguration configuration, IUserProfileRepository userProfileRepository) : base(configuration)
        {
            _userprofileRepository = userProfileRepository;
        }

        public List<Post> GetAll(int currentUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, p.Headline, p.Review, p.ImageUrl, p.CreateDateTime, p.ConcertId, p.UserProfileId,
	                        c.[Name],
	                        up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime, up.ImageUrl

                        FROM Post p
                        JOIN Concert c ON p.ConcertId = c.Id
                        JOIN UserProfile up ON p.UserProfileId = up.Id
                        WHERE p.IsApproved = 1
                        AND  < SYSDATETIME()
                        ORDER BY  DESC;
                    ";

                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>();
                    while (reader.Read())
                    {
                        posts.Add(new Post()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Headline = DbUtils.GetString(reader, "Headline"),
                            Review = DbUtils.GetString(reader, "Review"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            ConcertId = DbUtils.GetInt(reader, "ConcertId"),
                            Concert = new Concert()
                            {
                                Id = DbUtils.GetInt(reader, "ConcertId"),
                                Name = DbUtils.GetString(reader, "Name")
                            },
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            },
                            IsByCurrentUser = currentUserId == DbUtils.GetInt(reader, "UserProfileId") ? true : false
                        });

                    }
                    reader.Close();

                    return posts;
                }
            }
        }

        public Post GetById(int id, int currentUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT p.Id, p.Headline, p.Review, p.ImageUrl, p.CreateDateTime, 
                            , p.IsApproved, p.ConcertId, p.UserProfileId,
                            c.[Name],
	                        up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime, up.ImageUrl

                            FROM Post p
                            JOIN Concert c ON p.ConcertId = c.Id
                            JOIN UserProfile up ON p.UserProfileId = up.Id
                            WHERE p.IsApproved = 1
                            AND  < SYSDATETIME()                          
                            AND p.Id = @id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    Post post = null;
                    if (reader.Read())
                    {
                        post = new Post()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Headline = DbUtils.GetString(reader, "Headline"),
                            Review = DbUtils.GetString(reader, "Review"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            ConcertId = DbUtils.GetInt(reader, "ConcertId"),
                            Concert = new Concert()
                            {
                                Id = DbUtils.GetInt(reader, "ConcertId"),
                                Name = DbUtils.GetString(reader, "Name")
                            },
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            },
                            IsByCurrentUser = currentUserId == DbUtils.GetInt(reader, "UserProfileId") ? true : false

                        };
                    }
                    reader.Close();

                    return post;
                }
            }
        }

        public void Add(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Post (Headline, Review, ImageUrl, 
                                        CreateDateTime, IsApproved, ConcertId, UserProfileId)
                                        OUTPUT Inserted.Id
                                        VALUES (@headline, @content, @imageLocation, SYSDateTime(), @publishDateTime,
                                        @isApproved, @categoryId, @userProfileId)";

                    DbUtils.AddParameter(cmd, "@headline", post.Headline);
                    DbUtils.AddParameter(cmd, "@content", post.Review);
                    DbUtils.AddParameter(cmd, "@imageLocation", post.ImageUrl);
                    DbUtils.AddParameter(cmd, "@isApproved", 1);
                    DbUtils.AddParameter(cmd, "@categoryId", post.ConcertId);
                    DbUtils.AddParameter(cmd, "@userProfileId", post.UserProfileId);

                    post.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Post WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Update(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Post SET 
                                        Headline = @headline,
                                        Review = @content,
                                        ImageUrl = @imageLocation,
                                        ConcertId = @categoryId
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@headline", post.Headline);
                    DbUtils.AddParameter(cmd, "@content", post.Review);
                    DbUtils.AddParameter(cmd, "@imageLocation", post.ImageUrl);
                    DbUtils.AddParameter(cmd, "@categoryId", post.ConcertId);
                    DbUtils.AddParameter(cmd, "@userProfileId", post.UserProfileId);
                    DbUtils.AddParameter(cmd, "@id", post.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Post> GetUserPostsById(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT p.Id, p.Headline, p.Review, p.ImageUrl, p.CreateDateTime, , p.IsApproved, p.ConcertId, p.UserProfileId,
                    c.[Name],
                    up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime, up.ImageUrl

                    FROM Post p
                    LEFT JOIN Concert c ON p.ConcertId = c.Id
                    LEFT JOIN UserProfile up ON p.UserProfileId = up.Id
                    WHERE p.UserProfileId = userProfileId 
                    AND 
                    p.IsApproved = 1 
                    AND 
                     < SYSDATETIME()
                    ORDER BY  DESC; 
                    ";

                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>(userProfileId);


                    while (reader.Read())
                    {
                        posts.Add(NewPostFromReader(reader));
                    }

                    reader.Close();

                    return posts;
                }
            }
        }

        private Post NewPostFromReader(SqlDataReader reader)
        {
            return new Post()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Headline = reader.GetString(reader.GetOrdinal("Headline")),
                Review = reader.GetString(reader.GetOrdinal("Review")),
                ImageUrl = DbUtils.GetString(reader, "HeaderImage"),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                ConcertId = reader.GetInt32(reader.GetOrdinal("ConcertId")),
                Concert = new Concert()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ConcertId")),
                    Name = reader.GetString(reader.GetOrdinal("ConcertName"))
                },
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                UserProfile = new UserProfile()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                    UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                    UserType = new UserType()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                        Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                    }
                }
            };
        }

        public List<Post> GetAllPostsForUser(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT p.Id, p.Headline, p.Review, p.ImageUrl AS HeaderImage, p.CreateDateTime, , p.IsApproved, p.ConcertId, p.UserProfileId,
                    c.[Name] AS ConcertName,
                    up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime, up.ImageUrl AS AvatarImage, up.UserTypeId,
                    ut.[Name] AS UserTypeName
                    FROM Post p
                    LEFT JOIN Concert c ON p.ConcertId = c.Id
                    LEFT JOIN UserProfile up ON p.UserProfileId = up.Id
                    LEFT JOIN UserType ut ON up.UserTypeId = ut.Id
                    WHERE p.UserProfileId = @userProfileId 
                    ORDER BY  DESC;                     
                    ";

                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>(userProfileId);


                    while (reader.Read())
                    {
                        posts.Add(NewPostFromReader(reader));
                    }

                    reader.Close();

                    return posts;
                }
            }
        }

    }
}