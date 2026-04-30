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
    public class LocalAuthorityRepository : ILocalAuthorityRepository
    {
        private readonly string _connectionString;
        private List<Citizen> Citizens = new List<Citizen>();

        public LocalAuthorityRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _connectionString = connectionString;
        }





        public List<LocalAuthority> GetAllLocalAuthorities()
        {
            List<LocalAuthority> localAuthorities = new List<LocalAuthority>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LocalAuthority", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LocalAuthority localAuthority = new LocalAuthority
                        (
                            reader.GetInt32("localAuthorityId"),
                            reader["localAuthorityName"] as string,
                            reader["eanNumber"] as string
                        );
                        localAuthorities.Add(localAuthority);

                    }
                    return localAuthorities;
                }
            }
        }


        public LocalAuthority GetLocalAuthorityById(int id)
        {
            LocalAuthority? LocalAuthority = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LocalAuthority WHERE CaseOfficerId = @Id", connection);
                cmd.Parameters.AddWithValue("@CaseOfficerId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        LocalAuthority = new LocalAuthority
                        (
                            reader.GetInt32("localAuthorityId"),
                            reader["localAuthorityName"] as string,
                            reader["eanNumber"] as string
                        );
                    }
                }
            }
            return LocalAuthority;
        }
    }
}
