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
    public class CitizenRepository : ICitizenRepository
    {
        private readonly string _connectionString;
        private List<Citizen> Citizens = new List<Citizen>();

        public CitizenRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _connectionString = connectionString;
        }


        public Citizen GetCitizenById(int id)
        {
            Citizen? Citizen = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Citizen WHERE CitizenId = @Id", connection);
                cmd.Parameters.AddWithValue("@CitizenId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Citizen = new Citizen
                        (
                            reader.GetInt32(0),
                            reader["Name"] as string,
                            reader["Phone"] as string,
                            reader["Email"] as string,
                            reader["_cprNumber"] as string,
                            reader["_workStatus"] as string,
                            reader["_workType"] as string,
                            reader["_consentStatus"] as string,
                            reader["_currentStatus"] as string,
                            reader["_specialConsiderations"] as string
                        );
                    }
                }
            }
            return Citizen;
        }


        public int AddCitizen(Citizen citizen)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Citizens (CitizenId, Name, Phone, Email, _cprNumber, _workStatus, _workType, _consentStatus, _currentStatus, _specialConsiderations) " +
                    "VALUES (@CitizenId, @Name, @Phone, @Email, @_cprNumber, @_workStatus, @_workType, @_consentStatus, @_currentStatus, @_specialConsiderations)",
                conn
            );
                cmd.Parameters.AddWithValue("@CitizenId", citizen.CitizenId);
                cmd.Parameters.AddWithValue("@Name", citizen.Name);
                cmd.Parameters.AddWithValue("@Phone", citizen.Phone);
                cmd.Parameters.AddWithValue("@Email", citizen.Email);
                cmd.Parameters.AddWithValue("@_cprNumber", citizen._cprNumber);
                cmd.Parameters.AddWithValue("@_workStatus", citizen._workStatus);
                cmd.Parameters.AddWithValue("@_workType", citizen._workType);
                cmd.Parameters.AddWithValue("@_consentStatus", citizen._consentStatus);
                cmd.Parameters.AddWithValue("@_currentStatus", citizen._currentStatus);
                cmd.Parameters.AddWithValue("@_specialConsiderations", citizen._specialConsiderations);
                
                return (int)cmd.ExecuteScalar();


            }
        }
        public void UpdateCitizen(Citizen citizen)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Citizen SET Name = @Name, " +
                    "Phone = @Phone, " +
                    "Email = @Email, " +
                    "_cprNumber = @_cprNumber, " +
                    "_workStatus = @_workStatus, " +
                    "_workType = @_workType, " +
                    "_consentStatus = @_consentStatus, " +
                    "_currentStatus = @_currentStatus, " +
                    "_specialConsiderations = @_specialConsiderations" +
                    "WHERE CitizenId = @CitizenId", connection);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = citizen.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = citizen.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = citizen.Email;
                cmd.Parameters.Add("@_cprNumber", SqlDbType.NVarChar).Value = citizen._cprNumber;
                cmd.Parameters.Add("@_workStatus", SqlDbType.NVarChar).Value = citizen._workStatus;
                cmd.Parameters.Add("@_workType", SqlDbType.NVarChar).Value = citizen._workType;
                cmd.Parameters.Add("@_consentStatus", SqlDbType.NVarChar).Value = citizen._consentStatus;
                cmd.Parameters.Add("@_currentStatus", SqlDbType.NVarChar).Value = citizen._currentStatus;
                cmd.Parameters.Add("@_specialConsiderations", SqlDbType.NVarChar).Value = citizen._specialConsiderations;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizen.CitizenId;
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCitizen(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Citizen WHERE CitizenId = @CitizenId", connection);
                cmd.Parameters.AddWithValue("@CitizenId", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}