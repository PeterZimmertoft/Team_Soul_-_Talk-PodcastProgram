using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;
using Microsoft.Extensions.Configuration;

namespace Soul_Talk.Persistence__Repositories_
{
    public class CaseOfficerRepository : IRepository<CaseOfficer>
    {
        private readonly string connectionString;
        private List<Citizen> Citizens = new List<Citizen>();

        public CaseOfficerRepository(string connectionString)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            connectionString = connectionString;
        }

        public List<CaseOfficer> GetAll()
        {
            List<CaseOfficer> caseOfficers = new List<CaseOfficer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
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


        public CaseOfficer GetById(int id)
        {
            CaseOfficer? caseOfficer = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CaseOfficer WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);
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

        //IRepository CRUD metoder.
        public int Add(CaseOfficer model)
        {
            throw new NotImplementedException();
        }

        public void Update(CaseOfficer model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}