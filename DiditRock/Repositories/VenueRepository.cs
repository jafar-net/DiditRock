using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DiditRock.Models;
using DiditRock.Utils;

namespace DiditRock.Repositories
{
    public class VenueRepository : BaseRepository, IVenueRepository
    {
        public VenueRepository(IConfiguration configuration) : base(configuration) { }
        public List<Venue> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Venue
                                       ORDER BY Name";

                    var venues = new List<Venue>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var venue = new Venue
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            venues.Add(venue);
                        }

                    }
                    return venues;
                }
            }
        }
        public Venue GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Venue
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    Venue venue = null;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venue = new Venue { Id = id, Name = DbUtils.GetString(reader, "Name") };
                        }
                    }
                    return venue;
                }
            }
        }
        public void Add(Venue venue)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Venue (Name)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name)";

                    DbUtils.AddParameter(cmd, "@Name", venue.Name);

                    venue.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Venue venue)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE TAG SET Name = @name WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@name", venue.Name);
                    DbUtils.AddParameter(cmd, "@id", venue.Id);

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
                    cmd.CommandText = @"DELETE FROM TAG
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}