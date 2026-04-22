using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class LocalAuthority
    {
        private int _localAuthorityId {  get; set; }
        private string _localAuthorityName { get; set; }
        private string _eanNumber { get; set; }

        public LocalAuthority(int localAuthorityId, string localAuthorityName, string eanNumber)
        {
            this._localAuthorityId = localAuthorityId;
            this._localAuthorityName = localAuthorityName;
            this._eanNumber = eanNumber;
        }
    }
}
