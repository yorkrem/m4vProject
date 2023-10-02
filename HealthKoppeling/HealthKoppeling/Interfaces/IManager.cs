namespace HealthKoppeling.Interfaces
{
    public interface IManager<T>
    {
        public List<T> Get();

        public void Add(T item);

        public void Update(T item);

        public void Remove(T item);

        public bool CheckIfExists(T item);
    }
}
