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

    }
}
