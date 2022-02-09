using DiditRock.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiditRock.Repositories
{
    public class ConcertArtistRepository : BaseRepository, IConcertArtistRepository
    {

        public ConcertArtistRepository(IConfiguration config) : base(config) { }


        public ConcertArtist GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT ConcertId, ArtistId
                         FROM ConcertArtist
                        WHERE Id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    ConcertArtist concertArtist = new ConcertArtist();

                    if (reader.Read())
                    {
                        concertArtist.Id = id;
                        concertArtist.ConcertId = reader.GetInt32(reader.GetOrdinal("ConcertId"));
                        concertArtist.ArtistId = reader.GetInt32(reader.GetOrdinal("ArtistId"));
                    }

                    reader.Close();

                    return concertArtist;
                }
            }
        }

        public void Add(ConcertArtist concertArtist)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO ConcertArtist (ConcertId, ArtistId) 
                                        OUTPUT INSERTED.Id
                                        VALUES (@concertId, @concertId)";

                    cmd.Parameters.AddWithValue("@concertId", concertArtist.ConcertId);
                    cmd.Parameters.AddWithValue("@artistId", concertArtist.ArtistId);

                    int id = (int)cmd.ExecuteScalar();

                    concertArtist.Id = id;
                }
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ConcertArtist WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void clearConcertArtistsForConcert(int concertId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ConcertArtist WHERE ConcertId = @concertId";
                    cmd.Parameters.AddWithValue("@concertId", concertId);

                    cmd.ExecuteNonQuery();
                }
            }

        }


    }
}