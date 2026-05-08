namespace Soul_Talk.Persistence__Repositories_
{
    // Et generisk repository interface, som alle repositories i systemet implementerer. Det indeholder de CRUD operationer, som alle repositories skal have.
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public int Add(T model);
        public void Update(T model);
        public void Delete(int id);
    }
}
