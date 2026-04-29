using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class LocalAuthority
    {
        public int _localAuthorityId {  get; private set; }
        public string _localAuthorityName { get; private set; }
        public string _eanNumber { get; private set; }

        public LocalAuthority(int localAuthorityId, string localAuthorityName, string eanNumber)
        {
            this._localAuthorityId = localAuthorityId;
            this._localAuthorityName = localAuthorityName;
            this._eanNumber = eanNumber;
        }
    }
}
