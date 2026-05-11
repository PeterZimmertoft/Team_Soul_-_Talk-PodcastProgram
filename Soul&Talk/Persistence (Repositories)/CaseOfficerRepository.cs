using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class CaseOfficerRepository : ICaseOfficerRepository
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

                SqlCommand cmd = new SqlCommand(
                    "SELECT CaseOfficerId, Name, Department, Phone, Email FROM CaseOfficer",
                    connection);

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

                SqlCommand cmd = new SqlCommand(
                    "SELECT CaseOfficerId, Name, Department, Phone, Email FROM CaseOfficer WHERE CaseOfficerId = @CaseOfficerId",
                    connection);

                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCaseOfficer(reader) : new CaseOfficer();
                }
            }
        }

        public CaseOfficer? GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT CaseOfficerId, Name, Department, Phone, Email FROM CaseOfficer " +
                    "WHERE LOWER(LTRIM(RTRIM(Name))) = LOWER(@Name)",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name.Trim();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCaseOfficer(reader) : null;
                }
            }
        }

        public CaseOfficer GetOrCreateByName(string name)
        {
            string trimmedName = name?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(trimmedName))
            {
                throw new ArgumentException("Case officer name cannot be empty.", nameof(name));
            }

            CaseOfficer? existing = GetByName(trimmedName);
            if (existing != null)
            {
                return existing;
            }

            int id = Add(new CaseOfficer(0, trimmedName, string.Empty, string.Empty, string.Empty));
            return GetById(id);
        }

        public int Add(CaseOfficer model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO CaseOfficer (Name, Department, Phone, Email) " +
                    "VALUES (@Name, @Department, @Phone, @Email); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS int);",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = model.Name?.Trim() ?? string.Empty;
                cmd.Parameters.Add("@Department", SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(model.Department) ? string.Empty : model.Department.Trim();
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(model.Phone) ? string.Empty : model.Phone.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(model.Email) ? string.Empty : model.Email.Trim();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(CaseOfficer model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE CaseOfficer SET Name = @Name, Department = @Department, Phone = @Phone, Email = @Email " +
                    "WHERE CaseOfficerId = @CaseOfficerId",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = model.Name?.Trim() ?? string.Empty;
                cmd.Parameters.Add("@Department", SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(model.Department) ? string.Empty : model.Department.Trim();
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(model.Phone) ? string.Empty : model.Phone.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(model.Email) ? string.Empty : model.Email.Trim();
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = model.CaseOfficerId;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM CaseOfficer WHERE CaseOfficerId = @CaseOfficerId", connection);
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
            }
        }

        private static CaseOfficer ReadCaseOfficer(SqlDataReader reader)
        {
            return new CaseOfficer(
                reader.GetInt32(reader.GetOrdinal("CaseOfficerId")),
                reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString() ?? string.Empty,
                reader["Department"] == DBNull.Value ? string.Empty : reader["Department"].ToString() ?? string.Empty,
                reader["Phone"] == DBNull.Value ? string.Empty : reader["Phone"].ToString() ?? string.Empty,
                reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString() ?? string.Empty);
        }
    }
}
