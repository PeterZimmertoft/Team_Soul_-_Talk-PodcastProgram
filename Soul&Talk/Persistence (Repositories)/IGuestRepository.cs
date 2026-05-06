using Soul_Talk.Model;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface IGuestRepository : IRepository<Guest>
    {
        bool ProfileExists(string name, string phone, string email);
    }
}
