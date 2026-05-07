using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    //UC1 og UC3, hvor en gæst kan have borgeroplysninger knyttet til sig.
    public class CitizenRepository : IRepository<Citizen>
    {
        private readonly string connectionString;

        public CitizenRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Citizen> GetAll()
        {
            List<Citizen> citizens = new List<Citizen>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CitizenId, Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations FROM Citizen", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        citizens.Add(ReadCitizen(reader));
                    }
                }
            }

            return citizens;
        }

        public Citizen GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CitizenId, Name, Phone, Email, CprNumber, WorkStatus, WorkType, " +
                    "ConsentStatus, CurrentStatus, SpecialConsiderations FROM Citizen WHERE CitizenId = @CitizenId", connection);
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCitizen(reader) : new Citizen();
                }
            }
        }

        //citizen er identity(1,1), så ID tilføjes ikke i insert, da det er auto-increment i databasen.
        public int Add(Citizen citizen)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Citizen " +
                    "(Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations) " +
                    "VALUES (@Name, @Phone, @Email, @CprNumber, @WorkStatus, @WorkType, @ConsentStatus, @CurrentStatus, @SpecialConsiderations)", connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = citizen.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = citizen.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = citizen.Email;
                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar).Value = citizen.CprNumber;
                cmd.Parameters.Add("@WorkStatus", SqlDbType.NVarChar).Value = citizen.WorkStatus;
                cmd.Parameters.Add("@WorkType", SqlDbType.NVarChar).Value = citizen.WorkType;
                cmd.Parameters.Add("@ConsentStatus", SqlDbType.NVarChar).Value = citizen.ConsentStatus;
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.NVarChar).Value = citizen.CurrentStatus;
                cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar).Value = citizen.SpecialConsiderations;

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public void Update(Citizen citizen)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Citizen SET Name = @Name, Phone = @Phone, Email = @Email, CprNumber = @CprNumber, " +
                    "WorkStatus = @WorkStatus, WorkType = @WorkType, ConsentStatus = @ConsentStatus, CurrentStatus = @CurrentStatus, " +
                    "SpecialConsiderations = @SpecialConsiderations WHERE CitizenId = @CitizenId", connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = citizen.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = citizen.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = citizen.Email;
                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar).Value = citizen.CprNumber;
                cmd.Parameters.Add("@WorkStatus", SqlDbType.NVarChar).Value = citizen.WorkStatus;
                cmd.Parameters.Add("@WorkType", SqlDbType.NVarChar).Value = citizen.WorkType;
                cmd.Parameters.Add("@ConsentStatus", SqlDbType.NVarChar).Value = citizen.ConsentStatus;
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.NVarChar).Value = citizen.CurrentStatus;
                cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar).Value = citizen.SpecialConsiderations;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizen.CitizenId;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            DeleteCitizen(id);
        }

        public void DeleteCitizen(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Citizen WHERE CitizenId = @CitizenId", connection);
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = id;

                cmd.ExecuteNonQuery();
            }
        }

        private static Citizen ReadCitizen(SqlDataReader reader)
        {
            return new Citizen(
                reader.GetInt32(reader.GetOrdinal("CitizenId")),
                reader["Name"] as string,
                reader["Phone"] as string,
                reader["Email"] as string,
                reader["CprNumber"] as string,
                reader["WorkStatus"] as string,
                reader["WorkType"] as string,
                reader["ConsentStatus"] as string,
                reader["CurrentStatus"] as string,
                reader["SpecialConsiderations"] as string);
        }
    }
}