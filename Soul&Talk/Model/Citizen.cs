using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Citizen
    {
        public int CitizenId {  get; set; }
        public string Name { get; set; }
        private string _cprNumber { get; set; }
        private string _workStatus { get; set; }
        private string _workType { get; set; }
        private string _consentStatus { get; set; }
        private string _currentStatus { get; set; }
        private string _specialConsiderations { get; set; }
    

    public Citizen(int CitizenId, string Name, string cprNumber, string workStatus, string workType, string consentStatus, string currentStatus, string specialConsiderations)
        {
            this.CitizenId = CitizenId;
            this.Name = Name;
            this._cprNumber = cprNumber;
            this._workStatus = workStatus;
            this._workType = workType;
            this._consentStatus = consentStatus;
            this._currentStatus = currentStatus;
            this._specialConsiderations = specialConsiderations;
        }
    }
}
