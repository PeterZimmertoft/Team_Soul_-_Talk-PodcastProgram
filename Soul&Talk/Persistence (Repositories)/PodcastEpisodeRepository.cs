using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    //UC2: Planlæg podcast-episode.
    public class PodcastEpisodeRepository : IRepository<PodcastEpisode>
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

                SqlCommand cmd = new SqlCommand("SELECT PodcastEpisode.PodcastEpisodeID, PodcastEpisode.Title, PodcastEpisode.Date, PodcastEpisode.Duration, " +
                    "PodcastEpisode.Status, PodcastEpisode.MeetingPlace, PodcastEpisode.Note, PodcastEpisode.CaseOfficerId, CaseOfficer.Name AS CaseOfficerName " +
                    "FROM PodcastEpisode INNER JOIN CaseOfficer ON PodcastEpisode.CaseOfficerId = CaseOfficer.CaseOfficerId", connection);

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

                SqlCommand cmd = new SqlCommand("SELECT PodcastEpisode.PodcastEpisodeID, PodcastEpisode.Title, PodcastEpisode.Date, PodcastEpisode.Duration," +
                    " PodcastEpisode.Status, PodcastEpisode.MeetingPlace, PodcastEpisode.Note, PodcastEpisode.CaseOfficerId, " +
                    "CaseOfficer.Name AS CaseOfficerName FROM PodcastEpisode INNER JOIN CaseOfficer ON PodcastEpisode.CaseOfficerId = CaseOfficer.CaseOfficerId " +
                    "WHERE PodcastEpisode.PodcastEpisodeID = @podcastEpisodeID", connection);
                cmd.Parameters.Add("@podcastEpisodeID", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadPodcastEpisode(reader) : new PodcastEpisode();
                }
            }
        }

        public int Add(PodcastEpisode model)
        {
            return AddPodcastEpisode(model);
        }

        public void Update(PodcastEpisode model)
        {
            UpdatePodcastEpisode(model);
        }

        public void Delete(int id)
        {
            DeletePodcastEpisode(id);
        }

        public int AddPodcastEpisode(PodcastEpisode podcastEpisode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO " +
                    "PodcastEpisode (Title, Date, Duration, Status, MeetingPlace, Note, CaseOfficerId)" +

                    " VALUES " +
                    "(@Title, @Date, @Duration, @Status, @MeetingPlace, @Note, @CaseOfficerId);",
                    connection);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = podcastEpisode.Title;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = podcastEpisode.Date;
                cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = podcastEpisode.Duration;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = podcastEpisode.Status;
                cmd.Parameters.Add("@MeetingPlace", SqlDbType.NVarChar).Value = podcastEpisode.MeetingPlace;
                cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = podcastEpisode.Note;
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = podcastEpisode.CaseOfficerId;

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public void UpdatePodcastEpisode(PodcastEpisode podcastEpisode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UPDATE PodcastEpisode SET Title = @Title, Date = @Date, " +
                    "Duration = @Duration, Status = @Status, MeetingPlace = @MeetingPlace, Note = @Note, CaseOfficerId = @CaseOfficerId WHERE PodcastEpisodeID = @PodcastEpisodeID", connection);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = podcastEpisode.Title;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = podcastEpisode.Date;
                cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = podcastEpisode.Duration;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = podcastEpisode.Status;
                cmd.Parameters.Add("@MeetingPlace", SqlDbType.NVarChar).Value = podcastEpisode.MeetingPlace;
                cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = podcastEpisode.Note;
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = podcastEpisode.CaseOfficerId;
                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = podcastEpisode.PodcastEpisodeID;

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePodcastEpisode(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM PodcastEpisode WHERE PodcastEpisodeID = @PodcastEpisodeID", connection);
                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = id;

                cmd.ExecuteNonQuery();
            }
        }

        public int AddGuestToPodcastEpisode(int podcastEpisodeId, int guestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO PodcastEpisodeGuests (PodcastEpisodeID, GuestId) VALUES (@PodcastEpisodeID, @GuestId);", connection);

                cmd.Parameters.Add("@PodcastEpisodeID", SqlDbType.Int).Value = podcastEpisodeId;
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guestId;

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        private static PodcastEpisode ReadPodcastEpisode(SqlDataReader reader)
        {
            return new PodcastEpisode(
                reader.GetInt32(reader.GetOrdinal("PodcastEpisodeID")),
                reader["Title"] as string,
                (DateTime)reader["Date"],
                reader.GetInt32(reader.GetOrdinal("Duration")),
                reader["Status"] as string,
                reader["MeetingPlace"] as string,
                reader["Note"] as string)
            {
                CaseOfficerId = reader.GetInt32(reader.GetOrdinal("CaseOfficerId")),
                CaseOfficerName = reader["CaseOfficerName"] as string
            };
        }
    }
}