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
                                Name = DbUtils.GetString(reader, "Name"),
                                Location = DbUtils.GetString(reader, "Location"),
                                UpcomingShows = DbUtils.GetString(reader, "UpcomingShows"),
                                VenueType = DbUtils.GetString(reader, "VenueType"),
                                Capacity = DbUtils.GetInt(reader, "Capacity"),
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
                            venue = new Venue
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Location = DbUtils.GetString(reader, "Location"),
                                UpcomingShows = DbUtils.GetString(reader, "UpcomingShows"),
                                VenueType = DbUtils.GetString(reader, "VenueType"),
                                Capacity = DbUtils.GetInt(reader, "Capacity"),
                            };
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
                    cmd.CommandText = @"INSERT INTO Venue (Name, Location, UpcomingShows, VenueType, Capacity)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name, @Location, @UpcomingShows, @VenueType, @Capacity)";

                    DbUtils.AddParameter(cmd, "@Name", venue.Name);
                    DbUtils.AddParameter(cmd, "@Location", venue.Location);
                    DbUtils.AddParameter(cmd, "@UpcomingShows", venue.UpcomingShows);
                    DbUtils.AddParameter(cmd, "@VenueType", venue.VenueType);
                    DbUtils.AddParameter(cmd, "@Capacity", venue.Capacity);

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
                    cmd.CommandText = @"UPDATE Venue SET 
                                        Name = @name,
                                        Location = @location,
                                        UpcomingShows = @upcomingShows,
                                        Capacity = @capacity,
                                        VenueType = @venueType
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@name", venue.Name);
                    DbUtils.AddParameter(cmd, "@location", venue.Location);
                    DbUtils.AddParameter(cmd, "@upcomingShows", venue.UpcomingShows);
                    DbUtils.AddParameter(cmd, "@capacity", venue.Capacity);
                    DbUtils.AddParameter(cmd, "@venueType", venue.VenueType);
                    DbUtils.AddParameter(cmd, "@id", venue.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int Id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM VENUE
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "Id", Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}