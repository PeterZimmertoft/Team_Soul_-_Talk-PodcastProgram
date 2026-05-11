using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class GuestRepository : IGuestRepository
    {
        private readonly string connectionString;

        public GuestRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Guest> GetAll()
        {
            List<Guest> guests = new List<Guest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT GuestId, Name, Phone, Email FROM Guest", connection);

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

        public Guest GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT GuestId, Name, Phone, Email FROM Guest WHERE GuestId = @GuestId", connection);
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadGuest(reader) : new Guest();
                }
            }
        }

        public int Add(Guest guest, int? citizenId = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Guest (Name, Phone, Email, CitizenId) " +
                    "VALUES (@Name, @Phone, @Email, @CitizenId); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS int);",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value =
                    string.IsNullOrWhiteSpace(guest.Phone) ? DBNull.Value : guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = guest.Email;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value =
                    citizenId.HasValue ? citizenId.Value : DBNull.Value;

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(Guest guest, int? citizenId = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Guest SET Name = @Name, Phone = @Phone, Email = @Email, CitizenId = @CitizenId " +
                    "WHERE GuestId = @GuestId",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value =
                    string.IsNullOrWhiteSpace(guest.Phone) ? DBNull.Value : guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = guest.Email;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value =
                    citizenId.HasValue ? citizenId.Value : DBNull.Value;
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guest.GuestId;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            int? citizenId = GetCitizenIdForGuest(id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand deleteEpisodeLinks = new SqlCommand(
                    "DELETE FROM PodcastEpisodeGuests WHERE GuestId = @GuestId", connection);
                deleteEpisodeLinks.Parameters.Add("@GuestId", SqlDbType.Int).Value = id;
                deleteEpisodeLinks.ExecuteNonQuery();

                SqlCommand deleteGuest = new SqlCommand("DELETE FROM Guest WHERE GuestId = @GuestId", connection);
                deleteGuest.Parameters.Add("@GuestId", SqlDbType.Int).Value = id;
                deleteGuest.ExecuteNonQuery();

                if (citizenId.HasValue)
                {
                    SqlCommand deleteCitizen = new SqlCommand("DELETE FROM Citizen WHERE CitizenId = @CitizenId", connection);
                    deleteCitizen.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizenId.Value;
                    deleteCitizen.ExecuteNonQuery();
                }
            }
        }

        public bool ProfileExists(string name, string phone, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Guest WHERE Name = @Name AND Phone = @Phone AND Email = @Email",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = email;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool ProfileExistsForAnotherGuest(string name, string phone, string email, int guestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Guest WHERE Name = @Name AND Phone = @Phone AND Email = @Email AND GuestId <> @GuestId",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = email;
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guestId;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public int? GetCitizenIdForGuest(int guestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CitizenId FROM Guest WHERE GuestId = @GuestId", connection);
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guestId;

                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? null : Convert.ToInt32(result);
            }
        }

        public Citizen? GetCitizenForGuest(int guestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT c.CitizenId, c.Name, c.Phone, c.Email, c.CprNumber, c.WorkStatus, c.WorkType, " +
                    "c.ConsentStatus, c.CurrentStatus, c.SpecialConsiderations, c.CaseOfficerId " +
                    "FROM Guest g INNER JOIN Citizen c ON g.CitizenId = c.CitizenId " +
                    "WHERE g.GuestId = @GuestId",
                    connection);

                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guestId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCitizen(reader) : null;
                }
            }
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

        private static Citizen ReadCitizen(SqlDataReader reader)
        {
            string consentStatus = Convert.ToBoolean(reader["ConsentStatus"]) ? "Ja" : "Nej";
            int caseOfficerId = reader["CaseOfficerId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CaseOfficerId"]);

            return new Citizen(
                reader.GetInt32(reader.GetOrdinal("CitizenId")),
                reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString() ?? string.Empty,
                reader["Phone"] == DBNull.Value ? string.Empty : reader["Phone"].ToString() ?? string.Empty,
                reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString() ?? string.Empty,
                reader["CprNumber"] == DBNull.Value ? string.Empty : reader["CprNumber"].ToString() ?? string.Empty,
                reader["WorkStatus"] == DBNull.Value ? string.Empty : reader["WorkStatus"].ToString() ?? string.Empty,
                reader["WorkType"] == DBNull.Value ? string.Empty : reader["WorkType"].ToString() ?? string.Empty,
                consentStatus,
                reader["CurrentStatus"] == DBNull.Value ? string.Empty : reader["CurrentStatus"].ToString() ?? string.Empty,
                reader["SpecialConsiderations"] == DBNull.Value ? string.Empty : reader["SpecialConsiderations"].ToString() ?? string.Empty,
                caseOfficerId);
        }
    }
}
