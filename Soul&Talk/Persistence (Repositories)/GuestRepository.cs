using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    //UC1: Opret profil til podcast episode.
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

        public int Add(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Guest (Name, Phone, Email) VALUES (@Name, @Phone, @Email);", connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = guest.Email;

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public void Update(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Guest SET Name = @Name, Phone = @Phone, Email = @Email WHERE GuestId = @GuestId", connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = guest.Email;
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = guest.GuestId;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Guest WHERE GuestId = @GuestId", connection);
                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = id;

                cmd.ExecuteNonQuery();
            }
        }

        public bool ProfileExists(string name, string phone, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Guest WHERE Name = @Name AND Phone = @Phone AND Email = @Email", connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private static Guest ReadGuest(SqlDataReader reader)
        {
            return new Guest
            {
                GuestId = reader.GetInt32(reader.GetOrdinal("GuestId")),
                Name = reader["Name"] as string,
                Phone = reader["Phone"] as string,
                Email = reader["Email"] as string
            };
        }
    }
}