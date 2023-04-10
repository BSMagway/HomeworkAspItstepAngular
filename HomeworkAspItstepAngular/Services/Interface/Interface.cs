namespace HomeworkAspItstepAngular.Services.Interface
{
    public interface IDbService
    {
        void Set();

        void Get();
    }

    public interface IDbServiceProvider<T>
    {
        T Get();
    }

    public interface IDbServiceConsumer<T>
    {
        void Set(T s);
    }

    public class DbServiceManager<T> : IDbServiceProvider<T>, IDbServiceConsumer<T>
    {
        private T _value;

        public DbServiceManager(T value)
        {
            _value = value;
        }

        public T Get()
        {
            return _value;
        }

        public void Set(T s)
        {
            _value = s;
        }
    }
}
