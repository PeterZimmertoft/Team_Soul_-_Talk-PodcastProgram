using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    // UC1 og UC3: Opret, rediger og slet gæsteprofil.
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

        public int Add(Guest model)
        {
            return AddGuest(model, null);
        }

        // Overload, der tillader tilknytning af en Citizen ved oprettelse af en Guest. Hvis citizenId er null, oprettes Guest uden tilknytning til en Citizen.
        public int AddGuest(Guest guest, int? citizenId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Guest (Name, Phone, Email, CitizenId) " +
                    "VALUES (@Name, @Phone, @Email, @CitizenId)",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = guest.Email;

                if (citizenId.HasValue)
                {
                    cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizenId.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = DBNull.Value;
                }

                cmd.ExecuteNonQuery();

                SqlCommand getIdCmd = new SqlCommand(
                    "SELECT TOP 1 GuestId FROM Guest " +
                    "WHERE Name = @Name AND Phone = @Phone AND Email = @Email " +
                    "ORDER BY GuestId DESC",
                    connection);

                getIdCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = guest.Name;
                getIdCmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = guest.Phone;
                getIdCmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = guest.Email;

                return Convert.ToInt32(getIdCmd.ExecuteScalar());
            }
        }


        public void Update(Guest model)
        {
            int? citizenId = GetCitizenIdForGuest(model.GuestId);
            UpdateGuest(model, citizenId);
        }

        public void UpdateGuest(Guest guest, int? citizenId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Guest SET Name = @Name, Phone = @Phone, Email = @Email, CitizenId = @CitizenId " +
                    "WHERE GuestId = @GuestId",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = guest.Name;

                if (guest.Phone == null || guest.Phone == "")
                {
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = guest.Phone;
                }

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = guest.Email;

                if (citizenId.HasValue)
                {
                    cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizenId.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = DBNull.Value;
                }

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

                SqlCommand deleteEpisodeLinks = new SqlCommand("DELETE FROM PodcastEpisodeGuests WHERE GuestId = @GuestId", connection);
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
                    "SELECT COUNT(1) FROM Guest " +
                    "WHERE Name = @Name AND Phone = @Phone AND Email = @Email AND GuestId <> @GuestId",
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

                if (result == null || result == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(result);
            }
        }

        public Citizen GetCitizenForGuest(int guestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT c.CitizenId, c.Name, c.Phone, c.Email, c.CprNumber, c.WorkStatus, c.WorkType, " +
                    "c.ConsentStatus, c.CurrentStatus, c.SpecialConsiderations " +
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
            Guest guest = new Guest();

            guest.GuestId = reader.GetInt32(reader.GetOrdinal("GuestId"));
            guest.Name = reader["Name"] as string;
            guest.Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString();
            guest.Email = reader["Email"] as string;

            return guest;
        }

        private static Citizen ReadCitizen(SqlDataReader reader)
        {
            string consentStatus = "Nej";

            if (Convert.ToBoolean(reader["ConsentStatus"]))
            {
                consentStatus = "Ja";
            }

            return new Citizen(
                reader.GetInt32(reader.GetOrdinal("CitizenId")),
                reader["Name"] as string,
                reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                reader["Email"] as string,
                reader["CprNumber"] as string,
                reader["WorkStatus"] as string,
                reader["WorkType"] as string,
                consentStatus,
                reader["CurrentStatus"] as string,
                reader["SpecialConsiderations"] == DBNull.Value ? null : reader["SpecialConsiderations"].ToString());
            
        }
    }
}
