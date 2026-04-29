using Soul_Talk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface IGuestRepository
    {
        
        List<Guest> GetAllGuests();

        
        Guest GetGuestById(int id);

       
        void AddGuest(Guest guest);

        
        void UpdateGuest(Guest guest);

        
        void DeleteGuest(int id);
    }
}
