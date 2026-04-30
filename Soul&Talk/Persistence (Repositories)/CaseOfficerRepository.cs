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
    public class CaseOfficerRepository : ICaseOfficerRepository
    {
        private readonly string _connectionString;
        private List<Citizen> Citizens = new List<Citizen>();

        public CaseOfficerRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _connectionString = connectionString;
        }





        public List<CaseOfficer> GetAllCaseOfficers()
        {
            List<CaseOfficer> caseOfficers = new List<CaseOfficer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CaseOfficer", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CaseOfficer caseOfficer = new CaseOfficer
                        (
                            reader.GetInt32("CaseOfficerId"),
                            reader["Name"] as string,
                            reader["Department"] as string,
                            reader["Phone"] as string,
                            reader["Email"] as string
                        );
                        caseOfficers.Add(caseOfficer);

                    }
                    return caseOfficers;
                }
            }
        }


        public CaseOfficer GetCaseOfficerById(int id)
        {
            CaseOfficer? caseOfficer = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CaseOfficer WHERE CaseOfficerId = @Id", connection);
                cmd.Parameters.AddWithValue("@CaseOfficerId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        caseOfficer = new CaseOfficer
                        (
                            reader.GetInt32("CaseOfficerId"),
                            reader["Name"] as string,
                            reader["Department"] as string,
                            reader["Phone"] as string,
                            reader["Email"] as string
                        );
                    }
                }
            }
            return caseOfficer;
        }
    }
}