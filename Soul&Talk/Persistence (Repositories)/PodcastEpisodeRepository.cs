using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class PodcastEpisodeRepository : IPodcastEpisodeRepository
    {
        private readonly string connectionString;

        public PodcastEpisodeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<PodcastEpisode> GetAll()
        {
            List<PodcastEpisode> episodes = new List<PodcastEpisode>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT pe.PodcastEpisodeID,
                           pe.Title,
                           pe.[Date],
                           pe.Duration,
                           pe.Status,
                           pe.MeetingPlace,
                           pe.Note,
                           pe.CaseOfficerId,
                           co.Name AS CaseOfficerName
                    FROM PodcastEpisode pe
                    INNER JOIN CaseOfficer co ON co.CaseOfficerId = pe.CaseOfficerId
                    ORDER BY pe.[Date] DESC", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        episodes.Add(ReadPodcastEpisode(reader));
                    }
                }
            }

            return episodes;
        }

        public PodcastEpisode GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT pe.PodcastEpisodeID,
                           pe.Title,
                           pe.[Date],
                           pe.Duration,
                           pe.Status,
                           pe.MeetingPlace,
                           pe.Note,
                           pe.CaseOfficerId,
                           co.Name AS CaseOfficerName
                    FROM PodcastEpisode pe
                    INNER JOIN CaseOfficer co ON co.CaseOfficerId = pe.CaseOfficerId
                    WHERE pe.PodcastEpisodeID = @PodcastEpisodeID", connection);

                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadPodcastEpisode(reader) : new PodcastEpisode();
                }
            }
        }

        public int Add(PodcastEpisode model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO PodcastEpisode (Title, [Date], Duration, Status, MeetingPlace, Note, CaseOfficerId)
                    VALUES (@Title, @Date, @Duration, @Status, @MeetingPlace, @Note, @CaseOfficerId);
                    SELECT CAST(SCOPE_IDENTITY() AS int);", connection);

                AddParameters(cmd, model);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(PodcastEpisode model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE PodcastEpisode SET Title = @Title, [Date] = @Date, Duration = @Duration, " +
                    "Status = @Status, MeetingPlace = @MeetingPlace, Note = @Note, CaseOfficerId = @CaseOfficerId " +
                    "WHERE PodcastEpisodeID = @PodcastEpisodeID", connection);

                AddParameters(cmd, model);
                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = model.PodcastEpisodeID;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand deleteGuests = new SqlCommand(
                    "DELETE FROM PodcastEpisodeGuests WHERE PodcastEpisodeID = @PodcastEpisodeID", connection);
                deleteGuests.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = id;
                deleteGuests.ExecuteNonQuery();

                SqlCommand deleteEpisode = new SqlCommand(
                    "DELETE FROM PodcastEpisode WHERE PodcastEpisodeID = @PodcastEpisodeID", connection);
                deleteEpisode.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = id;
                deleteEpisode.ExecuteNonQuery();
            }
        }

        public int AddGuestToPodcastEpisode(int podcastEpisodeId, int guestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO PodcastEpisodeGuests (PodcastEpisodeID, GuestId) " +
                    "VALUES (@PodcastEpisodeID, @GuestId)", connection);

                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = podcastEpisodeId;
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guestId;

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public List<Guest> GetGuestsForPodcastEpisode(int podcastEpisodeId)
        {
            List<Guest> guests = new List<Guest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT g.GuestId, g.Name, g.Phone, g.Email " +
                    "FROM PodcastEpisodeGuests peg " +
                    "INNER JOIN Guest g ON peg.GuestId = g.GuestId " +
                    "WHERE peg.PodcastEpisodeID = @PodcastEpisodeID", connection);

                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = podcastEpisodeId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        guests.Add(ReadGuest(reader));
                    }
                }
            }

            return guests;
        }

        private static void AddParameters(SqlCommand cmd, PodcastEpisode model)
        {
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = model.Title;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = model.Date;
            cmd.Parameters.Add("@Duration", SqlDbType.Time).Value = model.Duration;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = model.Status;
            cmd.Parameters.Add("@MeetingPlace", SqlDbType.NVarChar, 100).Value = model.MeetingPlace;
            cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = model.CaseOfficerId;
            cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = string.IsNullOrWhiteSpace(model.Note) ? DBNull.Value : model.Note;
        }

        private static PodcastEpisode ReadPodcastEpisode(SqlDataReader reader)
        {
            PodcastEpisode episode = new PodcastEpisode(
                reader.GetInt32(reader.GetOrdinal("PodcastEpisodeID")),
                reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString() ?? string.Empty,
                (DateTime)reader["Date"],
                reader.GetTimeSpan(reader.GetOrdinal("Duration")),
                reader["Status"] == DBNull.Value ? string.Empty : reader["Status"].ToString() ?? string.Empty,
                reader["MeetingPlace"] == DBNull.Value ? string.Empty : reader["MeetingPlace"].ToString() ?? string.Empty,
                reader["Note"] == DBNull.Value ? string.Empty : reader["Note"].ToString() ?? string.Empty);

            episode.CaseOfficerId = reader.GetInt32(reader.GetOrdinal("CaseOfficerId"));
            episode.CaseOfficerName = reader["CaseOfficerName"] == DBNull.Value ? string.Empty : reader["CaseOfficerName"].ToString() ?? string.Empty;

            return episode;
        }

        private static Guest ReadGuest(SqlDataReader reader)
        {
            return new Guest
            {
                GuestId = reader.GetInt32(reader.GetOrdinal("GuestId")),
                Name = reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString() ?? string.Empty,
                Phone = reader["Phone"] == DBNull.Value ? string.Empty : reader["Phone"].ToString() ?? string.Empty,
                Email = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString() ?? string.Empty
            };
        }
    }
}
