using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;

namespace Soul_Talk.Persistence__Repositories_
{
    public class PodcastEpisodeRepository
    {
        private readonly string _connectionString;
        private List<Guest> guests = new List<Guest>();

        public PodcastEpisodeRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _connectionString = connectionString;
        }


        public List<PodcastEpisode> GetAllPodcastEpisodes()
        {
            List<PodcastEpisode> PodcastEpisodes = new List<PodcastEpisode>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PodcastEpisode", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PodcastEpisode PodcastEpisode = new PodcastEpisode
                        (
                            reader.GetInt32("podcastEpisodeID"),
                            reader["title"] as string,
                            (DateTime)reader["date"],
                            reader.GetInt32("duration"),
                            reader["status"] as string,
                            reader["meetingPlace"] as string,
                            reader["note"] as string
                        );
                        PodcastEpisodes.Add(PodcastEpisode);

                    }
                    return PodcastEpisodes;
                }
            }
        }


        public PodcastEpisode GetLocalAuthorityById(int id)
        {
            PodcastEpisode? PodcastEpisode = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PodcastEpisode WHERE CaseOfficerId = @Id", connection);
                cmd.Parameters.AddWithValue("@CaseOfficerId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PodcastEpisode = new PodcastEpisode
                        (
                            reader.GetInt32("podcastEpisodeID"),
                            reader["title"] as string,
                            (DateTime)reader["date"],
                            reader.GetInt32("duration"),
                            reader["status"] as string,
                            reader["meetingPlace"] as string,
                            reader["note"] as string
                        );
                    }
                }
            }
            return PodcastEpisode;
        }

        public int AddPodcastEpisode(PodcastEpisode PodcastEpisode)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO PodcastEpisode (title, date, duration, status, meetingPlace, note) " +
                    "VALUES (@title, @date, @duration, @status, @meetingPlace, @note); " +
                    "SELECT SCOPE_IDENTITY();",
                conn
            );
                cmd.Parameters.AddWithValue("@title", PodcastEpisode.Title);
                cmd.Parameters.AddWithValue("@date", PodcastEpisode.Date);
                cmd.Parameters.AddWithValue("@duration", PodcastEpisode.Duration);
                cmd.Parameters.AddWithValue("@status", PodcastEpisode._status);
                cmd.Parameters.AddWithValue("@meetingPlace", PodcastEpisode._meetingPlace);
                cmd.Parameters.AddWithValue("@note", PodcastEpisode._note);

                int newId = (int)cmd.ExecuteScalar();
                return newId;
            }
        }

        public void UpdatePodcastEpisode(PodcastEpisode PodcastEpisode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE PodcastEpisode SET title = @title, " +
                    "date = @date, " +
                    "duration = @duration, " +
                    "status = @status, " +
                    "meetingPlace = @meetingPlace, " +
                    "note = @note" +
                    "WHERE podcastEpisodeID = @podcastEpisodeID", connection);
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = PodcastEpisode.Title;
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = PodcastEpisode.Date;
                cmd.Parameters.Add("@duration", SqlDbType.Int).Value = PodcastEpisode.Duration;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = PodcastEpisode._status;
                cmd.Parameters.Add("@meetingPlace", SqlDbType.NVarChar).Value = PodcastEpisode._meetingPlace;
                cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = PodcastEpisode._note;
                cmd.Parameters.Add("@podcastEpisodeID", SqlDbType.Int).Value = PodcastEpisode.PodcastEpisodeID;
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePodcastEpisode(PodcastEpisode PodcastEpisode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM PodcastEpisode WHERE podcastEpisodeID = @podcastEpisodeID", connection);
                cmd.Parameters.AddWithValue("@podcastEpisodeID", PodcastEpisode.PodcastEpisodeID);
                cmd.ExecuteNonQuery();
            }
        }

        public int AddGuestToPodcastEpisode(int podcastEpisodeID, int guestID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO PodcastEpisodeGuests (podcastEpisodeID, guestID) " +
                    "VALUES (@podcastEpisodeID, @guestID); " +
                    "SELECT SCOPE_IDENTITY();",
                conn
            );
                cmd.Parameters.AddWithValue("@podcastEpisodeID", podcastEpisodeID);
                cmd.Parameters.AddWithValue("@guestID", guestID);
                int newId = (int)cmd.ExecuteScalar();
                return newId;
            }
        }
    }
}