using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Citizen : Guest
    {
        public int CitizenId {  get; set; }
        public string Name { get; set; }
        public string CprNumber { get; private set; }
        public string WorkStatus { get; set; }
        public string WorkType { get; private set; }
        public string ConsentStatus { get; private set; }
        public string CurrentStatus { get; private set; }
        public string SpecialConsiderations { get; private set; }



        public Citizen() : base() { }


        public Citizen(int CitizenId, string Name, string Phone, string Email, string _cprNumber, string _workStatus, string _workType, string _consentStatus, string _currentStatus, string _specialConsiderations) :
            base(0, Name, Phone, Email)
        {
            this.CitizenId = CitizenId;
            this.Name = Name;
            this.CprNumber = _cprNumber;
            this.WorkStatus = _workStatus;
            this.WorkType = _workType;
            this.ConsentStatus = _consentStatus;
            this.CurrentStatus = _currentStatus;
            this.SpecialConsiderations = _specialConsiderations;
        }
    }
}
