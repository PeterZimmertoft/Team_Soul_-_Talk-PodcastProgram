using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{

    // Kommune repository, som overholder virksomhedens behov. Da en sagsbehandler kommer fra en kommune,
    // men den bruges ikke i systemet da den ikke er relevant for UC1, UC2 og UC3. Den er dog implementeret
    // for at vise hvordan det ville se ud hvis det skulle bruges i fremtiden. 
    public class LocalAuthorityRepository : IRepository<LocalAuthority>
    {
        private readonly string connectionString;

        public LocalAuthorityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<LocalAuthority> GetAll()
        {
            List<LocalAuthority> localAuthorities = new List<LocalAuthority>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT LocalAuthorityId, LocalAuthorityName, EanNumber FROM LocalAuthority", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        localAuthorities.Add(ReadLocalAuthority(reader));
                    }
                }
            }

            return localAuthorities;
        }

        public LocalAuthority GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT LocalAuthorityId, LocalAuthorityName, EanNumber FROM LocalAuthority WHERE LocalAuthorityId = @LocalAuthorityId", connection);
                cmd.Parameters.Add("@LocalAuthorityId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadLocalAuthority(reader) : new LocalAuthority();
                }
            }
        }

        private static LocalAuthority ReadLocalAuthority(SqlDataReader reader)
        {
            return new LocalAuthority(
                reader.GetInt32(reader.GetOrdinal("LocalAuthorityId")),
                reader["LocalAuthorityName"] as string,
                reader["EanNumber"] as string);
        }

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
