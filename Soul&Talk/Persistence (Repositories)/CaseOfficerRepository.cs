using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class CaseOfficerRepository : IRepository<CaseOfficer>
    {
        private readonly string connectionString;

        public CaseOfficerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CaseOfficer> GetAll()
        {
            List<CaseOfficer> caseOfficers = new List<CaseOfficer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CaseOfficerId, Name, Department, Phone, Email FROM CaseOfficer", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        caseOfficers.Add(ReadCaseOfficer(reader));
                    }
                }
            }

            return caseOfficers;
        }

        public CaseOfficer GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CaseOfficerId, Name, Department, Phone, Email FROM CaseOfficer WHERE CaseOfficerId = @CaseOfficerId", connection);
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCaseOfficer(reader) : new CaseOfficer();
                }
            }
        }

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

        private static CaseOfficer ReadCaseOfficer(SqlDataReader reader)
        {
            return new CaseOfficer(
                reader.GetInt32(reader.GetOrdinal("CaseOfficerId")),
                reader["Name"] as string,
                reader["Department"] as string,
                reader["Phone"] as string,
                reader["Email"] as string);
        }
    }
}