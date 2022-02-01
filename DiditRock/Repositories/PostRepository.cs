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

        public List<Post> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, p.Headline, p.Review, p.ImageUrl, p.CreateDateTime, p.ConcertId, p.UserId,
                        c.Name, up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime
                        FROM Post p
                        JOIN Concert c ON p.ConcertId = c.Id
                        JOIN UserProfile up ON p.UserId = up.Id
                        ORDER BY p.CreateDateTime ASC;
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
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
            
                            }
                        });

                    }
                    reader.Close();

                    return posts;
                }
            }
        }

        public Post GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT p.Id, p.Headline, p.Review, p.ImageUrl, p.CreateDateTime, p.ConcertId, p.UserId,
                        c.Name, up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime
                        FROM Post p
                        JOIN Concert c ON p.ConcertId = c.Id
                        JOIN UserProfile up ON p.UserId = up.Id
                        ORDER BY p.CreateDateTime ASC";

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
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                
                            },

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
                                        CreateDateTime, UserId, ConcertId)
                                        OUTPUT Inserted.Id
                                        VALUES (@headline, @review, @imageurl,                             @createDateTime, @userId, @ConcertId)";

                    DbUtils.AddParameter(cmd, "@headline", post.Headline);
                    DbUtils.AddParameter(cmd, "@review", post.Review);
                    DbUtils.AddParameter(cmd, "@imageurl", post.ImageUrl);
                    DbUtils.AddParameter(cmd, "@createDateTime", post.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@userId", post.UserId);
                    DbUtils.AddParameter(cmd, "@ConcertId", post.ConcertId);

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
                                        Review = @review,
                                        ImageUrl = @imageurl,
                                        ConcertId = @ConcertId
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@headline", post.Headline);
                    DbUtils.AddParameter(cmd, "@review", post.Review);
                    DbUtils.AddParameter(cmd, "@imageurl", post.ImageUrl);
                    DbUtils.AddParameter(cmd, "@concertId", post.ConcertId);
                    DbUtils.AddParameter(cmd, "@userId", post.UserId);
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
                    SELECT p.Id, p.Headline, p.Review, p.ImageUrl, p.CreateDateTime, p.PublishDateTime, p.IsApproved, p.ConcertId, p.UserProfileId,
                    c.[Name],
                    up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime, up.ImageUrl

                    FROM Post p
                    LEFT JOIN Concert c ON p.ConcertId = c.Id
                    LEFT JOIN UserProfile up ON p.UserProfileId = up.Id
                    WHERE p.UserProfileId = userProfileId 
                    AND 
                    p.IsApproved = 1 
                    AND 
                    p.PublishDateTime < SYSDATETIME()
                    ORDER BY p.PublishDateTime DESC; 
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
                UserId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
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
                    SELECT p.Id, p.Headline, p.Review, p.ImageUrl AS HeaderImage, p.CreateDateTime, p.PublishDateTime, p.IsApproved, p.ConcertId, p.UserProfileId,
                    c.[Name] AS ConcertName,
                    up.DisplayName, up.FirstName, up.LastName, up.Email, up.CreateDateTime, up.ImageUrl AS AvatarImage, up.UserTypeId,
                    ut.[Name] AS UserTypeName
                    FROM Post p
                    LEFT JOIN Concert c ON p.ConcertId = c.Id
                    LEFT JOIN UserProfile up ON p.UserProfileId = up.Id
                    LEFT JOIN UserType ut ON up.UserTypeId = ut.Id
                    WHERE p.UserProfileId = @userProfileId 
                    ORDER BY p.PublishDateTime DESC;                     
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