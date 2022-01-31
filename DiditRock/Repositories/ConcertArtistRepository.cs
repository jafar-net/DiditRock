using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiditRock.Models;
using DiditRock.Utils;

namespace DiditRock.Repositories
{
    public class ConcertArtistRepository : BaseRepository, IConcertArtistRepository
    {
        public ConcertArtistRepository(IConfiguration config) : base(config) { }

        public List<ConcertArtist> GetAllConcertArtistsForConcert(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT  ca.Id, ca.ConcertId, ca.ArtistId, t.Name AS ArtistName
                    FROM
	                ConcertArtist ca
	                LEFT JOIN Artist t ON ca.ArtistId = t.Id
                    WHERE
	                ca.ConcertId = @Id
                    ";

                    DbUtils.AddParameter(cmd, "@ConcertId", id);

                    var reader = cmd.ExecuteReader();

                    var concertArtists = new List<ConcertArtist>();
                    while (reader.Read())
                    {
                        concertArtists.Add(new ConcertArtist()
                        {
                            Id = DbUtils.GetInt(reader, "ConcertArtistId"),

                            ConcertId = id,
                            Concert = new Concert()
                            {
                                //Id = DbUtils.GetInt(reader, "ConcertId"),
                                //Title = DbUtils.GetString(reader, "ConcertTitle"),
                                //Content = DbUtils.GetString(reader, "Content"),
                                //ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                                //PublishDateTime = DbUtils.GetDateTime(reader, "PublishDateTime").ToString("MM/dd/yyyy"),
                                //CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            },

                            ArtistId = DbUtils.GetInt(reader, "ArtistId"),
                            Artist = new Artist()
                            {
                                Id = DbUtils.GetInt(reader, "ArtistId"),
                                Name = DbUtils.GetString(reader, "ArtistName")
                            }
                        });
                    }

                    reader.Close();
                    return concertArtists;
                }

            }
        }


        public ConcertArtist GetConcertArtistById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT Id
                            FROM ConcertArtist
                            WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ConcertArtist concertArtist = new ConcertArtist()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id"))
                        };
                        reader.Close();
                        return concertArtist;
                    }
                    reader.Close();
                    return null;
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
                    cmd.CommandText = @"
                        INSERT INTO ConcertArtist(ConcertId, ArtistId)
                        OUTPUT INSERTED.ID
                        VALUES (@concertId, @artistId)";

                    cmd.Parameters.AddWithValue("@concertId", concertArtist.ConcertId);
                    cmd.Parameters.AddWithValue("@artistId", concertArtist.ArtistId);

                    int id = (int)cmd.ExecuteScalar();

                    concertArtist.Id = id;
                }
            }
        }

        public void Delete(int concertArtistId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM ConcertArtist
                        WHERE Id = @concertArtistId";

                    cmd.Parameters.AddWithValue("@concertArtistId", concertArtistId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}