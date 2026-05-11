using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class CaseOfficer
    {
        public int CaseOfficerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        public CaseOfficer() { }

        public CaseOfficer(int caseOfficerId, string name, string department, string phone, string email)
        {
            CaseOfficerId = caseOfficerId;
            Name = name;
            Department = department;
            Phone = phone;
            Email = email;
        }
    }
}
