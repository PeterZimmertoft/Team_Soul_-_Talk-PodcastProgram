using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class GuestRepository : IRepository<Guest>
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM Guest", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guest guest = new Guest
                        {
                            GuestId = Convert.ToInt32(reader["GuestId"]),
                            Name = reader["Name"] as string,
                            Phone = reader["Phone"] as string,
                            Email = reader["Email"] as string
                        };

                        guests.Add(guest);
                    }
                }
            }

            return guests;
        }

        public Guest GetById(int id)
        {
            Guest? guest = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Guest WHERE GuestId = @GuestId",
                    connection
                );

                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        guest = new Guest
                        {
                            GuestId = Convert.ToInt32(reader["GuestId"]),
                            Name = reader["Name"] as string,
                            Phone = reader["Phone"] as string,
                            Email = reader["Email"] as string
                        };
                    }
                }
            }

            return guest;
        }

        public int Add(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Guest (Name, Phone, Email) " +
                    "VALUES (@Name, @Phone, @Email); " +
                    "SELECT SCOPE_IDENTITY();",
                    connection
                );

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = guest.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = guest.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = guest.Email;

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Guest SET " +
                    "Name = @Name, " +
                    "Phone = @Phone, " +
                    "Email = @Email " +
                    "WHERE GuestId = @GuestId",
                    connection
                );

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

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Guest WHERE GuestId = @GuestId",
                    connection
                );

                cmd.Parameters.Add("@GuestId", SqlDbType.Int).Value = id;

                cmd.ExecuteNonQuery();
            }
        }

        }

    }