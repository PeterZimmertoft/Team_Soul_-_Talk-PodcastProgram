using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    // Arver til IRepository<Guest> og tilføjer metoder, der er specifikke for gæster,
    // såsom at tjekke for eksisterende profiler og håndtere borgeroplysninger knyttet til gæster.
    public interface IGuestRepository : IRepository<Guest>
    {
        bool ProfileExists(string name, string phone, string email);
        bool ProfileExistsForAnotherGuest(string name, string phone, string email, int guestId);

        int AddGuest(Guest guest, int? citizenId);
        void UpdateGuest(Guest guest, int? citizenId);

        int? GetCitizenIdForGuest(int guestId);
        Citizen GetCitizenForGuest(int guestId);
    }
}
