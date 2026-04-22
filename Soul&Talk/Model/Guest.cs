using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Guest
    {
        public int GuestId {  get; set; }
        public string Name {  get; set; }
        private string _phone {  get; set; }
        private string _email {  get; set; }
        public bool ConsentStatus {  get; set; }

        public Guest(int GuestId, string Name, string _phone, string _email, bool ConsentStatus)
        {
            this.GuestId = GuestId;
            this.Name = Name;
            this._phone = _phone;
            this._email = _email;
            this.ConsentStatus = ConsentStatus;
        }


        
    }
}
