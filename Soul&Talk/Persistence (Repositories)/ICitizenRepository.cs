using Soul_Talk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface ICitizenRepository
    {
        Citizen GetCitizenById(int id);

        int AddCitizen(Citizen citizen);

        void UpdateCitizen(Citizen citizen);

        void DeleteCitizen(int id);
    }
}
