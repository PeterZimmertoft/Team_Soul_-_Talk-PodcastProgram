using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class GuestRepository
    {
        private readonly string _connectionString;

        public GuestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddGuest(Guest guest)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();


            var cmd = new SqlCommand(
                "INSERT INTO Guests (GuestId, Name, Phone, Email, ConsentStatus) " +
                "VALUES (@GuestId, @Name, @Phone, @Email, @ConsentStatus)",
            conn
        );

            cmd.Parameters.AddWithValue("@GuestId", guest.GuestId);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Phone", guest.Phone);
            cmd.Parameters.AddWithValue("@Email", guest.Email);
            cmd.Parameters.AddWithValue("@ConsentStatus", guest.ConsentStatus);

            cmd.ExecuteNonQuery();
        }

    }
}

