using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class CaseOfficer
    {
        public int CaseOfficerId {  get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        private string _phone {  get; set; }
        private string _email { get; set; }

    

    public CaseOfficer(int CaseOfficerId, string Name, string Department, string phone, string email)
        {
            this.CaseOfficerId = CaseOfficerId;
            this.Name = Name;
            this.Department = Department;
            this._phone = phone;
            this._email = email;
       }
    }
}
