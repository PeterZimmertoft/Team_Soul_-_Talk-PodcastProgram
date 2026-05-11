using Soul_Talk.Model;
using System.Collections.Generic;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface IGuestRepository
    {
        List<Guest> GetAll();
        Guest GetById(int id);

        int Add(Guest guest, int? citizenId = null);
        void Update(Guest guest, int? citizenId = null);
        void Delete(int id);

        bool ProfileExists(string name, string phone, string email);
        bool ProfileExistsForAnotherGuest(string name, string phone, string email, int guestId);

        int? GetCitizenIdForGuest(int guestId);
        Citizen? GetCitizenForGuest(int guestId);
    }
}