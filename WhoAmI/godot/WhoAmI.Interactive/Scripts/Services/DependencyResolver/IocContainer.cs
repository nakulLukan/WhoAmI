namespace WhoAmI.Services.DependencyResolver
{
    public static class IocContainer
    {
        public static IocSingleton Singleton = new IocSingleton();
        public static IocTransient Transient = new IocTransient();
    }

}