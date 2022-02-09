using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DiditRock.Models;
using DiditRock.Utils;

namespace DiditRock.Repositories
{
    public class ArtistRepository : BaseRepository, IArtistRepository
    {
        public ArtistRepository(IConfiguration configuration) : base(configuration) { }
        public List<Artist> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Artist
                                       ORDER BY Name";

                    var artists = new List<Artist>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var artist = new Artist
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            artists.Add(artist);
                        }

                    }
                    return artists;
                }
            }
        }
        public Artist GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Artist
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    Artist artist = null;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            artist = new Artist { Id = id, Name = DbUtils.GetString(reader, "Name") };
                        }
                    }
                    return artist;
                }
            }
        }
        public void Add(Artist artist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Artist (Name)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name)";

                    DbUtils.AddParameter(cmd, "@Name", artist.Name);

                    artist.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Artist artist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE ARTIST SET Name = @name WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@name", artist.Name);
                    DbUtils.AddParameter(cmd, "@id", artist.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Artist> GetArtistsByConcertId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT ca.Id, ca.ConcertId, ca.ArtistId, t.Name 
                         FROM ConcertArtist ca
                              LEFT JOIN Artist t ON t.Id = ca.ArtistId
                              LEFT JOIN Concert c ON c.id= ca.ConcertId
                        WHERE c.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    List<Artist> concertArtists = new List<Artist>();

                    while (reader.Read())
                    {
                        Artist concertArtist = new Artist()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        concertArtists.Add(concertArtist);
                    }

                    reader.Close();

                    return concertArtists;
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
                    cmd.CommandText = @"DELETE FROM ARTIST
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}