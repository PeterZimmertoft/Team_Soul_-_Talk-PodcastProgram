using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Citizen : Guest
    {
        public int CitizenId {  get; set; }
        public string Name { get; set; }
        public string _cprNumber { get; private set; }
        public string _workStatus { get; set; }
        public string _workType { get; private set; }
        public string _consentStatus { get; private set; }
        public string _currentStatus { get; private set; }
        public string _specialConsiderations { get; private set; }



        public Citizen() : base() { }

        public Citizen(int CitizenId, string Name, string Phone, string Email, string _cprNumber, string _workStatus, string _workType, string _consentStatus, string _currentStatus, string _specialConsiderations) :
            base(CitizenId, Name, Phone, Email)
        {
            this.CitizenId = CitizenId;
            this.Name = Name;
            this._cprNumber = _cprNumber;
            this._workStatus = _workStatus;
            this._workType = _workType;
            this._consentStatus = _consentStatus;
            this._currentStatus = _currentStatus;
            this._specialConsiderations = _specialConsiderations;
        }
    }
}
