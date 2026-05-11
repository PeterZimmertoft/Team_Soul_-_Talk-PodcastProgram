using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Citizen
    {
        public int CitizenId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CprNumber { get; private set; }
        public string WorkStatus { get; set; }
        public string WorkType { get; private set; }
        public string ConsentStatus { get; private set; }
        public string CurrentStatus { get; private set; }
        public string SpecialConsiderations { get; private set; }
        public int CaseOfficerId { get; set; }

        public Citizen() { }

        public Citizen(
            int citizenId,
            string name,
            string phone,
            string email,
            string cprNumber,
            string workStatus,
            string workType,
            string consentStatus,
            string currentStatus,
            string specialConsiderations,
            int caseOfficerId)
        {
            CitizenId = citizenId;
            Name = name;
            Phone = phone;
            Email = email;
            CprNumber = cprNumber;
            WorkStatus = workStatus;
            WorkType = workType;
            ConsentStatus = consentStatus;
            CurrentStatus = currentStatus;
            SpecialConsiderations = specialConsiderations;
            CaseOfficerId = caseOfficerId;
        }
    }
}
