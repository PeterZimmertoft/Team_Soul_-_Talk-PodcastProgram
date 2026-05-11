using Soul_Talk.Model;
using System.Collections.Generic;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface ICaseOfficerRepository
    {
        List<CaseOfficer> GetAll();
        CaseOfficer GetById(int id);

        int Add(CaseOfficer model);
        void Update(CaseOfficer model);
        void Delete(int id);

        CaseOfficer? GetByName(string name);
        CaseOfficer GetOrCreateByName(string name);
    }
}