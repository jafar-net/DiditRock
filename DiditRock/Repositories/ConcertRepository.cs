using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DiditRock.Models;
using DiditRock.Utils;

namespace DiditRock.Repositories
{
    public class ConcertRepository : BaseRepository, IConcertRepository
    {
        public ConcertRepository(IConfiguration configuration) : base(configuration) { }
        public List<Concert> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.Name, c.EncoreSongs, c.VenueId, c.Genre, c.Date, v.Name AS VenueName
                                        FROM CONCERT c
                                        JOIN VENUE v ON c.VenueId = v.Id
                                        ORDER BY c.Date ASC";
                    var concerts = new List<Concert>();

                    using (var reader = cmd.ExecuteReader())

                        while (reader.Read())
                        {
                            var concert = new Concert
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                EncoreSongs = DbUtils.GetString(reader, "EncoreSongs"),
                                Genre = DbUtils.GetString(reader, "Genre"),
                                Date = DbUtils.GetDateTime(reader, "Date"),
                                VenueId = DbUtils.GetInt(reader, "VenueId"),
                                Venue = new Venue()
                                {
                                    Id = DbUtils.GetInt(reader, "VenueId"),
                                    Name = DbUtils.GetString(reader, "VenueName")
                                }
                            };
                            concerts.Add(concert);
                        }
                    return concerts;
                }
            }
        }
        public Concert GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.Name, c.EncoreSongs, c.VenueId, c.Genre, c.Date, v.Name AS VenueName, ca.Id AS CAId, ca.ArtistId, ca.ConcertId, t.Id AS TId, t.Name AS ArtistName
                      FROM CONCERT c
                      JOIN VENUE v ON c.VenueId = v.Id
                      JOIN ConcertArtist ca ON c.Id=ca.ConcertId
                      JOIN Artist t ON t.Id=ca.ArtistId
                      WHERE c.Id=@Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    Concert concert = null;
                    while (reader.Read())
                    {
                        if (concert == null)
                        {
                            concert = new Concert()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                EncoreSongs = DbUtils.GetString(reader, "EncoreSongs"),
                                Genre = DbUtils.GetString(reader, "Genre"),
                                Date = DbUtils.GetDateTime(reader, "Date"),
                                VenueId = DbUtils.GetInt(reader, "VenueId"),
                                Venue = new Venue()
                                {
                                    Id = DbUtils.GetInt(reader, "VenueId"),
                                    Name = DbUtils.GetString(reader, "VenueName")
                                },
                                Artists = new List<Artist>()

                            };
                            
                            };
                                concert.Artists.Add(new Artist()
                                {
                                    Id = DbUtils.GetInt(reader, "ArtistId"),
                                    Name = DbUtils.GetString(reader, "ArtistName")
                                });

                    }
                    reader.Close();

                    return concert;
                }
            }
        }
        public void Add(Concert concert)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Concert (Name, EncoreSongs, Genre, Date, VenueId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name, @EncoreSongs, @Genre, @Date, @VenueId)";

                    DbUtils.AddParameter(cmd, "@Name", concert.Name);
                    DbUtils.AddParameter(cmd, "@EncoreSongs", concert.EncoreSongs);
                    DbUtils.AddParameter(cmd, "@Genre", concert.Genre);
                    DbUtils.AddParameter(cmd, "@Date", concert.Date);
                    DbUtils.AddParameter(cmd, "@VenueId", concert.VenueId);

                    concert.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Concert concert)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE CONCERT 
                                        SET Name = @name, EncoreSongs = @EncoreSongs, Genre = @Genre, Date = @Date, VenueId = @VenueId 
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", concert.Id);
                    DbUtils.AddParameter(cmd, "@Name", concert.Name);
                    DbUtils.AddParameter(cmd, "@EncoreSongs", concert.EncoreSongs);
                    DbUtils.AddParameter(cmd, "@Genre", concert.Genre);
                    DbUtils.AddParameter(cmd, "@Date", concert.Date);
                    DbUtils.AddParameter(cmd, "@VenueId", concert.VenueId);

                    cmd.ExecuteNonQuery();
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
                    cmd.CommandText = @"DELETE FROM CONCERT
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}