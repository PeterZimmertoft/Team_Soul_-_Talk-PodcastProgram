using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Guest
    {
        public int GuestId {  get; set; }
        public string Name {  get; set; }
        public string Phone {  get; set; }
        public string Email {  get; set; }


        public Guest(int GuestId, string Name, string Phone, string Email)
        {
            this.GuestId = GuestId;
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
        }

        public Guest() { }
    }
}
