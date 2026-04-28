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
    public class GuestRepository
    {
        private readonly string _connectionString;
        private List<Guest> guests = new List<Guest>();

        public GuestRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _connectionString = connectionString;
        }

        public void AddGuest(Guest guest)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();


            var cmd = new SqlCommand(
                "INSERT INTO Guests (GuestId, Name, Phone, Email) " +
                "VALUES (@GuestId, @Name, @Phone, @Email)",
            conn
        );

            cmd.Parameters.AddWithValue("@GuestId", guest.GuestId);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Phone", guest.Phone);
            cmd.Parameters.AddWithValue("@Email", guest.Email);

            cmd.ExecuteNonQuery();
        }

        public void UpdateGuest(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Guest SET Name = @Name, " +
                    "Phone = @Phone, " +
                    "Email = @Email" +
                    "WHERE GuestId = @GuestId", connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = guest.Email;
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guest.GuestId;
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteGuest(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Guest WHERE GuestId = @GuestId", connection);
                cmd.Parameters.AddWithValue("@GuestId", guest);
                cmd.ExecuteNonQuery();
            }
        }

        public Guest GetGuestById(int id)
        {
            Guest? guest = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Guest WHERE GuestId = @GuestId", connection);
                cmd.Parameters.AddWithValue("@GuestId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        guest = new Guest
                        {
                            GuestId = reader.GetInt32(0),
                            Name = reader["Name"] as string,
                            Phone = reader["Phone"] as string,
                            Email = reader["Email"] as string
                        };
                    }
                }
            }
            return guest;
        }

        public List<Guest> GetAll()
        {
            List<Guest> guests = new List<Guest>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Guest", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guest guest = new Guest
                        {
                            GuestId = reader.GetInt32(0),
                            Name = reader["Name"] as string,
                            Phone = reader["Phone"] as string,
                            Email = reader["Email"] as string
                        };
                        guests.Add(guest);
                    }
                    return guests;
                }
            }
        }
    }
}