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
                    cmd.CommandText = @"SELECT * FROM Concert
                                       ORDER BY Name";

                    var concerts = new List<Concert>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var concert = new Concert
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            concerts.Add(concert);
                        }

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
                    cmd.CommandText = @"SELECT * FROM Concert
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    Concert concert = null;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            concert = new Concert { Id = id, Name = DbUtils.GetString(reader, "Name") };
                        }
                    }
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
                    cmd.CommandText = @"INSERT INTO Concert (Name)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Name)";

                    DbUtils.AddParameter(cmd, "@Name", concert.Name);

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
                    cmd.CommandText = @"UPDATE CONCERT SET Name = @name WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@name", concert.Name);
                    DbUtils.AddParameter(cmd, "@id", concert.Id);

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