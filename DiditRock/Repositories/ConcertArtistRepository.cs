using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DiditRock.Models;
using DiditRock.Utils;



namespace DiditRock.Repositories
{
    public class ConcertArtistRepository : BaseRepository, IConcertArtistRepository
    {
        public ConcertArtistRepository(IConfiguration configuration) : base(configuration) { }

        public List<ConcertArtist> Get(int concertId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pt.*, t.Name FROM ConcertArtist pt JOIN Artist t ON pt.ArtistId = t.Id
                                        WHERE ConcertId = @concertId";

                    DbUtils.AddParameter(cmd, "@concertId", concertId);

                    List<ConcertArtist> concertArtists = new List<ConcertArtist>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            concertArtists.Add(new ConcertArtist
                            {
                                ConcertId = concertId,
                                ArtistId = DbUtils.GetInt(reader, "ArtistId"),
                                ArtistName = DbUtils.GetString(reader, "Name")
                            });
                        }
                    }
                    return concertArtists;
                }
            }
        }
        public void Add(ConcertArtist concertArtist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO ConcertArtist (ConcertId, ArtistId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@concertId, @artistId)";

                    DbUtils.AddParameter(cmd, "@concertId", concertArtist.ConcertId);
                    DbUtils.AddParameter(cmd, "@artistId", concertArtist.ArtistId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(ConcertArtist concertArtist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ConcertArtist
                                        WHERE ConcertId = @concertId AND ArtistId = @artistId";
                    DbUtils.AddParameter(cmd, "@concertId", concertArtist.ConcertId);
                    DbUtils.AddParameter(cmd, "@artistId", concertArtist.ArtistId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}