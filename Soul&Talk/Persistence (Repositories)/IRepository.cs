namespace Soul_Talk.Persistence__Repositories_
{
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public int Add(T model);
        public void Update(T model);
        public void Delete(int id);
    }
}
