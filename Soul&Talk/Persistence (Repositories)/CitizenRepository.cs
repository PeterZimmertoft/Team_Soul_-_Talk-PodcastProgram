using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public class CitizenRepository : ICitizenRepository
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

                SqlCommand cmd = new SqlCommand(
                    "SELECT CitizenId, Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations, CaseOfficerId FROM Citizen",
                    connection);

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

                SqlCommand cmd = new SqlCommand(
                    "SELECT CitizenId, Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations, CaseOfficerId " +
                    "FROM Citizen WHERE CitizenId = @CitizenId",
                    connection);

                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCitizen(reader) : new Citizen();
                }
            }
        }

        public int Add(Citizen model)
        {
            if (model.CaseOfficerId <= 0)
            {
                throw new InvalidOperationException("A case officer must be selected.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Citizen " +
                    "(Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations, CaseOfficerId) " +
                    "VALUES (@Name, @Phone, @Email, @CprNumber, @WorkStatus, @WorkType, @ConsentStatus, @CurrentStatus, @SpecialConsiderations, @CaseOfficerId); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS int);",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = model.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = model.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = model.Email;
                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 11).Value = model.CprNumber;
                cmd.Parameters.Add("@WorkStatus", SqlDbType.NVarChar, 50).Value = model.WorkStatus;
                cmd.Parameters.Add("@WorkType", SqlDbType.NVarChar, 50).Value = model.WorkType;
                cmd.Parameters.Add("@ConsentStatus", SqlDbType.Bit).Value = model.ConsentStatus == "Ja";
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.NVarChar, 50).Value = model.CurrentStatus;
                cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar, 255).Value = string.IsNullOrWhiteSpace(model.SpecialConsiderations) ? DBNull.Value : model.SpecialConsiderations;
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = model.CaseOfficerId;

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(Citizen model)
        {
            if (model.CaseOfficerId <= 0)
            {
                throw new InvalidOperationException("A case officer must be selected.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Citizen SET Name = @Name, Phone = @Phone, Email = @Email, CprNumber = @CprNumber, " +
                    "WorkStatus = @WorkStatus, WorkType = @WorkType, ConsentStatus = @ConsentStatus, CurrentStatus = @CurrentStatus, " +
                    "SpecialConsiderations = @SpecialConsiderations, CaseOfficerId = @CaseOfficerId WHERE CitizenId = @CitizenId",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = model.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(model.Phone) ? DBNull.Value : model.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = model.Email;
                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 11).Value = model.CprNumber;
                cmd.Parameters.Add("@WorkStatus", SqlDbType.NVarChar, 50).Value = model.WorkStatus;
                cmd.Parameters.Add("@WorkType", SqlDbType.NVarChar, 50).Value = model.WorkType;
                cmd.Parameters.Add("@ConsentStatus", SqlDbType.Bit).Value = model.ConsentStatus == "Ja";
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.NVarChar, 50).Value = model.CurrentStatus;
                cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar, 255).Value = string.IsNullOrWhiteSpace(model.SpecialConsiderations) ? DBNull.Value : model.SpecialConsiderations;
                cmd.Parameters.Add("@CaseOfficerId", SqlDbType.Int).Value = model.CaseOfficerId;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = model.CitizenId;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Citizen WHERE CitizenId = @CitizenId", connection);
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = id;

                cmd.ExecuteNonQuery();
            }
        }

        public bool CprExistsForAnotherCitizen(string cprNumber, int citizenId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Citizen WHERE CprNumber = @CprNumber AND CitizenId <> @CitizenId",
                    connection);

                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 11).Value = cprNumber;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizenId;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private static Citizen ReadCitizen(SqlDataReader reader)
        {
            string consentStatus = Convert.ToBoolean(reader["ConsentStatus"]) ? "Ja" : "Nej";
            int caseOfficerId = reader["CaseOfficerId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CaseOfficerId"]);

            return new Citizen(
                reader.GetInt32(reader.GetOrdinal("CitizenId")),
                reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString() ?? string.Empty,
                reader["Phone"] == DBNull.Value ? string.Empty : reader["Phone"].ToString() ?? string.Empty,
                reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString() ?? string.Empty,
                reader["CprNumber"] == DBNull.Value ? string.Empty : reader["CprNumber"].ToString() ?? string.Empty,
                reader["WorkStatus"] == DBNull.Value ? string.Empty : reader["WorkStatus"].ToString() ?? string.Empty,
                reader["WorkType"] == DBNull.Value ? string.Empty : reader["WorkType"].ToString() ?? string.Empty,
                consentStatus,
                reader["CurrentStatus"] == DBNull.Value ? string.Empty : reader["CurrentStatus"].ToString() ?? string.Empty,
                reader["SpecialConsiderations"] == DBNull.Value ? string.Empty : reader["SpecialConsiderations"].ToString() ?? string.Empty,
                caseOfficerId);
        }
    }
}
