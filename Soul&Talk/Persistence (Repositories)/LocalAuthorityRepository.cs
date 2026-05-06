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
    public class LocalAuthorityRepository : IRepository<LocalAuthority>
    {
        private readonly string connectionString;
        private List<Citizen> Citizens = new List<Citizen>();

        public LocalAuthorityRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            this.connectionString = connectionString;
        }

        public List<LocalAuthority> GetAll()
        {
            List<LocalAuthority> localAuthorities = new List<LocalAuthority>();

            using (SqlConnection connection = new SqlConnection(connectionString))
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


        public LocalAuthority GetById(int id)
        {
            LocalAuthority? LocalAuthority = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        //IRepository CRUD metoder.
        public int Add(LocalAuthority model)
        {
            throw new NotImplementedException();
        }

        public void Update(LocalAuthority model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
