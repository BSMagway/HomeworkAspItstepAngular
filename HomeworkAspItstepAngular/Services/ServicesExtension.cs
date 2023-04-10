using HomeworkAspItstepAngular.Services.Interface;

namespace HomeworkAspItstepAngular.Services
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddManagedSingleton<T>(this IServiceCollection sc, T instance)
        {
            var impl = new DbServiceManager<T>(instance);
            sc.AddSingleton<IDbServiceProvider<T>>(impl);
            sc.AddSingleton<IDbServiceConsumer<T>>(impl);

            return sc;
        }
    }
}
