using Soul_Talk.Model;
using System.Collections.Generic;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface ICitizenRepository
    {
        List<Citizen> GetAll();
        Citizen GetById(int id);

        int Add(Citizen model);
        void Update(Citizen model);
        void Delete(int id);

        bool CprExistsForAnotherCitizen(string cprNumber, int citizenId);
    }
}
