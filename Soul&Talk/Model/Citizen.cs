using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Citizen
    {
        public int CitizenId {  get; set; }
        public string Name { get; set; }
        private string cprnumber { get; set; }
        private string workStatus { get; set; }
        private string workType { get; set; }
        private string consentStatus { get; set; }
        private string currentStatus { get; set; }
        private string specialConsiderations { get; set; }
    }
}
