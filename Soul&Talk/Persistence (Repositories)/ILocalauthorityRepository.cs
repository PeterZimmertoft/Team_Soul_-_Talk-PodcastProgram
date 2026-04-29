using Soul_Talk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface ILocalAuthorityRepository
    {
        List<LocalAuthority> GetAllLocalAuthorities();
        LocalAuthority GetLocalAuthorityById(int id);
    }
}
