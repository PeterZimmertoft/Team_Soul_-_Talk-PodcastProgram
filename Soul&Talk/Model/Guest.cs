using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class Guest
    {
        public int GuestId {  get; set; }
        public string Name {  get; set; }
        private string phone {  get; set; }
        private string email {  get; set; }
        public bool ConsentStatus {  get; set; }

        public Guest(int id)
        {
            
        }


        
    }
}
