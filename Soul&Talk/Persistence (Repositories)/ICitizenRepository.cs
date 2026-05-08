using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface ICitizenRepository : IRepository<Citizen>
    {
        bool CprExistsForAnotherCitizen(string cprNumber, int citizenId);
    }
}
