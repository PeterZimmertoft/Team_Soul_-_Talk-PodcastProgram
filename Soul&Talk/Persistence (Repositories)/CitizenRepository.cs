using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    // UC1 og UC3, hvor en gæst kan have borgeroplysninger knyttet til sig.
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
                    "SELECT CitizenId, Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations FROM Citizen",
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
                    "SELECT CitizenId, Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations " +
                    "FROM Citizen WHERE CitizenId = @CitizenId",
                    connection);

                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadCitizen(reader) : new Citizen();
                }
            }
        }

        //IRepository - Crud Metoder - Create, Read, Update, Delete

        public int Add(Citizen model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Citizen " +
                    "(Name, Phone, Email, CprNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations) " +
                    "VALUES (@Name, @Phone, @Email, @CprNumber, @WorkStatus, @WorkType, @ConsentStatus, @CurrentStatus, @SpecialConsiderations)",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = model.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = model.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = model.Email;
                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 10).Value = model.CprNumber;
                cmd.Parameters.Add("@WorkStatus", SqlDbType.NVarChar, 50).Value = model.WorkStatus;
                cmd.Parameters.Add("@WorkType", SqlDbType.NVarChar, 50).Value = model.WorkType;
                cmd.Parameters.Add("@ConsentStatus", SqlDbType.Bit).Value = model.ConsentStatus == "Ja";
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.NVarChar, 50).Value = model.CurrentStatus;
                cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar, 255).Value = model.SpecialConsiderations;

                cmd.ExecuteNonQuery();

                SqlCommand getIdCmd = new SqlCommand(
                    "SELECT CitizenId FROM Citizen WHERE CprNumber = @CprNumber",
                    connection);

                getIdCmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 10).Value = model.CprNumber;

                return Convert.ToInt32(getIdCmd.ExecuteScalar());
            }
        }
        private static Citizen ReadCitizen(SqlDataReader reader)
        {
            string consentStatus = "Nej";

            if (Convert.ToBoolean(reader["ConsentStatus"]))
            {
                consentStatus = "Ja";
            }

            return new Citizen(
                reader.GetInt32(reader.GetOrdinal("CitizenId")),
                reader["Name"] as string,
                reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                reader["Email"] as string,
                reader["CprNumber"] as string,
                reader["WorkStatus"] as string,
                reader["WorkType"] as string,
                consentStatus,
                reader["CurrentStatus"] as string,
                reader["SpecialConsiderations"] == DBNull.Value ? null : reader["SpecialConsiderations"].ToString());
        }

        public void Update(Citizen model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Citizen SET Name = @Name, Phone = @Phone, Email = @Email, CprNumber = @CprNumber, " +
                    "WorkStatus = @WorkStatus, WorkType = @WorkType, ConsentStatus = @ConsentStatus, CurrentStatus = @CurrentStatus, " +
                    "SpecialConsiderations = @SpecialConsiderations WHERE CitizenId = @CitizenId",
                    connection);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = model.Name;

                if (model.Phone == null || model.Phone == "")
                {
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = model.Phone;
                }

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = model.Email;
                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 10).Value = model.CprNumber;
                cmd.Parameters.Add("@WorkStatus", SqlDbType.NVarChar, 50).Value = model.WorkStatus;
                cmd.Parameters.Add("@WorkType", SqlDbType.NVarChar, 50).Value = model.WorkType;

                bool consentStatus = false;
                if (model.ConsentStatus == "Ja")
                {
                    consentStatus = true;
                }

                cmd.Parameters.Add("@ConsentStatus", SqlDbType.Bit).Value = consentStatus;
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.NVarChar, 50).Value = model.CurrentStatus;

                if (model.SpecialConsiderations == null || model.SpecialConsiderations == "")
                {
                    cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar, 255).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SpecialConsiderations", SqlDbType.NVarChar, 255).Value = model.SpecialConsiderations;
                }

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


        // En metode til at tjekke, om et CPR-nummer allerede findes for en anden borger i databasen.
        // Dette er vigtigt for at sikre, at CPR-numre er unikke og ikke kan knyttes til flere borgere.
        public bool CprExistsForAnotherCitizen(string cprNumber, int citizenId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Citizen WHERE CprNumber = @CprNumber AND CitizenId <> @CitizenId",
                    connection);

                cmd.Parameters.Add("@CprNumber", SqlDbType.NVarChar, 10).Value = cprNumber;
                cmd.Parameters.Add("@CitizenId", SqlDbType.Int).Value = citizenId;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        
    }
}